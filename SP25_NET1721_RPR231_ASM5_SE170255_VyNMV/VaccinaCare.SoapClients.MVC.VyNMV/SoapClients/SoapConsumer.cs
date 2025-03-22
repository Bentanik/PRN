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

    public async Task<HealthGuide[]> GetHealthGuides()
    {
        try
        {
            var result = await _healthGuideSoapService.GetAllAsync();
            return result;
        }
        catch (Exception ex)
        {
        }
        return new HealthGuide[] { new HealthGuide() };
    }

    public async Task<HealthGuide> GetHealthGuide(int id)
    {
        try
        {
            var result = await _healthGuideSoapService.GetByIdAsync(id);
            return result;
        }
        catch (Exception ex)
        {
        }

        return new HealthGuide();
    }
}
