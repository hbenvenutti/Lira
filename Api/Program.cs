using Lira.Bootstrap;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.ConfigureApi();

builder.Services.ConfigureApi(configuration: builder.Configuration);

var app = builder.Build();

app.ConfigureApi(environment: app.Environment);

app.ConfigureControllers();

app.Run();
