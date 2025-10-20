using Microsoft.AspNetCore.Mvc;
using OCPI.Net.Client;
using OCPI.Models;

namespace OCPI.Controllers;

    [Route("ocpi/2.2/tokens")]
    [ApiController]
    public class TokensController : ControllerBase
    {
        private readonly IOcpiClient _ocpiClient;
        private readonly OcpiDbContext _context;

        public TokensController(IOcpiClient ocpiClient, OcpiDbContext context)
        {
            _ocpiClient = ocpiClient;
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CreateToken([FromBody] OcpiToken token)
        {
            token.LastUpdated = DateTime.UtcNow;
            _context.Tokens.Add(token);
            await _context.SaveChangesAsync();

            // OCPI platformuna senkronize et
            var response = await _ocpiClient.PutTokenAsync(token.Uid, token);

            return response.IsSuccess
                ? CreatedAtAction(nameof(GetToken), new { uid = token.Uid }, token)
                : BadRequest(response.ErrorResponse);
        }

        [HttpGet("{uid}")]
        public async Task<IActionResult> GetToken(string uid)
        {
            var token = await _context.Tokens.FindAsync(uid);
            return token != null ? Ok(token) : NotFound();
        }

        [HttpPatch("{uid}/status")]
        public async Task<IActionResult> UpdateTokenStatus(
            string uid,
            [FromBody] TokenStatusUpdateRequest request)
        {
            var token = await _context.Tokens.FindAsync(uid);
            if (token == null) return NotFound();

            token.Status = request.NewStatus;
            token.LastUpdated = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return NoContent();
        }
    }

    public class TokenStatusUpdateRequest
    {
        public TokenStatus NewStatus { get; set; }
    }

//Token yönetimi için API uç noktalarını sağlar.