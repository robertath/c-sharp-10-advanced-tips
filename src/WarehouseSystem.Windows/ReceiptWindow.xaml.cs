using System.ComponentModel;
using System.Windows;
using WarehouseSystem.Business;

namespace WarehouseSystem.Windows;

/// <summary>
/// Interaction logic for ReceiptWindow.xaml
/// </summary>
public partial class ReceiptWindow : Window
{
    private readonly OrderProcessor processor;

    public ReceiptWindow(OrderProcessor processor)
    {
        InitializeComponent();

        this.processor = processor;
        this.processor.OrderCreated += Processor_OrderCreated;
    }

    protected override void OnClosing(CancelEventArgs e)
    {
        this.processor.OrderCreated -= Processor_OrderCreated;
        base.OnClosing(e);
    }

    private void Processor_OrderCreated(object sender,
        OrderCreatedEventArgs args)
    {
        MessageBox.Show($"Processed {args.Order.OrderNumber}");
    }
}
