[ApiController]
[Route("api/[controller]")]
public class LocationsController : ControllerBase
{
    private readonly IOcpiServices _svc;
    public LocationsController(IOcpiServices svc) => _svc = svc;

    /// <summary>
    /// Tüm lokasyonları döner.
    /// </summary>
    /// <returns>Lokasyon listesi</returns>
    [HttpGet]
    [ProducesResponseType(typeof(List<Location>), 200)]
    public async Task<IActionResult> GetAll() => Ok(await _svc.GetLocationsAsync());

    /// <summary>
    /// ID'ye göre tek lokasyonu getirir.
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(Location), 200)]
    public async Task<IActionResult> Get(string id) => Ok(await _svc.GetLocationByIdAsync(id));

    var loc = new Location
    {
        Id = "LOC1",
        Evses = new List<Evse> {
        new Evse {
            Id = "EVSE1",
            Connectors = new List<Connector> { new Connector { Id = "C1" } }
        }
    }
    };
    db.Locations.Add(loc);

        [
  {
    "id": "LOC1",
    "evses": [
      {
        "id": "EVSE1",
        "connectors": [
          { "id": "C1" }
        ]
      }
    ]
  }
]
}