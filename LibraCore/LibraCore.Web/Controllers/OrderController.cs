using LibraCore.Services.Interfaces;
using LibraCore.ViewModels.Order;
using Microsoft.AspNetCore.Mvc;

namespace LibraCore.Web.Controllers
{
    public class OrderController : BaseController
    {
        private readonly IOrderService orderService;

        public OrderController(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            string userId = GetUserId()!;

            IEnumerable<OrderViewModel> orders = await orderService
                .GetUserOrdersAsync(userId);

            return View(orders);
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest();
            }

            OrderDetailsViewModel? order = await orderService.GetOrderDetailsAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }
    }
}
