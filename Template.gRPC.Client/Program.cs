
using Grpc.Net.Client;
using Template.gRPC.Client.ApplicationServices;
using Template.gRPC.Service;

using var channel = GrpcChannel.ForAddress("https://localhost:7032");
var clientBlog = new BlogService.BlogServiceClient(channel);
var clientCompressor = new CompressorService.CompressorServiceClient(channel);
var clientCounter = new CounterService.CounterServiceClient(channel);
var clientMail = new MailService.MailServiceClient(channel);
var clientRacer = new RacerService.RacerServiceClient(channel);
var clientTicket = new TicketService.TicketServiceClient(channel);

// ###################################################################################### //

var blobApplicationService = new BlogApplicationService(clientBlog);
await blobApplicationService.ListBlog();
Console.WriteLine();
Console.WriteLine("Press any key to continue...");
Console.ReadKey();

// ###################################################################################### //

//var compressorApplicationService = new CompressorApplicationService(clientCompressor);
//await compressorApplicationService.UnaryCallExample();
//Console.WriteLine();
//Console.WriteLine("Press any key to continue...");
//Console.ReadKey();

// ###################################################################################### //

//var counterApplicationService = new CounterApplicationService(clientCounter);
//await counterApplicationService.ClientStreamingCallExample();
//await counterApplicationService.ServerStreamingCallExample();
//await counterApplicationService.SetCounterEmpty();
//Console.WriteLine();
//Console.WriteLine();
//Console.WriteLine("Press any key to continue...");
//Console.ReadKey();

// ###################################################################################### //

//var mailApplicationService = new MailApplicationService(clientMail);
//await mailApplicationService.Main("edudome9@gmail.com");
//Console.WriteLine();
//Console.WriteLine();
//Console.WriteLine("Press any key to continue...");
//Console.ReadKey();

// ###################################################################################### //

//var racerApplicationService = new RacerApplicationService(clientRacer);
//await racerApplicationService.Menu();
//Console.WriteLine();
//Console.WriteLine();
//Console.WriteLine("Press any key to continue...");
//Console.ReadKey();

// ###################################################################################### //

//// Necesita implementarse la API que devuelve el token.
//var ticketApplicationService = new TicketApplicationService(clientTicket);
//await ticketApplicationService.Menu();
//Console.WriteLine();
//Console.WriteLine();
//Console.WriteLine("Press any key to continue...");
//Console.ReadKey();

// ###################################################################################### //

Console.WriteLine("Press any key to exit...");
Console.ReadKey();
