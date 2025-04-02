using Microsoft.AspNetCore.Authentication.Cookies;
using VaccinaCare.gRPC.Clients.Protos.HealthGuide;
using VaccinaCare.gRPC.Clients.Protos.HealthGuideCategory;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Config Grpc
string grpcServerUrl = builder.Configuration["GrpcServerUrl"] ?? "https://localhost:5050";

builder.Services.AddGrpcClient<HealthGuideGrpc.HealthGuideGrpcClient>(o =>
{
    o.Address = new Uri(grpcServerUrl);
});
builder.Services.AddGrpcClient<HealthGuideCategoryGrpc.HealthGuideCategoryGrpcClient> (o =>
{
    o.Address = new Uri(grpcServerUrl);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();
