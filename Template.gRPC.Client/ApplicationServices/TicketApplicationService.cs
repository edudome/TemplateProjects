using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Template.gRPC.Service;

namespace Template.gRPC.Client.ApplicationServices
{
    public class TicketApplicationService
    {
        TicketService.TicketServiceClient _client;

        public TicketApplicationService(TicketService.TicketServiceClient client)
        {
            _client = client;
        }

        private const string Address = "https://localhost:5001";

        public async Task Menu()
        {
            Console.WriteLine("gRPC Ticketer");
            Console.WriteLine();
            Console.WriteLine("Press a key:");
            Console.WriteLine("1: Get available tickets");
            Console.WriteLine("2: Purchase ticket");
            Console.WriteLine("3: Authenticate");
            Console.WriteLine("4: Exit");
            Console.WriteLine();

            string? token = null;

            var exiting = false;
            while (!exiting)
            {
                var consoleKeyInfo = Console.ReadKey(intercept: true);
                switch (consoleKeyInfo.KeyChar)
                {
                    case '1':
                        await GetAvailableTickets();
                        break;
                    case '2':
                        await PurchaseTicket(token);
                        break;
                    case '3':
                        token = await Authenticate();
                        break;
                    case '4':
                        exiting = true;
                        break;
                }
            }

            Console.WriteLine("Exiting");
        }

        public async Task<string> Authenticate()
        {
            try
            {
                Console.WriteLine($"Authenticating as {Environment.UserName}...");
                using var httpClient = new HttpClient();
                using var request = new HttpRequestMessage
                {
                    RequestUri = new Uri($"{Address}/generateJwtToken?name={HttpUtility.UrlEncode(Environment.UserName)}"),
                    Method = HttpMethod.Get,
                    Version = new Version(2, 0)
                };
                using var tokenResponse = await httpClient.SendAsync(request);
                tokenResponse.EnsureSuccessStatusCode();

                var token = await tokenResponse.Content.ReadAsStringAsync();
                Console.WriteLine("Successfully authenticated.");
                Console.WriteLine("Token:");
                Console.WriteLine(token);

                return token;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error authenticating." + Environment.NewLine + ex.ToString());
                return await Task.FromResult(ex.Message);
            }
        }

        public async Task PurchaseTicket(string? token)
        {
            Console.WriteLine("Purchasing ticket...");
            try
            {
                Metadata? headers = null;
                if (token != null)
                {
                    headers = new Metadata();
                    headers.Add("Authorization", $"Bearer {token}");
                }

                var response = await _client.BuyTicketsAsync(new BuyTicketsRequest { Count = 1 }, headers);
                if (response.Success)
                {
                    Console.WriteLine("Purchase successful.");
                }
                else
                {
                    Console.WriteLine("Purchase failed. No tickets available.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error purchasing ticket." + Environment.NewLine + ex.ToString());
            }
        }

        public async Task GetAvailableTickets()
        {
            Console.WriteLine("Getting available ticket count...");
            var response = await _client.GetAvailableTicketsAsync(new Empty());
            Console.WriteLine("Available ticket count: " + response.Count);
        }
    }
}
