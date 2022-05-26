using Microsoft.AspNetCore.Mvc;
using Cart.Api.Models;
using Cart.Api.SQL;

namespace Cart.Api.Controllers
{
    [ApiController]
    [Route("cart")]
    public class CartController : ControllerBase
    {

        private readonly ILogger<CartController> _logger;
        private readonly ICartDataProvider _cartDataProvider;

        public CartController(ILogger<CartController> logger, ICartDataProvider cartDataProvider)
        {
            _logger = logger;
            _cartDataProvider = cartDataProvider;
        }

        [HttpGet(template: "GetOrder")]
        public IEnumerable<CartItem> Get(int userId, int productId)
        {
            return _cartDataProvider.GetByUserAndProduct(userId, productId);
        }

        [HttpGet(template: "GetCart")]
        public IEnumerable<CartItem> GetCart(int userId)
        {
            return _cartDataProvider.GetByUser(userId);
        }

        [HttpPut(template: "Add")]
        public void Add(CartItem cart)
        {
             _cartDataProvider.Add(cart);
        }

        [HttpPost(template: "Update")]
        public void Update(CartItem cart)
        {
            _cartDataProvider.Update(cart);
        }

        [HttpDelete(template: "Delete")]
        public void Delete(int userId, int productId)
        {
            _cartDataProvider.Delete(userId, productId);
        }
    }
}