[Route("ocpi/2.2/chargingprofiles")]
[ApiController]
namespace OCPI.Controllers;
public class ChargingProfilesController : ControllerBase
{
    private readonly OcpiDbContext _context;

    [HttpPost]
    public async Task<IActionResult> SetProfile([FromBody] ChargingProfile profile)
    {
        _context.ChargingProfiles.Add(profile);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetProfile), new { id = profile.Id }, profile);
    }

    [HttpGet("{sessionId}")]
    public IActionResult GetProfile(string sessionId)
    {
        var profile = _context.ChargingProfiles
            .FirstOrDefault(cp => cp.SessionId == sessionId);

        return profile != null ? Ok(profile) : NotFound();
    }
}