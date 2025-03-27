using Common.Shared;
using MassTransit;
using MassTransit.Transports;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HealthGuide.Microservices.Controllers;

[ApiController]
[Route("[controller]")]
public class HealthGuideController 
    : ControllerBase
{
    private readonly List<BusinessObject.Shared.Models.HealthGuide> _healthGuides;
    private readonly ILogger<HealthGuideController> _logger;
    private readonly IBus _bus;
    public HealthGuideController(IBus bus, ILogger<HealthGuideController> logger)
    {
        _bus = bus;
        _logger = logger;

        _healthGuides = new List<BusinessObject.Shared.Models.HealthGuide>()
        {
            new BusinessObject.Shared.Models.HealthGuide
            {
                Title = "123",
                Author = "Nguyen Mai Viet Vy",
                Content = "213",
            },
            new BusinessObject.Shared.Models.HealthGuide
            {
                Title = "123",
                Author = "Nguyen Van A",
                Content = "213",
            },
            new BusinessObject.Shared.Models.HealthGuide
            {
                Title = "123",
                Author = "Nguyen Van B",
                Content = "213",
            }
        };
    }

    [HttpGet]
    public IEnumerable<BusinessObject.Shared.Models.HealthGuide> Get()
    {
        return _healthGuides;
    }

    [HttpGet("{id}")]
    public BusinessObject.Shared.Models.HealthGuide GetById(int id)
    {
        return _healthGuides.FirstOrDefault(x => x.Id == id);
    }

    [HttpPost]
    public async Task<IActionResult> Post(BusinessObject.Shared.Models.HealthGuide healthGuide)
    {
        if (healthGuide != null)
        {
            #region Business rule process anh/or call other API Service

            #endregion

            #region Publish data to Queue on RabbitMQ

            //Lets Queue as orderQueue.
            //Create a new URL ‘rabbitmq://localhost/orderQueue’
            //If orderQueue does not exist, RabbitMQ creates one
            Uri uri = new Uri("rabbitmq://localhost/healthGuideQueue");
            //Gets an endpoint to send the shared model object
            var endPoint = await _bus.GetSendEndpoint(uri);
            //Push the message to the queue
            await endPoint.Send(healthGuide);

            #endregion

            #region Logger

            //string messageLog = string.Format("[{0}] RECEIVE data from RabbitMQ.orderQueue: {1}", DateTime.Now.ToString(), JsonUtil.Serialize<object>(healthGuide));

            //JsonUtil.WriteToFile(@"C:\Users\vietv\Documents\New folder", messageLog);
            //_logger.LogInformation(messageLog);

            #endregion


            return Ok();
        }
        return BadRequest();
    }
}
