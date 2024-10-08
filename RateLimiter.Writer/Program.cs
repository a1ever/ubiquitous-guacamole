using RateLimiter.Writer.GRPCServices;
using RateLimiter.Writer.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructure();

builder.Services.AddDomainServices();

builder.Services.AddGrpc();

var app = builder.Build();

// Маршрутизация gRPC
app.MapGrpcService<WriterImpl>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client.");

await app.RunAsync("http://*:5001");