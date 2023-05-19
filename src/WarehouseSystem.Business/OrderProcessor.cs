using System.ComponentModel.DataAnnotations;
using WarehouseSystem.Domain;

namespace WarehouseSystem.Business;

public class OrderProcessor
{
    public Func<Order, bool>? OrOrderInitialized { get; set; }

    public event EventHandler<OrderCreatedEventArgs>? OrderCreated;
    public event EventHandler<OrderProcessCompletedEventArgs>? OrderProcessCompleted;

    protected virtual void OnOrderCreated(OrderCreatedEventArgs args)
    {
        OrderCreated?.Invoke(this, args);
    }

    protected virtual void OnOrderProcessCompleted(OrderProcessCompletedEventArgs args)
    {
        OrderProcessCompleted?.Invoke(this, args);
    }

    #region Nullables
    private void Initialized(Order order)
    {
        ArgumentNullException.ThrowIfNull(order);
        if (OrOrderInitialized?.Invoke(order) == false)
        {
            throw new ArgumentException($"Couldn't initialize {order.OrderNumber}");
        }
    }

    public void Process(Order order)
    {
        Initialized(order);

        OnOrderCreated(new(order)
        {
            OldTotal = 100,
            NewTotal = 80
        });

        OnOrderProcessCompleted(new() { Order = order });
    }


    public void Process(Order order, decimal discount)
    {
        Initialized(order);

        OnOrderCreated(new(order)
        {
            OldTotal = 100,
            NewTotal = 100 - discount
        });
    }

    /// <summary>
    /// Using Tuples to return IEnumerable<OrderExtensions.GenerateReport> 
    /// </summary>
    /// <param name="orders"></param>
    /// <returns>IEnumerable<OrderExtensions.GenerateReport></returns>
    /// /// equal to: public ValueTuple<Guid,int, decimal, IEnumerable<Item>> Process(IEnumerable<Order> orders)
    public IEnumerable<(Guid orderNumber,
            int amountOfItems,
            decimal total,
            IEnumerable<Item> items)> Process(IEnumerable<Order> orders)
    {
        var summaries = orders.Select(order =>
        {
            return
            (
                Order: order.OrderNumber,
                Items: order.LineItems.Count(),
                Total: order.LineItems.Sum(item => item.Price),
                LineItems: order.LineItems
            );
        });

        return summaries.OrderBy(summary => summary.Total);
    }

    /// <summary>
    /// Using Switch Expression
    /// </summary>
    /// <param name="order"></param>
    /// <returns></returns>
    private decimal CalculateFreightCost(Order order)
        => order.ShippingProvider switch
        {
            SwedishPostalServiceShippingProvider
            { DeliverNextDay: true }
            provider => provider.FreightCost + 50m,

            SwedishPostalServiceShippingProvider
            provider => provider.FreightCost - 50m,

            var provider => provider?.FreightCost ?? 50m
        };
    #endregion



    #region Tuples & Pattern Matching
    ////private void Initialized(Order order)
    ////{
    ////    ArgumentNullException.ThrowIfNull(order);
    ////    if (OrOrderInitialized?.Invoke(order) == false)
    ////    {
    ////        throw new Exception($"Couldn't initialize {order.OrderNumber}");
    ////    }
    ////}

    ////public void Process(Order order,
    ////    Action<Order> onCompleted = default)
    ////{
    ////    Initialized(order);

    ////    try
    ////    {
    ////        OnOrderCreated(new()
    ////        {
    ////            Order = order,
    ////            OldTotal = 100,
    ////            NewTotal = 80
    ////        });
    ////        //onCompleted?.Invoke(order);

    ////        OnOrderProcessCompleted(new() { Order = order });
    ////    }
    ////    catch (Exception ex)
    ////    {

    ////    }
    ////}


    ///// <summary>
    ///// Using Tuples to return IEnumerable<OrderExtensions.GenerateReport> 
    ///// </summary>
    ///// <param name="orders"></param>
    ///// <returns>IEnumerable<OrderExtensions.GenerateReport></returns>
    ///// /// equal to: public ValueTuple<Guid,int, decimal, IEnumerable<Item>> Process(IEnumerable<Order> orders)
    //public IEnumerable<(Guid orderNumber, 
    ////        int amountOfItems, 
    ////        decimal total, 
    ////        IEnumerable<Item> items)> Process(IEnumerable<Order> orders)
    ////{
    ////    var summaries = orders.Select(order =>
    ////    {
    ////        return 
    ////        (
    ////            Order: order.OrderNumber,
    ////            Items: order.LineItems.Count(),
    ////            Total: order.LineItems.Sum(item => item.Price),
    ////            LineItems: order.LineItems
    ////        );
    ////    });

    ////    return summaries.OrderBy(summary => summary.Total);
    ////}

    ///// <summary>
    ///// Using Switch Expression
    ///// </summary>
    ///// <param name="order"></param>
    ///// <returns></returns>
    //private decimal CalculateFreightCost(Order order) => 
    ////    order.ShippingProvider switch
    ////    {
    ////        SwedishPostalServiceShippingProvider
    ////        { DeliverNextDay: true } 
    ////        provider => provider.FreightCost + 50m,

    ////        SwedishPostalServiceShippingProvider 
    ////        provider => provider.FreightCost - 50m,

    ////        var provider => provider?.FreightCost ?? 50m
    ////    };


    ////private decimal CalculateFreightCostOld(Order order) =>
    ////    order.ShippingProvider switch
    ////    {
    ////        SwedishPostalServiceShippingProvider
    ////        {
    ////            DeliverNextDay: true
    ////        } => 100m,

    ////        SwedishPostalServiceShippingProvider => 0m,

    ////        _ => 50m
    ////    };
    #endregion

    #region Using Anonymous Types    
    ////private void Initialized(Order order)
    ////{
    ////    ArgumentNullException.ThrowIfNull(order);
    ////    if (OrOrderInitialized?.Invoke(order) == false)
    ////    {
    ////        throw new Exception($"Couldn't initialize {order.OrderNumber}");
    ////    }
    ////}

    ////public void Process(Order order,
    ////    Action<Order> onCompleted = default)
    ////{
    ////    Initialized(order);

    ////    try
    ////    {
    ////        OnOrderCreated(new()
    ////        {
    ////            Order = order,
    ////            OldTotal = 100,
    ////            NewTotal = 80
    ////        });
    ////        //onCompleted?.Invoke(order);

    ////        OnOrderProcessCompleted(new() { Order = order });
    ////    }
    ////    catch (Exception ex)
    ////    {

    ////    }
    ////}

    ////public object Process(IEnumerable<Order> orders)
    ////{
    ////    var summaries = orders.Select(order =>
    ////    {
    ////        return new
    ////        {
    ////            Order = order.OrderNumber,
    ////            Items = order.LineItems.Count(),
    ////            Total = order.LineItems.Sum(item => item.Price),
    ////            LineItems = order.LineItems
    ////        };
    ////    });
    ////    var orderedSummaries = summaries.OrderBy(summary => summary.Total);

    ////    var summary = orderedSummaries.First();
    ////    var summaryWithTax = summary with
    ////    {
    ////        Total = summary.Total * 1.25m
    ////    };

    ////    var item = summaryWithTax.LineItems.First();
    ////    item.Name = "RHoff";
    ////    return summaryWithTax;

    ////}
    #endregion

    #region Using Action & Func & Events
    ////public Func<Order, bool> OrOrderInitialized { get; set; }

    ////public event EventHandler<OrderCreatedEventArgs> OrderCreated;
    ////public event EventHandler<OrderProcessCompletedEventArgs> OrderProcessCompleted;

    ////protected virtual void OnOrderCreated(OrderCreatedEventArgs args)
    ////{
    ////    OrderCreated?.Invoke(this, args);
    ////}

    ////protected virtual void OnOrderProcessCompleted(OrderProcessCompletedEventArgs args)
    ////{
    ////    OrderProcessCompleted?.Invoke(this, args);
    ////}

    ////private void Initialized(Order order)
    ////{
    ////    ArgumentNullException.ThrowIfNull(order);
    ////    if(OrOrderInitialized?.Invoke(order) == false)
    ////    {
    ////        throw new Exception($"Couldn't initialize {order.OrderNumber}");
    ////    }
    ////}

    ////public void Process(Order order,
    ////    Action<Order> onCompleted = default)
    ////{
    ////    Initialized(order);

    ////    try
    ////    {
    ////        OnOrderCreated(new()
    ////        {
    ////            Order = order,
    ////            OldTotal = 100,
    ////            NewTotal = 80
    ////        });
    ////        //onCompleted?.Invoke(order);

    ////        OnOrderProcessCompleted(new() { Order = order });
    ////    }
    ////    catch (Exception ex) 
    ////    {

    ////    }
    ////}
    #endregion Using Action & Func

    #region Using Delegates

    ////public delegate bool OrderInitialized(Order order);
    ////public delegate void ProcessCompleted(Order order);

    ////public OrderInitialized? OnOrderInitialized { get; set; }

    ////private void Initialize(Order order)
    ////{
    ////    ArgumentNullException.ThrowIfNull(order);

    ////    if(OnOrderInitialized?.Invoke(order) == false)
    ////    {
    ////        throw new Exception($"Could't initialize {order.OrderNumber}");
    ////    }
    ////}

    ////public void Process(Order order,
    ////    ProcessCompleted onCompleted = default)
    ////{
    ////    Initialize(order);
    ////    try
    ////    {
    ////        onCompleted?.Invoke(order);
    ////    }catch (Exception ex)
    ////    {}
    ////}

    #endregion Using Delegates
}
