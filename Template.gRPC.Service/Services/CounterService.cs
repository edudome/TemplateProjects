using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Template.gRPC.Service.Models;
using static Template.gRPC.Service.CounterService;

namespace Template.gRPC.Service.Services
{
    public class CounterService : CounterServiceBase
    {
        private readonly ILogger _logger;
        private readonly IncrementingCounter _counter;

        public CounterService(IncrementingCounter counter, ILoggerFactory loggerFactory)
        {
            _counter = counter;
            _logger = loggerFactory.CreateLogger<CounterService>();
        }

        public override async Task<CounterReply> AccumulateCountAsync(IAsyncStreamReader<CounterRequest> requestStream, ServerCallContext context)
        {
            await foreach (var message in requestStream.ReadAllAsync())
            {
                _logger.LogInformation($"Acumulando valor por: {message.Count}");

                _counter.Increment(message.Count);
            }

            return await Task.FromResult(new CounterReply { Count = _counter.Count });
        }

        public override async Task CountdownAsync(Empty request, IServerStreamWriter<CounterReply> responseStream, ServerCallContext context)
        {
            for (var i = _counter.Count; i >= 0; i--)
            {
                await responseStream.WriteAsync(new CounterReply { Count = i });
                await Task.Delay(TimeSpan.FromSeconds(2));
            }
        }

        public override async Task<CounterReply> SetEmpyCounterAsync(Empty request, ServerCallContext context)
        {
            _logger.LogInformation("Setting Counter to 0");
            _counter.SetEmpty();
            return await Task.FromResult(new CounterReply { Count = _counter.Count });
        }
    }
}
