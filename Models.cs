namespace OCPI.Models
{
    public class OcpiResponse
    {
        public bool IsSuccess { get; set; }
        public string Error { get; set; }
    }

    public class OcpiResponse<T> : OcpiResponse
    {
        public T Data { get; set; }
    }

    public class Cdr
    {
        public string Id { get; set; }
        public string SessionId { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }

    public class OcpiCdr
    {
        public string Id { get; set; }
        public string LocationId { get; set; }
    }

    public class OcpiToken
    {
        public string Uid { get; set; }
        public string Type { get; set; }
    }

    public enum CommandType
    {
        Start,
        Stop,
        Reserve
    }

    public class CommandResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
    }

    public class Location
    {
        public string Id { get; set; }
        public List<Evse> Evses { get; set; }
    }

    public class Evse
    {
        public string Id { get; set; }
        public List<Connector> Connectors { get; set; }
    }

    public class Connector
    {
        public string Id { get; set; }
    }

    public class Session
    {
        public string Id { get; set; }
        public string LocationId { get; set; }
        public string ConnectorId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string Status { get; set; }
    }

    public class SessionRequest
    {
        public string LocationId { get; set; }
        public string ConnectorId { get; set; }
    }

    public class Tariff
    {
        public string Id { get; set; }
        public string Currency { get; set; }
        public decimal Price { get; set; }
    }

    public class OcpiTariff
    {
        public string Id { get; set; }
        public decimal Price { get; set; }
    }
}