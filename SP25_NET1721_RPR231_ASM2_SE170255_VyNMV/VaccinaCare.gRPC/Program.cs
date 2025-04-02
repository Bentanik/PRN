using VaccinaCare.gRPC.Protos.HealthGuide;
using VaccinaCare.gRPC.Protos.HealthGuideCategory;
using VaccinaCare.gRPC.Services;
using VaccinaCare.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddGrpc();

builder.Services.AddScoped<IUserService, UserService>()
                .AddScoped<IHealthGuidService, HealthGuidService>()
                .AddScoped<IHealthGuidCategoryService, HealthGuidCategoryService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapGrpcService<HealthGuideService>();
app.MapGrpcService<HealthGuideCategoryService>();

app.Run();
