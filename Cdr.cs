using OCPI.Net.Enums;
using System.ComponentModel.DataAnnotations;

namespace OCPI.Models;

    public class Cdr
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        public string SessionId { get; set; }

        [Required]
        public DateTime StartDateTime { get; set; }

        [Required]
        public DateTime EndDateTime { get; set; }

        [Required]
        public decimal TotalEnergy { get; set; } // kWh

        [Required]
        public decimal TotalCost { get; set; }

        [Required]
        public string Currency { get; set; } = "EUR";

        public CdrStatus Status { get; set; } = CdrStatus.PENDING;

        [Required]
        public string LocationId { get; set; }

        public string MeterId { get; set; }

        public List<CdrToken> Tokens { get; set; } = new();
    }

    public class CdrToken
    {
        public string Uid { get; set; }
        public TokenType Type { get; set; }
        public string ContractId { get; set; }
    }

//Bir şarj oturumunun detaylarını (başlangıç, bitiş, tüketilen enerji, maliyet vb.) tutan model sınıfı.
