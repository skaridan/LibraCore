using LibraCore.GCommon.Exceptions;
using LibraCore.Services.Interfaces;
using LibraCore.ViewModels.Order;
using Microsoft.AspNetCore.Mvc;

using static LibraCore.GCommon.OutputMessages.Order;

namespace LibraCore.Web.Controllers
{
    public class OrderController : BaseController
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

        [HttpPost]
        public async Task<IActionResult> Buy(Guid bookId)
        {
            if (bookId == Guid.Empty)
            {
                return BadRequest();
            }

            try
            {
                string userId = GetUserId()!;

                await orderService.CreateOrderAsync(bookId, userId);

                TempData["Success"] = CreateOrderSuccessMessage;
            }
            catch (EntityNotFoundException)
            {
                logger.LogError(BookNotFoundMessage);

                TempData["Error"] = BookNotFoundMessage;

                return NotFound();
            }
            catch (EntityPersistFailureException)
            {
                logger.LogError(CreateOrderFailureMessage);

                TempData["Error"] = CreateOrderFailureMessage;
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
