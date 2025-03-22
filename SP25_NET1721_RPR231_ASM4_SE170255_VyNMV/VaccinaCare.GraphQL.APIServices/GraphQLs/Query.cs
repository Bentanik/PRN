using VaccinaCare.Repositories.Models;
using VaccinaCare.Services;

namespace VaccinaCare.GraphQL.APIServices.GraphQLs;

public class Query
{
    private readonly IHealthGuidService _service;
    public Query(IHealthGuidService service)
    {
        _service = service;
    }

    public async Task<List<HealthGuide>> GetHealthGuids()
    {
        try
        {
            var result = await _service.GetAll();
            return result;
        }
        catch (Exception ex)
        {
            return new List<HealthGuide>();
        }
    }

    public async Task<HealthGuide> GetHealthGuid(string id)
    {
        try
        {
            var result = await _service.GetById(int.Parse(id));
            return result;
        }
        catch (Exception ex)
        {
            return new HealthGuide();
        }
    }
}
