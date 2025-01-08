using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Добавляем конфигурацию Ocelot
builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);

// Регистрируем Ocelot в контейнере зависимостей
builder.Services.AddOcelot();

var app = builder.Build();

// Используем Ocelot Middleware
await app.UseOcelot();

app.Run();
