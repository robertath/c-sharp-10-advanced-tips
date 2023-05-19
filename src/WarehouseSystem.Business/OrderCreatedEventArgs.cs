using WarehouseSystem.Domain;

namespace WarehouseSystem.Business
{
    public class OrderCreatedEventArgs : EventArgs
    {
        public Order Order { get; init; }
        public decimal OldTotal { get; set; }
        public decimal NewTotal { get; set; }

        public OrderCreatedEventArgs(Order order)
        {
            Order = order;
        }
    }
}
