using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

var builder = WebApplication.CreateBuilder(args);

//  InMemoryDatabase
builder.Services.AddDbContext<OcpiDbContext>(opt => opt.UseInMemoryDatabase("OcpiDemoDb"));

//  Servis ekleme
builder.Services.AddScoped<IOcpiServices, OcpiServices>();

//  Controllers ve Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => c.SwaggerDoc("v1", new OpenApiInfo { Title = "OCPI Demo API", Version = "v1" }));

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

//  Seed verisi ekle
SeedDatabase(app);

app.Run();

void SeedDatabase(WebApplication app)
{
    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<OcpiDbContext>();

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
    db.Sessions.Add(new Session
    {
        Id = "S1",
        LocationId = "LOC1",
        ConnectorId = "C1",
        StartTime = DateTime.UtcNow,
        Status = "ACTIVE"
    });

    db.SaveChanges();
}

//  MODELLER
public class Location { public string Id { get; set; } public List<Evse> Evses { get; set; } = new(); }
public class Evse { public string Id { get; set; } public List<Connector> Connectors { get; set; } = new(); }
public class Connector { public string Id { get; set; } }
public class Session { public string Id { get; set; } public string LocationId { get; set; } public string ConnectorId { get; set; } public DateTime StartTime { get; set; } public DateTime? EndTime { get; set; } public string Status { get; set; } }
public class SessionRequest { public string LocationId { get; set; } public string ConnectorId { get; set; } }

//  DbContext
public class OcpiDbContext : DbContext
{
    public OcpiDbContext(DbContextOptions<OcpiDbContext> options) : base(options) { }
    public DbSet<Location> Locations { get; set; }
    public DbSet<Session> Sessions { get; set; }
}

//  Servis
public interface IOcpiServices
{
    Task<List<Location>> GetLocationsAsync();
    Task<Location> GetLocationByIdAsync(string id);
    Task<Session> StartSessionAsync(SessionRequest request);
    Task<Session> StopSessionAsync(string sessionId);
}

public class OcpiServices : IOcpiServices
{
    private readonly OcpiDbContext _db;
    public OcpiServices(OcpiDbContext db) => _db = db;

    public async Task<List<Location>> GetLocationsAsync() =>
        await _db.Locations.Include(l => l.Evses).ThenInclude(e => e.Connectors).ToListAsync();

    public async Task<Location> GetLocationByIdAsync(string id) =>
        await _db.Locations.Include(l => l.Evses).ThenInclude(e => e.Connectors)
                           .FirstOrDefaultAsync(l => l.Id == id);

    public async Task<Session> StartSessionAsync(SessionRequest req)
    {
        var session = new Session
        {
            Id = Guid.NewGuid().ToString(),
            LocationId = req.LocationId,
            ConnectorId = req.ConnectorId,
            StartTime = DateTime.UtcNow,
            Status = "ACTIVE"
        };
        _db.Sessions.Add(session);
        await _db.SaveChangesAsync();
        return session;
    }

    public async Task<Session> StopSessionAsync(string id)
    {
        var s = await _db.Sessions.FirstOrDefaultAsync(x => x.Id == id);
        if (s == null) return null;
        s.Status = "COMPLETED";
        s.EndTime = DateTime.UtcNow;
        await _db.SaveChangesAsync();
        return s;
    }
}

// Controllers
[ApiController]
[Route("api/[controller]")]
public class LocationsController : ControllerBase
{
    private readonly IOcpiServices _svc;
    public LocationsController(IOcpiServices svc) => _svc = svc;

    [HttpGet] public async Task<IActionResult> GetAll() => Ok(await _svc.GetLocationsAsync());
    [HttpGet("{id}")] public async Task<IActionResult> Get(string id) => Ok(await _svc.GetLocationByIdAsync(id));
}

[ApiController]
[Route("api/[controller]")]
public class SessionsController : ControllerBase
{
    private readonly IOcpiServices _svc;
    public SessionsController(IOcpiServices svc) => _svc = svc;

    [HttpPost("start")] public async Task<IActionResult> Start(SessionRequest req) => Ok(await _svc.StartSessionAsync(req));
    [HttpPost("stop/{id}")] public async Task<IActionResult> Stop(string id) => Ok(await _svc.StopSessionAsync(id));


}

