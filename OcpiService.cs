using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OCPI.Data;
using OCPI.Models;

namespace OCPI.Services
{
    public class OcpiServices : IOcpiServices
    {
        private readonly OcpiDbContext _context;

        public OcpiServices(OcpiDbContext context)
        {
            _context = context;
        }

        public async Task<List<Location>> GetLocationsAsync()
        {
            return await _context.Locations
                .Include(l => l.Evses)
                .ThenInclude(e => e.Connectors)
                .ToListAsync();
        }

        public async Task<Location> GetLocationByIdAsync(string locationId)
        {
            return await _context.Locations
                .Include(l => l.Evses)
                .ThenInclude(e => e.Connectors)
                .FirstOrDefaultAsync(l => l.Id == locationId);
        }

        public async Task<Session> StartSessionAsync(SessionRequest request)
        {
            var session = new Session
            {
                Id = Guid.NewGuid().ToString(),
                LocationId = request.LocationId,
                ConnectorId = request.ConnectorId,
                StartTime = DateTime.UtcNow,
                Status = "ACTIVE"
            };

            _context.Sessions.Add(session);
            await _context.SaveChangesAsync();
            return session;
        }

        public async Task<Session> StopSessionAsync(string sessionId)
        {
            var session = await _context.Sessions.FirstOrDefaultAsync(s => s.Id == sessionId);
            if (session == null) return null;

            session.EndTime = DateTime.UtcNow;
            session.Status = "COMPLETED";
            _context.Sessions.Update(session);
            await _context.SaveChangesAsync();

            return session;
        }
    }
}

//OCPI modülleriyle haberleşir, veri çeker veya gönderir.