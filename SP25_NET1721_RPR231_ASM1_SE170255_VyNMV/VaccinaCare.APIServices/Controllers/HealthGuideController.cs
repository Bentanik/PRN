using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using VaccinaCare.Repositories.Models;
using VaccinaCare.Services;

namespace VaccinaCare.APIServices.Controllers;

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

    [Authorize(Roles = "1, 2")]
    [HttpGet]
    [EnableQuery]
    public async Task<IEnumerable<HealthGuide>> Get()
    {
        return await _healthGuidService.GetAll();
    }

    [Authorize(Roles = "1, 2")]
    [HttpGet("{id}")]
    public async Task<HealthGuide> Get(int id)
    {
        return await _healthGuidService.GetById(id);
    }

    [Authorize(Roles = "1, 2")]
    [HttpGet("{expertResponse}/{location}/{name}")]
    public async Task<IEnumerable<HealthGuide>> Get(string expertResponse, string location, string name)
    {
        return await _healthGuidService.Search(expertResponse, location, name);
    }

    [Authorize(Roles = "1, 2")]
    [HttpPost]
    public async Task<int> Post(HealthGuide healthGuide)
    {
        return await _healthGuidService.Create(healthGuide);
    }
    
    [Authorize(Roles = "1, 2")]
    [HttpPut("{id}")]
    public async Task<int> Put(HealthGuide healthGuide)
    {
        return await _healthGuidService.Update(healthGuide);
    }

    [Authorize(Roles = "1, 2")]
    [HttpDelete("{id}")]
    public async Task<bool> Delete(int id)
    {
        return await _healthGuidService.Delete(id);
    }
}
