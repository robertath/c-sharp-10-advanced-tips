using WarehouseSystem.Domain;

namespace WarehouseSystem
{
    public class OrderList
    {
        private readonly Order[] orders;

        public Order this[int index] => orders[index];

        public Order Find(Guid orderId) =>
            orders.First(order => order.OrderNumber == orderId);

        public OrderList(IEnumerable<Order> orders)
        {
            this.orders = orders.ToArray(); ;
        }
    }
}
