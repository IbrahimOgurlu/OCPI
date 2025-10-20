using System;
using System.ComponentModel.DataAnnotations;

namespace   OCPI.Models;

    public class EnergyData
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public string LocationId { get; set; }

        [Required]
        public string EvseUid { get; set; }

        [Required]
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        [Required]
        public decimal EnergyDelivered { get; set; } // kWh cinsinden

        public decimal? CurrentPower { get; set; } // kW cinsinden (anlık güç)

        [Required]
        public EnergySource Source { get; set; }
    }

    public enum EnergySource
    {
        GRID,       // Şebeke
        SOLAR,      // Güneş
        WIND,       // Rüzgar
        BATTERY,    // Depolama
        OTHER       // Diğer
    }
//Enerji ölçümlerini temsil eden model sınıfı.

