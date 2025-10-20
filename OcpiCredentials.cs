using System.ComponentModel.DataAnnotations;

namespace OCPI.Services;

    public class OcpiCredentials
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public string Token { get; set; }  // OCPI platform tokenı

        [Required]
        public string Url { get; set; }    // Karşı tarafın OCPI endpointi

        [Required]
        public string PartyId { get; set; } // Örn: "TUR"

        [Required]
        public string CountryCode { get; set; } // Örn: "TR"

        public DateTime LastUpdated { get; set; } = DateTime.UtcNow;

        public CredentialsStatus Status { get; set; } = CredentialsStatus.PENDING;
    }

    public enum CredentialsStatus
    {
        PENDING,
        ACTIVE,
        SUSPENDED,
        EXPIRED
    }

//OCPI protokolünde kimlik bilgilerini(token, endpoint URL, vb.) tutan model sınıfı. 