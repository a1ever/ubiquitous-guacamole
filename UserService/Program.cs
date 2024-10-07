using System.Data;
using FluentValidation;
using UserService.DomainService.Repositories.Abstractions;
using UserService.DomainService.Services.Abstractions;
using UserService.DomainService.Services.Implementations;
using UserService.GRPCServices;
using UserService.GRPCServices.Mapper;
using UserService.Repositories;
using Npgsql;
using UserService.DomainService.DTOs;
using UserService.DomainService.Mappings;
using UserService.DomainService.Validators;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpc();

builder.Services.AddScoped<IDbConnectionString, DbConnectionString>();
builder.Services.AddScoped<UserMapper>();
builder.Services.AddScoped<IValidator<UserRequestDto>, CreateUserRequestDtoValidator>();
builder.Services.AddScoped<IValidator<UserRequestDto>, UpdateUserRequestDtoValidator>();
builder.Services.AddScoped<IUserDomainService, UserDomainService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<Mapper>();

//var connection = new NpgsqlConnection("Host=localhost;");

var app = builder.Build();

app.MapGrpcService<UserServiceImpl>();

app.MapGet("/", () => "gRPC сервис работает. Используйте gRPC клиент для взаимодействия.");


await app.RunAsync("http://*:5002");