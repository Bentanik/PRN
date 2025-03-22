using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PE_PRN231_SP25_TrialTest_SE170255_BE.Repository.Models;
using PE_PRN231_SP25_TrialTest_SE170255_BE.Service;

namespace PE_PRN231_SP25_TrialTest_SE170255_BE.APIService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CosmeticCategoriesController : ControllerBase
{
    private readonly ICosmeticCategoryService _service;

    public CosmeticCategoriesController(ICosmeticCategoryService service)
    {
        _service = service;
    }

    // GET: api/CosmeticCategories
    [Authorize(Roles = "1")]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CosmeticCategory>>> GetCosmeticCategories()
    {
        return await _service.GetAll();
    }
}