using Microsoft.AspNetCore.Mvc;
using OutBoxPattern.Sample.Models;
using OutBoxPattern.Sample.Services;

namespace OutBoxPattern.Sample.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderController: ControllerBase
{
    private readonly IOrderService _orderService;
    private readonly IMailService _mailService;
    private readonly IEmailOutbox _emailOutbox;

    public OrderController(IOrderService orderService, IMailService mailService, IEmailOutbox emailOutbox)
    {
        _orderService = orderService;
        _mailService = mailService;
        _emailOutbox = emailOutbox;
    }
    
    [HttpPost]
    public async Task<IActionResult> Post(Order order)
    {
        var result = await _orderService.AddOrder(order);
        if (result == null)
        {
            return BadRequest();
        }
        
        // Send email if order store in the database
        var send = _mailService.Send(result.Email, "Order is completed", "Your order has been saved in the database", false);
        if (!send)
        {
            // store in the email outbox
            EmailOutbox emailOutbox = new EmailOutbox
            {
                OrderId = result.Id,
                Success = false
            };
            var outbox = await _emailOutbox.Add(emailOutbox);
            return Ok(outbox);
        }
        return Ok(result);
    }
}