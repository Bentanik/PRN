using System.Text.Json.Serialization;
using System.Text.Json;
using VaccinaCareWCFReferences;

namespace VaccinaCare.SoapClients.MVC.VyNMV.SoapClients;

public class SoapConsumer
{
    private readonly IHealthGuideSoapService _healthGuideSoapService;

    public SoapConsumer()
    {
        _healthGuideSoapService = new HealthGuideSoapServiceClient
            (HealthGuideSoapServiceClient.EndpointConfiguration.BasicHttpBinding_IHealthGuideSoapService);
    }

    // Get all HealthGuides
    public async Task<HealthGuide[]> GetHealthGuides()
    {
        try
        {
            var result = await _healthGuideSoapService.GetAllAsync();
            return result;
        }
        catch (Exception ex)
        {
            // Log exception
        }
        return new HealthGuide[] { new HealthGuide() };
    }

    // Get a specific HealthGuide by id
    public async Task<HealthGuide> GetHealthGuide(int id)
    {
        try
        {
            var result = await _healthGuideSoapService.GetByIdAsync(id);
            return result;
        }
        catch (Exception ex)
        {
            // Log exception
        }

        return new HealthGuide();
    }

    // Create a new HealthGuide
    public async Task<int> CreateHealthGuide(HealthGuide healthGuide)
    {
        try
        {
            var result = await _healthGuideSoapService.CreateAsync(healthGuide);
            return result; // Assuming CreateAsync returns an int, like the ID of the created health guide
        }
        catch (Exception ex)
        {
            // Log exception
        }

        return -1; // Indicate failure by returning -1
    }

    // Update an existing HealthGuide
    public async Task<int> UpdateHealthGuide(HealthGuide healthGuide)
    {
        try
        {
            var result = await _healthGuideSoapService.UpdateAsync(healthGuide);
            return result; // Assuming UpdateAsync returns an int, like the number of updated rows
        }
        catch (Exception ex)
        {
            // Log exception
        }

        return -1; // Indicate failure by returning -1
    }

    // Delete a HealthGuide by id
    public async Task<bool> DeleteHealthGuide(int id)
    {
        try
        {
            var result = await _healthGuideSoapService.DeleteAsync(id);
            return result; // Assuming DeleteAsync returns a bool indicating success or failure
        }
        catch (Exception ex)
        {
            // Log exception
        }

        return false; // Indicate failure by returning false
    }

    public async Task<List<HealthGuideCategory>> GetHealthGuideCategories()
    {
        try
        {
            var categories = await _healthGuideSoapService.GetHealthGuideCategoriesAsync(); // Assuming GetAllAsync fetches the categories

            // Serialize and deserialize the categories list to ensure proper structure
            var opt = new JsonSerializerOptions()
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            };

            var categoriesString = JsonSerializer.Serialize(categories, opt);
            var result = JsonSerializer.Deserialize<List<HealthGuideCategory>>(categoriesString, opt);

            return result ?? new List<HealthGuideCategory>(); // Return empty list if deserialization fails
        }
        catch (Exception ex)
        {
            // Log the exception or handle it as needed
        }

        return new List<HealthGuideCategory>(); // Return empty list on failure
    }
}
