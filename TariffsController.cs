using Microsoft.AspNetCore.Mvc;
using OCPI.Net.Client;
using OCPI.Models;

namespace OCPI.Controllers;

[Route("ocpi/2.2/tariffs")]
    [ApiController]
    public class TariffsController : ControllerBase
    {
        private readonly IOcpiClient _ocpiClient;
        private readonly OcpiDbContext _context;

        public TariffsController(IOcpiClient ocpiClient, OcpiDbContext context)
        {
            _ocpiClient = ocpiClient;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var tariffs = await _context.Tariffs.ToListAsync();
            return Ok(tariffs);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Tariff tariff)
        {
            _context.Tariffs.Add(tariff);
            await _context.SaveChangesAsync();

            // OCPI tarafına da senkronize et
            var response = await _ocpiClient.PushTariffAsync(tariff);

            return response.IsSuccess
                ? CreatedAtAction(nameof(GetAll), tariff)
                : BadRequest(response.ErrorResponse);
        }
    }

//API’den tarife bilgilerini yöneten controller.