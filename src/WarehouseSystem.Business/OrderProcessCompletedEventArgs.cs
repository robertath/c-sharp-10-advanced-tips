using WarehouseSystem.Domain;

namespace WarehouseSystem.Business
{
    public class OrderProcessCompletedEventArgs
    {
        public Order? Order { get; set; }
    }
}