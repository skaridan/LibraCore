namespace LibraCore.ViewModels.Order
{
    public class OrderItemViewModel
    {
        public string BookTitle { get; set; } = null!;

        public string? BookImageUrl { get; set; }

        public int Quantity { get; set; }

        public decimal PriceAtPurchase { get; set; }
    }
}
