using Microsoft.AspNetCore.Mvc;
using OCPI.Data;
using OCPI.Models;

namespace OCPI.Controllers;

    [Route("ocpi/2.2/energy")]
    [ApiController]
    public class EnergyController : ControllerBase
    {
        private readonly OcpiDbContext _context;

        public EnergyController(OcpiDbContext context)
        {
            _context = context;
        }

        [HttpPost("data")]
        public async Task<IActionResult> PostEnergyData([FromBody] EnergyData data)
        {
            _context.EnergyData.Add(data);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetEnergyData), new { id = data.Id }, data);
        }
        //Toplam Enerji Hesaplama:
        [HttpGet("{locationId}/total")]
        public IActionResult GetTotalEnergy(string locationId)
        {
            var total = _context.EnergyData
                .Where(e => e.LocationId == locationId)
                .Sum(e => e.EnergyDelivered);

            return Ok(new { TotalEnergy = total });
        }

        [HttpGet("{locationId}")]
        public IActionResult GetEnergyData(string locationId)
        {
            var data = _context.EnergyData
                .Where(e => e.LocationId == locationId)
                .OrderByDescending(e => e.Timestamp)
                .ToList();

            return Ok(data);
        }
        /*
         Örnek kullanım
        POST /ocpi/2.2/energy/data
Content-Type: application/json

{
  "locationId": "loc_123",
  "evseUid": "evse_456",
  "energyDelivered": 24.5,
  "currentPower": 7.2,
  "source": "SOLAR"
        */
}
}

//Enerji verilerini yönetir (şarj istasyonu tüketim bilgileri vb.).