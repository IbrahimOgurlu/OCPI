using Microsoft.AspNetCore.Mvc;
using OCPI.Net.Client;
using OCPI.Data;
using OCPI.Models;

namespace OCPI.Controllers;

    [Route("ocpi/2.2/credentials")]
    [ApiController]
    public class CredentialsController : ControllerBase
    {
        private readonly OcpiDbContext _context;
        private readonly IOcpiClient _ocpiClient;

        public CredentialsController(OcpiDbContext context, IOcpiClient ocpiClient)
        {
            _context = context;
            _ocpiClient = ocpiClient;
        }

        // OCPI Platformuna Credentials Gönderme
        [HttpPost]
        public async Task<IActionResult> PostCredentials([FromBody] OcpiCredentials credentials)
        {
            _context.Credentials.Add(credentials);
            await _context.SaveChangesAsync();

            var response = await _ocpiClient.PostCredentialsAsync(credentials.Url, new
            {
                credentials.Token,
                credentials.PartyId,
                credentials.CountryCode
            });

            return response.IsSuccess
                ? CreatedAtAction(nameof(GetCredentials), new { id = credentials.Id }, credentials)
                : BadRequest(response.ErrorResponse);
        }

        // Credentials Sorgulama
        [HttpGet("{id}")]
        public IActionResult GetCredentials(Guid id)
        {
            var creds = _context.Credentials.Find(id);
            return creds != null ? Ok(creds) : NotFound();
        }
    }

// Kimlik doğrulama ve yetkilendirme sürecini yönetir.