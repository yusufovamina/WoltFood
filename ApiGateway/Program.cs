using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

// ��������� ������������ Ocelot
builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);

// ������������ Ocelot � ���������� ������������
builder.Services.AddOcelot();

var app = builder.Build();

// ���������� Ocelot Middleware
await app.UseOcelot();

app.Run();
