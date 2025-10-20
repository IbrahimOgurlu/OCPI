using OCPI.Net.Enums;
using System.ComponentModel.DataAnnotations;

namespace OCPI.Models;

    public class Tariff
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        public string Currency { get; set; } = "EUR";

        public List<TariffElement> Elements { get; set; } = new();
        public DateTime LastUpdated { get; set; } = DateTime.UtcNow;
    }

    public class TariffElement
    {
        public TariffElementType Type { get; set; }
        public decimal Price { get; set; }
        public decimal? StepSize { get; set; } // kWh cinsinden
    }

//Tarifeler (fiyatlandırma) için model sınıfı.

