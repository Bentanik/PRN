using Grpc.Core;
using System.Text.Json.Serialization;
using System.Text.Json;
using VaccinaCare.gRPC.Protos.HealthGuideCategory;
using VaccinaCare.Services;

namespace VaccinaCare.gRPC.Services;

public class HealthGuideCategoryService : HealthGuideCategoryGrpc.HealthGuideCategoryGrpcBase
{
    private readonly ILogger<HealthGuideService> _logger;
    private readonly IHealthGuidCategoryService _healthGuideCategoryService;
    public HealthGuideCategoryService(ILogger<HealthGuideService> logger, IHealthGuidCategoryService HealthGuideCategoryService)
    {
        _logger = logger;
        _healthGuideCategoryService = HealthGuideCategoryService;
    }

    public override async Task<HealthGuideCategoryList> GetAll(Protos.HealthGuideCategory.EmptyRequest request, ServerCallContext context)
    {
        try
        {
            var result = new HealthGuideCategoryList();
            var HealthGuideCategorys = await _healthGuideCategoryService.GetAll();

            var opt = new JsonSerializerOptions()
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault
            };

            var HealthGuideCategoryString = JsonSerializer.Serialize(HealthGuideCategorys, opt);

            var items = JsonSerializer.Deserialize<List<VaccinaCare.gRPC.Protos.HealthGuideCategory.HealthGuideCategory>>(HealthGuideCategoryString, opt);

            result.Data.AddRange(items);

            return await Task.FromResult(result);
        }
        catch (Exception ex)
        {
            return new HealthGuideCategoryList();
        }
    }
}
