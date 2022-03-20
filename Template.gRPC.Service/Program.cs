using Template.gRPC.Service.Models;
using Template.gRPC.Service.Repository;
using Template.gRPC.Service.Services;

var builder = WebApplication.CreateBuilder(args);

// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

// Add services to the container.
builder.Services.AddGrpc(options => options.EnableDetailedErrors = true);
builder.Services.AddSingleton<IncrementingCounter>();
builder.Services.AddScoped<MailQueueRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<BlogService>();
app.MapGrpcService<CompressorService>();
app.MapGrpcService<CounterService>();
app.MapGrpcService<MailService>();
app.MapGrpcService<RacerService>();
app.MapGrpcService<TicketService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run(); // Si falla acá por tema de compatibilidad o algo así, probar ejecutando en PowerShell con "dotnet run template.grpc.service"

