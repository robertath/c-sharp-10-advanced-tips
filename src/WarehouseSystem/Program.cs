using System.Text.Json;
using WarehouseSystem;
using WarehouseSystem.Business;
using WarehouseSystem.Domain;
using WarehouseSystem.Domain.Extensions;


//var order = new Order(
//{
//    IsReadyForShipment = true,
//    LineItems = new[]
//    {
//        new Item {Name = "PS1", Price = 50 },
//        new Item {Name = "PS2", Price = 60 },
//        new Item {Name = "PS4", Price = 70 },
//        new Item {Name = "PS5", Price = 80 },
//    },
//    Total = 50
//};

var LineItems = new[]
{
    new Item {Name = "PS1", Price = 50 },
    new Item {Name = "PS2", Price = 60 },
    new Item {Name = "PS4", Price = 70 },
    new Item {Name = "PS5", Price = 80 },
};

var order = new Order(260, new(), LineItems);


bool SendMessageToWarehouse(Order order)
{
    Console.WriteLine("Please check pack the order");
    return true;
}

void SendConfirmationEmail(Order order) =>
    Console.WriteLine($"Order Confirmation Email for {order.OrderNumber}");

void One(Order order) => Console.WriteLine("One");
void Two(Order order) => Console.WriteLine("Two");
void Three(Order order) => Console.WriteLine("Three");

#region Load the orders from orders.json
Order[] orders = JsonSerializer.Deserialize<Order[]>(File.ReadAllText("orders.json"))!;
#endregion


#region Garbage collector
var processor = new OrderProcessor();

ExecuteFileProcessor();

GC.Collect();

Console.ReadLine();

void ExecuteFileProcessor()
{
    using var fileProcessor = new FileProcessor(processor);
    fileProcessor.Start();
    Console.WriteLine("Method completed");
}

//async Task ExecuteFileProcessor()
//{
//    await using var fileProcessor = new FileProcessor(processor);
//    fileProcessor.Start();
//    Console.WriteLine("Method completed");
//}

#endregion

#region Indexer

//Using Span<T>
var payload = new byte[1024];
var validator = new PayloadValidator();

validator.Validate(payload);


//var first = payload[0];

var list = new OrderList(orders);
var orderFromList = list[0];

int[] numbersForSlicing = new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
Index index = numbersForSlicing.Length / 2;
Index index2 = new Index(5, true);


//int[] slice = numbersForSlicing[..index]; //all index until index
//int[] slice = numbersForSlicing[index2..]; //all index after index
//int[] slice = numbersForSlicing[..^1]; //skip the last one
int[] slice = numbersForSlicing[^5..]; //skip the five firsts

foreach (var element in slice)
{
    Console.WriteLine(element);
}

#endregion

#region Nullables
////var processor = new OrderProcessor();
////processor.OrderProcessCompleted += Processor_Completed;



////void Processor_Completed(object? sender, OrderProcessCompletedEventArgs args)
////{
////    ArgumentNullException.ThrowIfNull(args.Order);

////    //if (args.Order is null)
////    //    return;

////    //if(args.Order is { Total: >100 })
////    //{
////        ////ShippingProvider provider = args.Order.ShippingProvider ?? new();
////        //or
////        ShippingProvider? provider = args.Order.ShippingProvider;
////        ////provider ??= new();
////        if (ShippingProviderValidator.ValidateShippingProvider(provider))
////        {
////            var name = provider.Name;
////        }        

////        Guid orderNumber = args.Order.OrderNumber;
////        string orderNumberAsString = orderNumber.ToString();
////        Console.WriteLine(orderNumberAsString);
////    //}


////}

////processor.Process(orders: new Order[10]);

#endregion

#region Record Types - Serializing and Deserializing a Record

//var orderAsJson = JsonSerializer.Serialize(order, 
//    options: new() { WriteIndented = true });


//CancelledOrder cancelledOrder = new(102, new(), LineItems);

//Console.WriteLine(orderAsJson);
//Console.WriteLine();
//Console.WriteLine(order.ToString());
//Console.WriteLine();
//Console.WriteLine(cancelledOrder.ToString());
//Console.ReadLine();
#endregion

#region Record Types (Introducing)

//var first = new Customer("Roberta", "Hoffmann") {
//    Address = new("John Up St.", "XXX3434")
//};

//var second = new PriorityCustomer("Roberta", "Hoffmann")
//{
//    Address = new("John Up St.", "XXX3434")
//};
//var third = first with { FirstName = "Thamara" };

//Console.WriteLine($"Are these instances equal? {first == second}");
//Console.WriteLine($"Third: {third.FirstName} {third.LastName}");
//Console.ReadLine();
#endregion

#region Pattern Matching - Switch Expression

//Console.WriteLine(order.GenerateReport());
//Console.ReadLine();

#endregion

#region Tupla & Deconstruct
//var dictionary = new Dictionary<string, Order>();
//dictionary.Add(order.OrderNumber.ToString(), order);

//foreach(var (orderNumber, theOrder) in dictionary)
//{
//    Console.WriteLine($"Order: {orderNumber} - Total: {theOrder.Total} - Amount Items: {theOrder.LineItems.Sum(i=>i.Price)}.");
//}

//var (orderNumber, total, items, averagePrice) = order;
//if (order is (total: > 0, true))
//{
//    Console.WriteLine($"Order: {orderNumber} - Total: {total} - Amount Items: {items.Count()} - Average Price: {averagePrice}.");
//}

//Guid orderNumber2;
//decimal sum;

//(orderNumber2, items, sum) =
//    (order.OrderNumber,
//    order.LineItems,
//    order.LineItems.Sum(item => item.Price));

//Console.WriteLine($"Order: {orderNumber2} - Total: {sum} - Amount Items: {items.Count()}.");
Console.ReadLine();
#endregion

#region Tupla with return type or parameters
//IEnumerable<Order> orders = JsonSerializer.Deserialize<Order[]>(File.ReadAllText("orders.json"));
//var isReadyForShipment = (Order order) => order.IsReadyForShipment;
//var processor = new OrderProcessor { OrOrderInitialized = isReadyForShipment };

//// Using Tuples as Return Types or Parameters
//foreach (var summary in processor.Process(orders))
//{
//    Console.WriteLine(summary.GenerateReport());
//}


////how to assign
//(Guid, int, decimal, IEnumerable<Item>) tuple = (Guid.Empty, 0, 0m, Enumerable.Empty<Item>());
//tuple.GenerateReport();


//// Tuples are smart and can detect the same object
//// Tuple elements ignored because of the names
//(Guid id, int total) GetSummary()
//{
//    return (orderId: Guid.Empty, orderTotal: 10);
//}

//var (id, items, total, _) = processor.Process(orders).First();

//Action<(Guid id, int amountOfItems)> log = (tuple) =>
//{

//};
//var first = processor.Process(orders);
//var second = processor.Process(orders);
//Console.WriteLine($"Are these equal? {first == second}");

//Console.ReadLine();
#endregion Tupla

#region Tupla
//var groupTupla = (order.OrderNumber, 
//                order.LineItems,
//                Sum: order.LineItems.Sum(item => item.Price));

//Console.WriteLine("--------------");
//var jsonTupla = JsonSerializer.Serialize(groupTupla, options: new() { IncludeFields = true, WriteIndented = true });
//Console.WriteLine(jsonTupla);

////using anonymous
//var groupAsAnonymousType = new
//{
//    order.OrderNumber,
//    order.LineItems,
//    Sum = order.LineItems.Sum(item => item.Price)
//};

//var jsonAnonymous = JsonSerializer.Serialize(groupAsAnonymousType);
//Console.WriteLine(jsonAnonymous);

//Console.ReadLine();
#endregion Tupla

#region Anonymous types + Linq
//IEnumerable<Order> orders = JsonSerializer.Deserialize<Order[]>(File.ReadAllText("orders.json"));
//var isReadyForShipment = (Order order) => order.IsReadyForShipment;
//var processor = new OrderProcessor { OrOrderInitialized = isReadyForShipment };
//var result = processor.Process(orders);

//var type = result.GetType();
//var properties = type.GetProperties();
//foreach (var property in properties)
//{
//    Console.Write($"Property: {property.Name} - ");
//    Console.WriteLine($"Value: {property.GetValue(result)}");
//}

//Console.ReadLine();
#endregion Anonymous types + Linq

#region Anonymous types
//var subset = new
//{
//    order.OrderNumber,
//    order.Total,
//    AveragePrice = order.LineItems.Average(item => item.Price)
//};
//Console.WriteLine(subset);
//Console.WriteLine("___________________________");
//Console.WriteLine();
//var firstInstance = new { Total = 100, AmountOfItems = 10 };
//var secondInstance = new { Total = 100, AmountOfItems = 10 };

//Console.WriteLine($"Equal: {firstInstance.Equals(secondInstance)}"); //equal checks the properties
//Console.WriteLine($"== : {firstInstance == secondInstance}"); //== check the reference

//Console.ReadLine();
#endregion Anonymous types

#region Extension Method
//foreach (var item in order.LineItems.Find(item => item.Price > 60))
//{
//    Console.WriteLine($"Item: {item.Name} - Price: {item.Price}.");
//}

////var cheapestItems = order.LineItems.Where(item => item.Price > 60)
////                                   .OrderBy(item => item.Price)
////                                   .Take(5);
//Console.ReadLine();
//Console.WriteLine("==============");
//// Creating an Extension Method Library
//var report = order.GenerateReport(recipient: "Filip Ekberg");
//Console.WriteLine(report);


//Console.ReadLine();
#endregion

#region Events
//Func<Order, bool> isReadyForShipment = (Order order) => order.IsReadyForShipment;
//var processor = new OrderProcessor()
//{
//    OrOrderInitialized = isReadyForShipment
//};
//Action<Order> onCompleted = (order) =>
//{
//    Console.WriteLine($"Processed: {order.OrderNumber}");
//};

//processor.OrderCreated += (sender, args) =>
//{
//    Thread.Sleep(1000);
//    Console.WriteLine($"Order: 1");
//};

//processor.OrderCreated += Log;
//processor.Process(order, onCompleted);

//void Log(object sender, OrderCreatedEventArgs e)
//{
//    Console.WriteLine($"Total: { e.NewTotal }");
//    Console.WriteLine("Order Created!");
//}

#endregion

#region Using Delegate
//var processor = new OrderProcessor();
//processor.OnOrderInitialized = SendMessageToWarehouse;
//or
//var processor = new OrderProcessor
//{
//    OnOrderInitialized = SendMessageToWarehouse
//};

//processor.Process(order, SendConfirmationEmail);


//void One(Order order) => Console.WriteLine("One");
//void Two(Order order) => Console.WriteLine("Two");
//void Three(Order order) => Console.WriteLine("Three");
#endregion Using Delegate

#region Multicast delegate
//var processor = new OrderProcessor
//{
//    OnOrderInitialized = SendMessageToWarehouse
//};

//OrderProcessor.ProcessCompleted chain = One;
//chain += Two;
//chain += Three;
//or
//OrderProcessor.ProcessCompleted chain = (OrderProcessor.ProcessCompleted)One + Two + Three; ;

//processor.Process(order, chain);

//chain -= Two;
//processor.Process(order, chain);
#endregion Multicast delegate

#region Lambda Expression
//var processor = new OrderProcessor
//{
//    OnOrderInitialized = SendMessageToWarehouse
//};

//OrderProcessor.ProcessCompleted onCompleted = (order) =>
//{
//    Console.WriteLine($"Processed {order.OrderNumber}");
//};

////onCompleted += (order) =>
////{
////    Console.WriteLine("Refill stock...");
////};

//processor.Process(order, onCompleted);
#endregion Lambda Statement

#region Lambda Statement
//var processor = new OrderProcessor
//{
//    OnOrderInitialized = (order) => order.IsReadyForShipment
//};


//var processedOrders = new List<Guid>();

//OrderProcessor.ProcessCompleted onCompleted = (order) =>
//{
//    processedOrders.Add(order.OrderNumber);
//    Console.WriteLine($"Processed {order.OrderNumber}");
//};

//processor.Process(order, onCompleted);
#endregion Lambda Statement

#region Action & Func
//Func<Order, bool> isReadyForShipment = (Order order) => order.IsReadyForShipment;

//var processor = new OrderProcessor
//{
//    OrOrderInitialized = isReadyForShipment
//};

//Action<Order> onCompleted = (order) =>
//{
//    Console.WriteLine($"Processed: {order.OrderNumber}");
//};

//processor.Process(order, onCompleted);

//Func<Order, bool> func = SendMessageToWarehouse;
#endregion Action & Func
