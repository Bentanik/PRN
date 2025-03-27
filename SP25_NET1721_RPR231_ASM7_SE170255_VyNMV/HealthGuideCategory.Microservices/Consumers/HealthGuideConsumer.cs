using MassTransit.Transports;
using MassTransit;
using BusinessObject.Shared.Models;
using Common.Shared;

namespace HealthGuideCategory.Microservices.Consumers;

public class HealthGuideConsumer : IConsumer<BusinessObject.Shared.Models.HealthGuide>
{
    private readonly ILogger _logger;

    public HealthGuideConsumer(ILogger<HealthGuideConsumer> logger)
    {
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<BusinessObject.Shared.Models.HealthGuide> context)
    {
        //https://localhost:7199/gateway/order

        #region Receive data from Queue on RabbitMQ            

        var data = context.Message;
        Console.WriteLine(context.Message);

        #endregion

        #region Business rule process anh/or call other API Service

        //Validate the Data
        //Store to Database
        //Notify the user via Email / SMS

        #endregion

        #region Logger

        #endregion

    }
}

