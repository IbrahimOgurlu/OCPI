using System;
using System.Collections.Generic; 
using System.Threading.Tasks; 
using OCPI.Models; // Location, Session, SessionRequest için

namespace OCPI.Services;
public interface IOcpiCdrServices
    {
        Task<OcpiResponse> SendCdrAsync(Cdr cdr);
        Task<OcpiResponse<OcpiCdr>> GetCdrsAsync(DateTimeOffset? from = null);
    }

    public interface IOcpiCommandServices
    {
        Task<OcpiResponse> SendCommandAsync(CommandType commandType, object payload);
        Task<OcpiResponse<CommandResult>> GetCommandResultAsync(string commandId);
    }

    public interface IOcpiTokenServices
    {
        Task<OcpiResponse> PutTokenAsync(string uid, OcpiToken token);
        Task<OcpiResponse<OcpiToken>> GetTokenAsync(string uid);
        Task<OcpiResponse> PatchTokenAsync(string uid, object updates);
    }

    public interface IOcpiServices
    {
        Task<List<Location>> GetLocationsAsync();
        Task<Location> GetLocationByIdAsync(string locationId);
        Task<Session> StartSessionAsync(SessionRequest request);
        Task<Session> StopSessionAsync(string sessionId);
    }

// servis arayüzleri. OCPI ile ilgili operasyonların tanımlandığı interface’lerdir.OCPI ile ilgili operasyonların tanımlandığı interface’lerdir.