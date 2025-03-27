using Common.Shared;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HealthGuideCategory.Microservices.Controllers;

[ApiController]
[Route("[controller]")]
public class HealthGuideCategoryController
    : ControllerBase
{
    private readonly List<BusinessObject.Shared.Models.HealthGuideCategory> _healthGuideCategories;
    private readonly ILogger<HealthGuideCategoryController> _logger;
    private readonly IBus _bus;
    public HealthGuideCategoryController(IBus bus, ILogger<HealthGuideCategoryController> logger)
    {
        _bus = bus;
        _logger = logger;

        _healthGuideCategories = new List<BusinessObject.Shared.Models.HealthGuideCategory>()
        {
            new BusinessObject.Shared.Models.HealthGuideCategory
            {
                Name = "A",
                Description = "123",
            },
            new BusinessObject.Shared.Models.HealthGuideCategory
            {
                Name = "B",
                Description = "123",

            },
            new BusinessObject.Shared.Models.HealthGuideCategory
            {
                 Name = "C",
                 Description = "123",
            }
        };
    }

    [HttpGet]
    public IEnumerable<BusinessObject.Shared.Models.HealthGuideCategory> Get()
    {
        return _healthGuideCategories;
    }

    [HttpGet("{id}")]
    public BusinessObject.Shared.Models.HealthGuideCategory GetById(int id)
    {
        return _healthGuideCategories.FirstOrDefault(x => x.Id == id);
    }
}
