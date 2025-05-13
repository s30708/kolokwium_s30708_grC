using System.Linq.Expressions;
using kolokwium_s30708_grC.Models;
using kolokwium_s30708_grC.Services;
using Microsoft.AspNetCore.Mvc;

namespace kolokwium_s30708_grC.Controllers;



[Route("api/[controller]")]
[ApiController]
public class DeliveryController : ControllerBase
{
    private IDeliveryService _deliveryService;

    public DeliveryController(IDeliveryService deliveryService)
    {
        _deliveryService = deliveryService;
    }


    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        var delivery = _deliveryService.GetDelivery(id);



        return Ok(delivery.Result);
    }

    /*[HttpPost]
    public IActionResult CreateDelivery([FromBody] DeliveryDTO deliveryDto)
    {
        
    }*/
    
}