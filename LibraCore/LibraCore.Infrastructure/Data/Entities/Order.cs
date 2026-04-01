using LibraCore.Infrastructure.Data.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using static LibraCore.GCommon.EntityValidation.Order;

namespace LibraCore.Infrastructure.Data.Entities
{
    public class Order
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        public DateTime OrderDate { get; set; } = DateTime.UtcNow;

        [Column(TypeName = OrderPriceFormat)]
        public decimal TotalPrice { get; set; }

        public OrderStatus Status { get; set; }

        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }

        public virtual ApplicationUser User { get; set; } = null!;

        public virtual ICollection<OrderItem> OrderItems { get; set; }
            = new HashSet<OrderItem>();
    }
}
