using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grpc.Core;
using Template.gRPC.Service;

namespace Template.gRPC.Client.ApplicationServices
{
    public class RacerApplicationService
    {
        private readonly TimeSpan RaceDuration = TimeSpan.FromSeconds(30);

        private readonly RacerService.RacerServiceClient _client;

        public RacerApplicationService(RacerService.RacerServiceClient client)
        {
            _client = client;
        }

        public async Task Menu()
        {
            Console.WriteLine($"Race duration: {RaceDuration.TotalSeconds} seconds");
            Console.WriteLine("Press any key to start race...");
            Console.ReadKey();

            await BidirectionalStreamingExample();

            Console.WriteLine("Finished");
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

        public async Task BidirectionalStreamingExample()
        {
            var headers = new Metadata { new Metadata.Entry("race-duration", RaceDuration.ToString()) };

            Console.WriteLine("Ready, set, go!");
            using var call = _client.ReadySetGo(new CallOptions(headers));
            var complete = false;

            // Read incoming messages in a background task
            RaceMessage? lastMessageReceived = null;
            var readTask = Task.Run(async () =>
            {
                await foreach (var message in call.ResponseStream.ReadAllAsync())
                {
                    lastMessageReceived = message;
                }
            });

            // Write outgoing messages until timer is complete
            var sw = Stopwatch.StartNew();
            var sent = 0;

            #region Reporting
            // Report requests in realtime
            var reportTask = Task.Run(async () =>
            {
                while (true)
                {
                    Console.WriteLine($"Messages sent: {sent}");
                    Console.WriteLine($"Messages received: {lastMessageReceived?.Count ?? 0}");

                    if (!complete)
                    {
                        await Task.Delay(TimeSpan.FromSeconds(1));
                        Console.SetCursorPosition(0, Console.CursorTop - 2);
                    }
                    else
                    {
                        break;
                    }
                }
            });
            #endregion

            while (sw.Elapsed < RaceDuration)
            {
                await call.RequestStream.WriteAsync(new RaceMessage { Count = ++sent });
            }

            // Finish call and report results
            await call.RequestStream.CompleteAsync();
            await readTask;

            complete = true;
            await reportTask;
        }
    }
}
