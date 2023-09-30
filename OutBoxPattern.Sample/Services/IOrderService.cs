using OutBoxPattern.Sample.Models;

namespace OutBoxPattern.Sample.Services;

public interface IOrderService
{
    Task<Order> AddOrder(Order order);
}