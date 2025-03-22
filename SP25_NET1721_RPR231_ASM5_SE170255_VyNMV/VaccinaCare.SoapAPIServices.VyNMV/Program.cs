using SoapCore;
using System.Text.Json.Serialization;
using VaccinaCare.Services;
using VaccinaCare.SoapAPIServices.VyNMV.SoapServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.Never;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add DI
builder.Services
    .AddScoped<IHealthGuidService, HealthGuidService>()
    .AddScoped<IHealthGuidCategoryService, HealthGuidCategoryService>()
    .AddScoped<IHealthGuideSoapService, HealthGuideSoapService>();

var app = builder.Build();

app.UseSoapEndpoint<IHealthGuideSoapService>("/healthGuideService.asmx", new SoapEncoderOptions());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();