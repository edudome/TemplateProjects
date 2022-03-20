using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Template.gRPC.Service;

namespace Template.gRPC.Client.ApplicationServices
{
    public class CounterApplicationService
    {
        private readonly Random Random = new Random();

        private readonly CounterService.CounterServiceClient _client;

        public CounterApplicationService(CounterService.CounterServiceClient client)
        {
            _client = client;
        }

        public async Task ClientStreamingCallExample()
        {
            using var call = _client.AccumulateCountAsync();
            for (var i = 1; i <= 5; i++)
            {
                Console.WriteLine($"Enviando valor {i}");
                await call.RequestStream.WriteAsync(new CounterRequest { Count = i });
                await Task.Delay(TimeSpan.FromSeconds(2));
            }

            await call.RequestStream.CompleteAsync();

            var response = await call;
            Console.WriteLine($"Termina con valor: {response.Count}");
        }

        public async Task ServerStreamingCallExample()
        {
            var cts = new CancellationTokenSource(TimeSpan.FromSeconds(60));

            using var call = _client.CountdownAsync(new Empty(), cancellationToken: cts.Token);

            try
            {
                await foreach (var message in call.ResponseStream.ReadAllAsync(cancellationToken: cts.Token))
                {
                    Console.WriteLine($"Countdown: {message.Count}");
                }
            }
            catch (RpcException ex) when (ex.StatusCode == StatusCode.Cancelled)
            {
                Console.WriteLine("Stream cancelled.");
            }
        }

        public async Task SetCounterEmpty()
        {
            var response = _client.SetEmpyCounterAsync(new Empty());
            Console.WriteLine($"Counter set to {response.Count}.");
            await Task.FromResult(response);
        }
    }
}
