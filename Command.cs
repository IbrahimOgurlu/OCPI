using OCPI.Net.Enums;
using System.ComponentModel.DataAnnotations;

namespace OCPI.Models;

    public class Command
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        public CommandType Type { get; set; }

        [Required]
        public string LocationId { get; set; }

        public string EvseUid { get; set; }
        public string ConnectorId { get; set; }

        [Required]
        public CommandStatus Status { get; set; } = CommandStatus.PENDING;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? ExecutedAt { get; set; }

        // Remote Start/Stop için
        public string SessionId { get; set; }
        public string Token { get; set; }

        // Reserve Now için
        public DateTime? ExpiryDate { get; set; }
        public string ReservationId { get; set; }
    }

    public enum CommandStatus
    {
        PENDING,
        EXECUTED,
        FAILED,
        CANCELLED
    }

//API üzerinden gelen komutlar(örneğin şarj başlatma/durdurma) işleyen controller. HTTP isteklerini alır, ilgili servislere yönlendirir.