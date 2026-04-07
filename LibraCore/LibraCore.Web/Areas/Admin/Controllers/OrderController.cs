using LibraCore.GCommon.Exceptions;
using LibraCore.Infrastructure.Data.Enums;
using LibraCore.Services.Interfaces;
using LibraCore.ViewModels.Order;
using Microsoft.AspNetCore.Mvc;

using static LibraCore.GCommon.OutputMessages.Order;

namespace LibraCore.Web.Areas.Admin.Controllers
{
    public class OrderController : BaseAdminController
    {
        private readonly IOrderService orderService;
        private readonly ILogger<OrderController> logger;

        public OrderController(IOrderService orderService, ILogger<OrderController> logger)
        {
            this.orderService = orderService;
            this.logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IEnumerable<OrderViewModel> orders = await orderService.GetAllOrdersAsync();
            return View(orders);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateStatus(Guid id, OrderStatus newStatus)
        {
            if (id == Guid.Empty)
            {
                return BadRequest();
            }

            try
            {
                await orderService.UpdateOrderStatusAsync(id, newStatus);
            }
            catch (EntityNotFoundException)
            {
                logger.LogError(BookNotFoundMessage);

                return NotFound();
            }
            catch (EntityPersistFailureException)
            {
                logger.LogError(CreateOrderFailureMessage);
                TempData["Error"] = CreateOrderFailureMessage;
            }

            return RedirectToAction("Details", "Order", new { area = "", id = id });
        }
    }
}
