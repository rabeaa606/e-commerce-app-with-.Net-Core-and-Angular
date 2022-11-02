using Address = Core.Entites.OrderAggregate.Address;

namespace Core.interfaces;
public interface IOrderService
{
    Task<Order> CreateOrderAsync(string buyerEmail, int delieveryMethod, string basketId, Address shippingAddress);
    Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail);
    Task<Order> GetOrderByIdAsync(int id, string buyerEmail);
    Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync();
}
