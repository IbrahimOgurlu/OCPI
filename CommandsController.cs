using Microsoft.AspNetCore.Mvc;
using OCPI.Net.Client;
using OCPI.Models;

namespace OCPI.Controllers;

    [Route("ocpi/2.2/commands")]
    [ApiController]
    public class CommandsController : ControllerBase
    {
        private readonly IOcpiClient _ocpiClient;
        private readonly OcpiDbContext _context;

        public CommandsController(IOcpiClient ocpiClient, OcpiDbContext context)
        {
            _ocpiClient = ocpiClient;
            _context = context;
        }

        [HttpPost("start-session")]
        public async Task<IActionResult> StartSession([FromBody] StartSessionCommand command)
        {
            var cmd = new Command
            {
                Type = CommandType.START_SESSION,
                LocationId = command.LocationId,
                ConnectorId = command.ConnectorId,
                Token = command.Token,
                Status = CommandStatus.PENDING
            };

            _context.Commands.Add(cmd);
            await _context.SaveChangesAsync();

            // OCPI platformuna gönder
            var response = await _ocpiClient.SendCommandAsync(
                CommandType.START_SESSION,
                new
                {
                    command.LocationId,
                    command.ConnectorId,
                    command.Token
                });

            return response.IsSuccess
                ? Accepted($"/commands/{cmd.Id}", cmd)
                : BadRequest(response.ErrorResponse);
        }

        [HttpPost("reserve-now")]
        public async Task<IActionResult> ReserveNow([FromBody] ReserveNowCommand command)
        {
            var cmd = new Command
            {
                Type = CommandType.RESERVE_NOW,
                LocationId = command.LocationId,
                EvseUid = command.EvseUid,
                ConnectorId = command.ConnectorId,
                Token = command.Token,
                ExpiryDate = command.ExpiryDate,
                ReservationId = Guid.NewGuid().ToString(),
                Status = CommandStatus.PENDING
            };

            _context.Commands.Add(cmd);
            await _context.SaveChangesAsync();

            var response = await _ocpiClient.SendCommandAsync(
                CommandType.RESERVE_NOW,
                new
                {
                    command.LocationId,
                    command.EvseUid,
                    command.ConnectorId,
                    command.Token,
                    command.ExpiryDate,
                    cmd.ReservationId
                });

            return response.IsSuccess
                ? Accepted($"/commands/{cmd.Id}", cmd)
                : BadRequest(response.ErrorResponse);
        }

        [HttpGet("{id}/status")]
        public async Task<IActionResult> GetCommandStatus(string id)
        {
            var cmd = await _context.Commands.FindAsync(id);
            return cmd != null
                ? Ok(new { cmd.Status, cmd.ExecutedAt })
                : NotFound();
        }
    }

    public class StartSessionCommand
    {
        public string LocationId { get; set; }
        public string ConnectorId { get; set; }
        public string Token { get; set; }
    }

    public class ReserveNowCommand
    {
        public string LocationId { get; set; }
        public string EvseUid { get; set; }
        public string ConnectorId { get; set; }
        public string Token { get; set; }
        public DateTime ExpiryDate { get; set; }
    }
// Bu controller, OCPI üzerinden gelen komutları işler.