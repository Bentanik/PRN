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

    [OperationContract]
    Task<int> Create(HealthGuide healthGuide);

    [OperationContract]
    Task<int> Update(HealthGuide healthGuide);

    [OperationContract]
    Task<bool> Delete(int id);

    [OperationContract]
    Task<List<HealthGuideCategory>> GetHealthGuideCategories();
}

public class HealthGuideSoapService : IHealthGuideSoapService
{
    private readonly IHealthGuidService _healthGuideService;
    private readonly IHealthGuidCategoryService _healthGuideCategoryService;

    public HealthGuideSoapService(IHealthGuidService healthGuideService, IHealthGuidCategoryService healthGuideCategoryService)
    {
        _healthGuideService = healthGuideService;
        _healthGuideCategoryService = healthGuideCategoryService;
    }

    public async Task<int> Create(HealthGuide healthGuide)
    {
        try
        {
            // Serialize the HealthGuide object into a JSON string with proper options
            var opt = new JsonSerializerOptions()
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            };
            var healthGuideString = JsonSerializer.Serialize(healthGuide, opt);

            // Optionally, you could convert the serialized string back into a different type if needed
            var item = JsonSerializer.Deserialize<VaccinaCare.Repositories.Models.HealthGuide>(healthGuideString, opt);

            // Call the service to create the HealthGuide
            var result = await _healthGuideService.Create(item);
            return result;
        }
        catch (Exception ex)
        {
            // Log the exception (if logging is available) or handle the error
            return -1; // Return a failure code
        }
    }

    public async Task<bool> Delete(int id)
    {
        try
        {
            // Call the service to delete a HealthGuide by ID
            var result = await _healthGuideService.Delete(id);
            return result;
        }
        catch (Exception ex)
        {
            // Log the exception or handle it as needed
            return false; // Return false to indicate failure
        }
    }

    public async Task<List<HealthGuide>> GetAll()
    {
        try
        {
            var items = await _healthGuideService.GetAll();

            // Serialize and deserialize the data to ensure proper structure
            var opt = new JsonSerializerOptions()
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            };
            var itemsString = JsonSerializer.Serialize(items, opt);
            var result = JsonSerializer.Deserialize<List<HealthGuide>>(itemsString, opt);

            return result;
        }
        catch (Exception ex)
        {
            // Log the exception (if needed) and return an empty list on failure
            return new List<HealthGuide>(); // Return empty list on failure
        }
    }

    public async Task<HealthGuide> GetById(int id)
    {
        try
        {
            var item = await _healthGuideService.GetById(id);

            // Serialize and deserialize the HealthGuide to ensure proper structure
            var opt = new JsonSerializerOptions()
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            };
            var itemString = JsonSerializer.Serialize(item, opt);
            var result = JsonSerializer.Deserialize<HealthGuide>(itemString, opt);

            return result;
        }
        catch (Exception ex)
        {
            // Log or handle the exception and return a default HealthGuide object on failure
            return new HealthGuide(); // Return default HealthGuide object on failure
        }
    }

    public async Task<List<HealthGuideCategory>> GetHealthGuideCategories()
    {
        try
        {
            var categories = await _healthGuideCategoryService.GetAll();

            // Serialize and deserialize the categories list to ensure proper structure
            var opt = new JsonSerializerOptions()
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            };
            var categoriesString = JsonSerializer.Serialize(categories, opt);
            var result = JsonSerializer.Deserialize<List<HealthGuideCategory>>(categoriesString, opt);

            return result;
        }
        catch (Exception ex)
        {
            // Log the exception or handle it as needed
            return new List<HealthGuideCategory>(); // Return empty list on failure
        }
    }

    public async Task<int> Update(HealthGuide healthGuide)
    {
        try
        {
            // Serialize the HealthGuide object into a JSON string with proper options
            var opt = new JsonSerializerOptions()
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            };
            var healthGuideString = JsonSerializer.Serialize(healthGuide, opt);

            // Optionally, you could convert the serialized string back into a different type if needed
            var item = JsonSerializer.Deserialize<VaccinaCare.Repositories.Models.HealthGuide>(healthGuideString, opt);

            // Call the service to update the HealthGuide
            var result = await _healthGuideService.Update(item);
            return result;
        }
        catch (Exception ex)
        {
            // Log the exception or handle it as needed
            return -1; // Indicate failure
        }
    }
}
