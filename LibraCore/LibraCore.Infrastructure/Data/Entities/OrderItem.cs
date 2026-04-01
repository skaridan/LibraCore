using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using static LibraCore.GCommon.EntityValidation.OrderItem;

namespace LibraCore.Infrastructure.Data.Entities
{
    public class OrderItem
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [ForeignKey(nameof(Order))]
        public Guid OrderId { get; set; }

        public virtual Order Order { get; set; } = null!;

        [ForeignKey(nameof(Book))]
        public Guid BookId { get; set; }

        public virtual Book Book { get; set; } = null!;

        [Range(MinOrderQuantity, MaxOrderQuantity)]
        public int Quantity { get; set; }

        [Column(TypeName = OrderItemPriceFormat)]
        public decimal PriceAtPurchase { get; set; }
    }
}
