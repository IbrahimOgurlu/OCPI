using OCPI.Net.Enums;
using System.ComponentModel.DataAnnotations;

namespace OCPI.Models;

public class OcpiToken
    {
        [Key]
        public string Uid { get; set; } // Token'un unique ID'si

        [Required]
        public TokenType Type { get; set; } // RFID, NFC, APP vs.

        [Required]
        public string AuthId { get; set; } // Fiziksel token ID

        [Required]
        public TokenStatus Status { get; set; } = TokenStatus.PENDING;

        public string VisualNumber { get; set; } // Kart üzerindeki numara
        public string Issuer { get; set; } // Token'ı basan kurum

        [Required]
        public bool Valid { get; set; } = true;

        public DateTime? LastUpdated { get; set; } = DateTime.UtcNow;

        // Whitelist bilgileri
        public WhitelistType Whitelist { get; set; } = WhitelistType.ALWAYS;
        public DateTime? WhitelistExpires { get; set; }

        // Token'ın bağlı olduğu kontrat
        public string ContractId { get; set; }
        public string ContractName { get; set; }
    }

    public enum TokenStatus
    {
        PENDING,
        ACTIVE,
        BLOCKED,
        EXPIRED,
        DELETED
    }

//OCPI protokolünde kullanılan token bilgisini tutar.
