namespace LibraCore.ViewModels.Order
{
    public class OrderDetailsViewModel
    {
        public Guid Id { get; set; }

        public DateTime OrderDate { get; set; }

        public decimal TotalPrice { get; set; }

        public string Status { get; set; } = null!;

        public IEnumerable<OrderItemViewModel> Items { get; set; }
            = new List<OrderItemViewModel>();
    }
}
