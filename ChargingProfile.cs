using OCPI.Net.Enums;
using System.ComponentModel.DataAnnotations;

namespace OCPI.Models;

    public class ChargingProfile
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public string SessionId { get; set; }

        [Required]
        public ChargingProfilePurposeType Purpose { get; set; }

        [Required]
        public ChargingRateUnit ChargingRateUnit { get; set; } = ChargingRateUnit.A;

        public List<ChargingSchedule> Schedules { get; set; } = new();
    }

    public class ChargingSchedule
    {
        public DateTime StartTime { get; set; }
        public decimal ChargingRate { get; set; } // Amper veya Watt cinsinden
        public int MinDuration { get; set; } // Dakika cinsinden
    }
    /*
     Güç sınırlama
    {
  "sessionId": "ses_456",
  "chargingRateUnit": "W",
  "schedules": [
    {
      "startTime": "2023-12-01T18:00:00Z",
      "chargingRate": 11000.0, // 11 kW sınır
      "minDuration": 60
    }
  ]
    */
}
