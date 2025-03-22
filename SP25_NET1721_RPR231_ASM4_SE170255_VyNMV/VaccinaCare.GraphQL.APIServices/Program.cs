using System.Text.Json.Serialization;
using VaccinaCare.GraphQL.APIServices.GraphQLs;
using VaccinaCare.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// GraphQL
builder.Services.AddGraphQLServer().AddQueryType<Query>().AddMutationType<Mutation>().BindRuntimeType<DateTime, DateTimeType>();
builder.Services.AddScoped<IHealthGuidService, HealthGuidService>();

// Fix Json Cycle
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.Never;
});

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

//CORS
app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

// Use GraphQL Playground
app.UseRouting().UseEndpoints(endpoints => { endpoints.MapGraphQL(); });

app.Run();
