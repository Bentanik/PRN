using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VaccinaCare.Repositories.Models;
using VaccinaCare.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VaccinaCare.Blazor.Api.APIServices.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class HealthGuideCategoryController : ControllerBase
{
    private readonly IHealthGuidCategoryService _service;
    public HealthGuideCategoryController(IHealthGuidCategoryService service)
    {
        _service = service;
    }

    // GET: api/<ScheduleTypeController>
    [HttpGet]
    public async Task<IEnumerable<HealthGuideCategory>> Get()
    {
        return await _service.GetAll();
    }
}
