using Microsoft.AspNetCore.Mvc;
using OCPI.Net.Client;
using OCPI.Models;

namespace OCPI.Controllers;

    [Route("ocpi/2.2/cdrs")]
    [ApiController]
    public class CdrsController : ControllerBase
    {
        private readonly IOcpiClient _ocpiClient;
        private readonly OcpiDbContext _context;

        public CdrsController(IOcpiClient ocpiClient, OcpiDbContext context)
        {
            _ocpiClient = ocpiClient;
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCdr([FromBody] Cdr cdr)
        {
            _context.Cdrs.Add(cdr);
            await _context.SaveChangesAsync();

            // OCPI platformuna gönder
            var response = await _ocpiClient.PostCdrAsync(cdr);

            return response.IsSuccess
                ? CreatedAtAction(nameof(GetCdr), new { id = cdr.Id }, cdr)
                : BadRequest(response.ErrorResponse);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCdr(string id)
        {
            var cdr = await _context.Cdrs.FindAsync(id);
            return cdr != null ? Ok(cdr) : NotFound();
        }
    }
//API üzerinden CDR verilerini yöneten controller.