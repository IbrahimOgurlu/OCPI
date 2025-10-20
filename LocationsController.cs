using Microsoft.AspNetCore.Mvc; 
using System.Collections.Generic; 
using System.Threading.Tasks; 
using OCPI.Models; 
using OCPI.Services; 

namespace OCPI.Controllers 
{
    [Route("ocpi/locations")] 
    [ApiController] 
    public class LocationsController : ControllerBase 
    {
        private readonly IOcpiService _ocpiService; 

        public LocationsController(IOcpiService ocpiService)
        {
            _ocpiService = ocpiService;
        }

        /// <summary>
        /// Tüm OCPI konumlarını (Locations) getirir.
        /// </summary>
        [HttpGet] // Bu metodun HTTP GET isteklerine yanıt vereceğini belirtir.
        public async Task<ActionResult<IEnumerable<Location>>> GetLocations()
        {
            
            var locations = await _ocpiService.GetLocationsAsync();
            
            return Ok(locations);
        }

        /// <summary>
        /// Yeni bir OCPI oturumu (Session) başlatır.
        /// </summary>
        /// <param name="request">Oturum başlatma isteğini içeren veri modeli.</param>
        [HttpPost("sessions")] // Bu metodun HTTP POST isteklerine yanıt vereceğini ve yolunu "sessions" olarak belirtir.
        public async Task<ActionResult<Session>> StartSession([FromBody] SessionRequest request)
        {
            
            var session = await _ocpiService.StartSessionAsync(request);
            
            return CreatedAtAction(nameof(StartSession), session);
        }

        
    }
}