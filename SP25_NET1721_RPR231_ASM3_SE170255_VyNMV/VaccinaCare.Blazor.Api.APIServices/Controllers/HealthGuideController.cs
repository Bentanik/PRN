using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VaccinaCare.Repositories.Models;
using VaccinaCare.Services;

namespace VaccinaCare.Blazor.Api.APIServices.Controllers;

[Route("api/[controller]")]
[ApiController]
public class HealthGuideController : ControllerBase
{
    private readonly IConfiguration _config;
    private readonly IHealthGuidService _healthGuidService;

    public HealthGuideController(IConfiguration config, IHealthGuidService userService)
    {
        _config = config;
        _healthGuidService = userService;
    }

    [HttpGet]
    public async Task<IEnumerable<HealthGuide>> Get()
    {
        return await _healthGuidService.GetAll();
    }

    [HttpGet("{id}")]
    public async Task<HealthGuide> Get(int id)
    {
        return await _healthGuidService.GetById(id);
    }

    [HttpGet("{expertResponse}/{location}/{name}")]
    public async Task<IEnumerable<HealthGuide>> Get(string expertResponse, string location, string name)
    {
        return await _healthGuidService.Search(expertResponse, location, name);
    }

    [HttpPost]
    public async Task<int> Post(HealthGuide schedule)
    {
        return await _healthGuidService.Create(schedule);
    }

    [HttpPut("{id}")]
    public async Task<int> Put(HealthGuide schedule)
    {
        return await _healthGuidService.Update(schedule);
    }

    [HttpDelete("{id}")]
    public async Task<bool> Delete(int id)
    {
        return await _healthGuidService.Delete(id);
    }
}
