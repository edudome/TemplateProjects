using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grpc.Core;
using Template.gRPC.Service;

namespace Template.gRPC.Client.ApplicationServices
{
    public class MailApplicationService
    {
        MailService.MailServiceClient _client;

        public MailApplicationService(MailService.MailServiceClient client)
        {
            _client = client;
        }

        public async Task Main(string mailName)
        {
            var mailboxName = GetMailboxName(mailName);

            using (var call = _client.Mailbox(headers: new Metadata { new Metadata.Entry("mailbox-name", mailboxName) }))
            {
                var responseTask = Task.Run(async () =>
                {
                    await foreach (var message in call.ResponseStream.ReadAllAsync())
                    {
                        Console.ForegroundColor = message.Reason == MailboxMessage.Types.Reason.Received ? ConsoleColor.White : ConsoleColor.Green;
                        Console.WriteLine();
                        Console.WriteLine(message.Reason == MailboxMessage.Types.Reason.Received ? "Mail received" : "Mail forwarded");
                        Console.WriteLine($"New mail: {message.New}, Forwarded mail: {message.Forwarded}");
                        Console.ResetColor();
                    }
                });

                while (true)
                {
                    var result = Console.ReadKey(intercept: true);
                    if (result.Key == ConsoleKey.Escape)
                    {
                        break;
                    }

                    await call.RequestStream.WriteAsync(new ForwardMailMessage());
                }

                Console.WriteLine("Disconnecting");
                await call.RequestStream.CompleteAsync();
                await responseTask;
            }

            Console.WriteLine("Disconnected. Press any key to exit.");
            Console.ReadKey();
        }

        public string GetMailboxName(string mailName)
        {
            if (string.IsNullOrEmpty(mailName))
            {
                Console.WriteLine("No mailbox name provided. Using default name. Usage: dotnet run <name>.");
                return "DefaultMailbox";
            }

            return mailName;
        }
    }
}
