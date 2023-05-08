using GrpcServer;
using GrpcServer.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpc();
builder.Services.AddGrpcReflection(); // To be able to add into postman

var app = builder.Build();
MethodTimeLogger.Logger = app.Logger;

app.MapGrpcService<DataService>();
app.MapGrpcService<PriceService>();

app.MapGrpcReflectionService(); // To be able to add into postman
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();