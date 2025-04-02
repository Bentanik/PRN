using Grpc.Core;
using System.Text.Json;
using System.Text.Json.Serialization;
using VaccinaCare.gRPC.Protos.HealthGuide;
using VaccinaCare.Services;

namespace VaccinaCare.gRPC.Services;

public class HealthGuideService : HealthGuideGrpc.HealthGuideGrpcBase
{
    private readonly ILogger<HealthGuideService> _logger;
    private readonly IHealthGuidService _healthGuideService;
    public HealthGuideService(ILogger<HealthGuideService> logger, IHealthGuidService HealthGuideService)
    {
        _logger = logger;
        _healthGuideService = HealthGuideService;
    }

    //public override Task<HealthGuideList> GetAll(EmptyRequest request, ServerCallContext context)
    //{
    //    return Task.FromResult(items);
    //}
    public override async Task<HealthGuideList> GetAll(EmptyRequest request, ServerCallContext context)
    {
        try
        {
            var result = new HealthGuideList();
            var HealthGuides = await _healthGuideService.GetAll();

            var opt = new JsonSerializerOptions()
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault
            };

            var HealthGuideString = JsonSerializer.Serialize(HealthGuides, opt);

            var items = JsonSerializer.Deserialize<List<VaccinaCare.gRPC.Protos.HealthGuide.HealthGuide>>(HealthGuideString, opt);
            
            result.Data.AddRange(items);

            return await Task.FromResult(result);
        }
        catch (Exception ex)
        {
            return new HealthGuideList();
        }
    }

    public override async Task<VaccinaCare.gRPC.Protos.HealthGuide.HealthGuide> GetById(HealthGuideIdRequest request, ServerCallContext context)
    {
        try
        {
            var HealthGuide = await _healthGuideService.GetById(request.HealthGuideId);
            var opt = new JsonSerializerOptions()
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault
            };
            var HealthGuideString = JsonSerializer.Serialize(HealthGuide, opt);
            var item = JsonSerializer.Deserialize<VaccinaCare.gRPC.Protos.HealthGuide.HealthGuide>(HealthGuideString, opt);
            if (item != null)
            {
                return await Task.FromResult(item);
            }
            else
            {
                return new VaccinaCare.gRPC.Protos.HealthGuide.HealthGuide();
            }
        }
        catch (Exception ex)
        {
            return new VaccinaCare.gRPC.Protos.HealthGuide.HealthGuide();
        }
    }

    public override async Task<ActionResult> DeleteById(HealthGuideIdRequest request, ServerCallContext context)
    {
        try
        {
            var resultDelete = await _healthGuideService.Delete(request.HealthGuideId);

            return await Task.FromResult(new ActionResult
            {
                Status = 1,
                Message = "Delete success"
            });
        }
        catch (Exception ex)
        {
            return await Task.FromResult(new ActionResult
            {
                Status = -1,
                Message = string.Format("Delete fail. {0}", ex.ToString())
            });
        }
    }

    public override async Task<ActionResult> Add(VaccinaCare.gRPC.Protos.HealthGuide.HealthGuide request, ServerCallContext context)
    {
        try
        {
            if (request != null)
            {
                var opt = new JsonSerializerOptions()
                {
                    ReferenceHandler = ReferenceHandler.IgnoreCycles,
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault
                };
                var HealthGuideString = JsonSerializer.Serialize(request, opt);
                var item = JsonSerializer.Deserialize<VaccinaCare.Repositories.Models.HealthGuide>(HealthGuideString, opt);
                var result = await _healthGuideService.Create(item);
                if (result > 0)
                {
                    return await Task.FromResult(new ActionResult
                    {
                        Status = 1,
                        Message = "Add Success"
                    });
                }
                return await Task.FromResult(new ActionResult() { Status = -1, Message = "Add Fail" });

            }
            return await Task.FromResult(new ActionResult() { Status = -1, Message = "Add Fail" });

        }
        catch (Exception ex)
        {
            return await Task.FromResult(new ActionResult() { Status = -1, Message = "Add Fail" });

        }
    }

    public override async Task<ActionResult> Edit(VaccinaCare.gRPC.Protos.HealthGuide.HealthGuide request, ServerCallContext context)
    {
        try
        {
            if (request != null)
            {
                var opt = new JsonSerializerOptions()
                {
                    ReferenceHandler = ReferenceHandler.IgnoreCycles,
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault
                };
                var HealthGuideString = JsonSerializer.Serialize(request, opt);
                var item = JsonSerializer.Deserialize<VaccinaCare.Repositories.Models.HealthGuide>(HealthGuideString, opt);
                var result = await _healthGuideService.Update(item);
                if (result > 0)
                {
                    return await Task.FromResult(new ActionResult
                    {
                        Status = 1,
                        Message = "Update Success"
                    });
                }
                return await Task.FromResult(new ActionResult() { Status = -1, Message = "Update Fail" });
            }
            return await Task.FromResult(new ActionResult() { Status = -1, Message = "Edit Fail" });
        }
        catch (Exception ex)
        {
            return await Task.FromResult(new ActionResult() { Status = -1, Message = "Update Fail" });

        }

    }
}


