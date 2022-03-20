using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grpc.Core;
using Template.gRPC.Service;

namespace Template.gRPC.Client.ApplicationServices
{
    public class CompressorApplicationService
    {
        CompressorService.CompressorServiceClient _client;

        public CompressorApplicationService(CompressorService.CompressorServiceClient client)
        {
            _client = client;
        }

        public async Task UnaryCallExample()
        {
            // 'grpc-internal-encoding-request' is a special metadata value that tells
            // the client to compress the request.
            // This metadata is only used in the client is not sent as a header to the server.
            var metadata = new Metadata();
            metadata.Add("grpc-internal-encoding-request", "gzip");

            var reply = await _client.SayHelloAsync(new HelloRequest { Name = "CompressorClient" }, headers: metadata);
            Console.WriteLine("Greeting: " + reply.Message);
        }
    }
}
