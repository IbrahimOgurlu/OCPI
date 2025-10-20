using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using OCPI.Data;
using OCPI.Services;
using OCPI.Clients;
using OCPI.Models;

// Web uygulamasını oluşturur.
var builder = WebApplication.CreateBuilder(args);



// API kontrolcülerini ekler.
builder.Services.AddControllers();
// Swagger/OpenAPI dokümantasyonu için API uç noktalarını keşfetmeye yarar.
builder.Services.AddEndpointsApiExplorer();
// Swagger (API dokümantasyon aracı) jenerasyonunu yapılandırır.
builder.Services.AddSwaggerGen(c => {
    c.SwaggerDoc("v1", new() { Title = "OCPI API", Version = "v1" });
});


// Connection String'i appsettings.json dosyasından "OcpiDbConnection" anahtarıyla alır.
builder.Services.AddDbContext<OcpiDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("OcpiDbConnection")));

// OCPI servislerini DI konteynerine ekler.

builder.Services.AddScoped<IOcpiService, OcpiService>();
// OCPI harici API'ler ile iletişim kurmak için HttpClient'ı yapılandırır.

builder.Services.AddHttpClient<IOcpiClient, OcpiClient>(client => {
    
    client.BaseAddress = new Uri(builder.Configuration["Ocpi:BaseUrl"]);
   
    client.DefaultRequestHeaders.Authorization =
        new System.Net.Http.Headers.AuthenticationHeaderValue(
            "Token",
            builder.Configuration["Ocpi:Token"]);
});

// Web uygulamasını oluşturur.
var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); 
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "OCPI v1")); // Swagger UI'ı etkinleştirir.
}

// HTTP isteklerini HTTPS'e yönlendirir.
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();