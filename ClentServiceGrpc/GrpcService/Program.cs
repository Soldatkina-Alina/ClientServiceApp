using GrpcService;
using GrpcService.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpc();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<GreeterService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

var service = new GreeterService(null);
var users = service.GetListUsers(new Google.Protobuf.WellKnownTypes.Empty(), null);
var r = users;

app.Run();
