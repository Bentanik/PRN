using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using PE_PRN231_SP25_TrialTest_SE170255_BE.Repository.Models;
using PE_PRN231_SP25_TrialTest_SE170255_BE.Service;

namespace PE_PRN231_SP25_TrialTest_SE170255_BE.APIService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CosmeticInformationController
    (ICosmeticInformationService cosmeticInformationService)
    : ControllerBase
{
    [Authorize(Roles = "1, 3, 4")]
    [HttpGet]
    [EnableQuery]
    public async Task<IEnumerable<CosmeticInformation>> Get()
    {
        return await cosmeticInformationService.GetAll();
    }

    // GET: CosmeticInformationController/Details/5
    [Authorize(Roles = "1, 3, 4")]
    [HttpGet("{id}")]
    public async Task<CosmeticInformation> Get(string id)
    {
        return await cosmeticInformationService.GetById(id);
    }

    // GET api/<TestAnswerController>/5
    [Authorize(Roles = "1, 3, 4")]
    [HttpGet("{name}/{size}/{skinType}")]
    public async Task<IEnumerable<CosmeticInformation>> Get(string name, string size, string skinType)
    {
        return await cosmeticInformationService.Search(name, size, skinType);
    }

    // GET: CosmeticInformationController/Create
    [Authorize(Roles = "1")]
    [HttpPost]
    public async Task<int> Post(CosmeticInformation main)
    {
        return await cosmeticInformationService.Create(main);
    }

    // PUT api/<TestAnswerController>/5
    [Authorize(Roles = "1")]
    [HttpPut("{id}")]
    public async Task<int> Put(CosmeticInformation main)
    {
        return await cosmeticInformationService.Update(main);
    }

    // DELETE api/<TestAnswerController>/5
    [Authorize(Roles = "1")]
    [HttpDelete("{id}")]
    public async Task<bool> Delete(string id)
    {
        return await cosmeticInformationService.Delete(id);
    }
}
