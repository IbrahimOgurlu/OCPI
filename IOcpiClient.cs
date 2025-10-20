using System;

namespace OCPI.Clients;

public interface IOcpiTariffService
    {
        Task<OcpiResponse<List<OcpiTariff>>> GetTariffsAsync();
        Task<OcpiResponse> PushTariffAsync(Tariff tariff);
    }

    public interface IOcpiClient
    {
        Task<OcpiResponse<T>> PostAsync<T>(string endpoint, object data);
        Task<OcpiResponse<T>> GetAsync<T>(string endpoint);
    }

//OCPI istemci. OCPI ile ilgili operasyonların tanımlandığı interface’lerdir.