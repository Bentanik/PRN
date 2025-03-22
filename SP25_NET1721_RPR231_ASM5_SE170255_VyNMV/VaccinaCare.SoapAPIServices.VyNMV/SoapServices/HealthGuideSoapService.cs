using System.ServiceModel;
using System.Text.Json;
using System.Text.Json.Serialization;
using VaccinaCare.Services;
using VaccinaCare.SoapAPIServices.VyNMV.SoapModels;

namespace VaccinaCare.SoapAPIServices.VyNMV.SoapServices;

[ServiceContract]
public interface IHealthGuideSoapService
{
    [OperationContract]
    Task<List<HealthGuide>> GetAll();
    [OperationContract]
    Task<HealthGuide> GetById(int id);
    //[OperationContract]
    //Task<int> Create(HealthGuide healthGuide);
    //[OperationContract]
    //Task<int> Update(HealthGuide healthGuide);
    //[OperationContract]
    //Task<bool> Delete(int id);
    //[OperationContract]
    //Task<List<HealthGuideCategory>> GetHealthGuideCategories();
}


public class HealthGuideSoapService 
    (IHealthGuidService healthGuideService, IHealthGuidCategoryService healthGuideCategoryService)
    : IHealthGuideSoapService
{
    public async Task<List<HealthGuide>> GetAll()
    {
        try
        {
            var items = await healthGuideService.GetAll();

            var opt = new JsonSerializerOptions() { ReferenceHandler = ReferenceHandler.IgnoreCycles, DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull };
            var itemsString = JsonSerializer.Serialize(items, opt);
            var result = JsonSerializer.Deserialize<List<HealthGuide>>(itemsString, opt);

            return result;
        }
        catch (Exception ex)
        {
            return new List<HealthGuide>();
        }
    }

    public async Task<HealthGuide> GetById(int id)
    {
        try
        {
            var item = await healthGuideService.GetById(id);

            var opt = new JsonSerializerOptions() { ReferenceHandler = ReferenceHandler.IgnoreCycles, DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull };
            var itemsString = JsonSerializer.Serialize(item, opt);
            var result = JsonSerializer.Deserialize<HealthGuide>(itemsString, opt);

            return result;
        }
        catch (Exception ex)
        {
            return new HealthGuide();
        }
    }
}
