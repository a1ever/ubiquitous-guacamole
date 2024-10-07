using System.Data;
using UserService.DomainService.Repositories.Abstractions;
using UserService.DomainService.Services.Abstractions;
using UserService.DomainService.Services.Implementations;
using UserService.GRPCServices;
using UserService.GRPCServices.Mapper;
using UserService.Repositories;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpc();

var connectionString = builder.Configuration.GetConnectionString("Host=127.0.0.1;Database=postgres;Username=postgres;Port=5432;Password=;");
builder.Services.AddScoped<IDbConnection>(sp => new NpgsqlConnection(connectionString));

builder.Services.AddScoped<IUserDomainService, UserDomainService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<Mapper>();

//var connection = new NpgsqlConnection("Host=localhost;");

var app = builder.Build();

app.MapGrpcService<UserServiceImpl>();

app.MapGet("/", () => "gRPC сервис работает. Используйте gRPC клиент для взаимодействия.");


await app.RunAsync("http://*:5002");