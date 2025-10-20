using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;

namespace OCPI.Clients
{
    public class OcpiClient : IOcpiClient
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public OcpiClient(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<OcpiResponse<T>> GetAsync<T>(string endpoint)
        {
            try
            {
                var response = await _httpClient.GetAsync(endpoint);
                return await ProcessResponse<T>(response);
            }
            catch (Exception ex)
            {
                return new OcpiResponse<T> { IsSuccess = false, Error = ex.Message };
            }
        }

        public async Task<OcpiResponse<T>> PostAsync<T>(string endpoint, object data)
        {
            try
            {
                var json = JsonSerializer.Serialize(data);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync(endpoint, content);
                return await ProcessResponse<T>(response);
            }
            catch (Exception ex)
            {
                return new OcpiResponse<T> { IsSuccess = false, Error = ex.Message };
            }
        }

        private async Task<OcpiResponse<T>> ProcessResponse<T>(HttpResponseMessage response)
        {
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                return new OcpiResponse<T> { IsSuccess = false, Error = content, StatusCode = response.StatusCode };
            }

            return new OcpiResponse<T>
            {
                IsSuccess = true,
                Data = JsonSerializer.Deserialize<T>(content),
                StatusCode = response.StatusCode
            };
        }
    }

    public class OcpiResponse<T>
    {
        public bool IsSuccess { get; set; }
        public T Data { get; set; }
        public string Error { get; set; }
        public System.Net.HttpStatusCode StatusCode { get; set; }
    }
}

//OCPI modülleriyle haberleşir, veri çeker veya gönderir.

