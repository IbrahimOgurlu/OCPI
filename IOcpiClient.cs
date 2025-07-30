using System;

namespace WattReise.Clients;

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

