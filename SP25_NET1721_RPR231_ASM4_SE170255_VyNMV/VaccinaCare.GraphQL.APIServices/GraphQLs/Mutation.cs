using Microsoft.AspNetCore.Mvc;
using VaccinaCare.Repositories.Models;
using VaccinaCare.Services;

namespace VaccinaCare.GraphQL.APIServices.GraphQLs;

public class Mutation
{
    private readonly IHealthGuidService _service;

    public Mutation(IHealthGuidService healthGuidService)
    {
        _service = healthGuidService;
    }

    // POST api/<TestAnswerController>
    //[Authorize(Roles = "3")]
    [HttpPost]
    public async Task<int> AddHealthGuide(HealthGuide healthGuid)
    {
        return await _service.Create(healthGuid);
    }

    // PUT api/<TestAnswerController>/5
    //[Authorize(Roles = "3")]
    [HttpPut("{id}")]
    public async Task<int> UpdateHealthGuide(HealthGuide healthGuid)
    {
        return await _service.Update(healthGuid);
    }

    // DELETE api/<TestAnswerController>/5
    //[Authorize(Roles = "3")]
    [HttpDelete("{id}")]
    public async Task<bool> DeleteHealthGuide(int id)
    {
        return await _service.Delete(id);
    }
}
