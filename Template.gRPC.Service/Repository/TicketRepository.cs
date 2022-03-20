using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Template.gRPC.Service.Repository
{
    public class TicketRepository
    {
        private readonly ILogger _logger;
        private int _availableTickets = 5;

        public TicketRepository(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<TicketRepository>();
        }

        public int GetAvailableTickets()
        {
            return _availableTickets;
        }

        public bool BuyTickets(string user, int count)
        {
            var updatedCount = _availableTickets - count;

            // Negative ticket count means there weren't enough available tickets
            if (updatedCount < 0)
            {
                _logger.LogError($"{user} failed to purchase tickets. Not enough available tickets.");
                return false;
            }

            _availableTickets = updatedCount;

            _logger.LogInformation($"{user} successfully purchased tickets.");
            return true;
        }
    }
}
