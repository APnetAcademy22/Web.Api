using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebAppFront.Models;

namespace WebAppFront.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpClientFactory _client;
        public string Message { get; set; }
        public IEnumerable<Product.Api.Models.Product> ProductList { get; set; }
        public IEnumerable<Cart.Api.Models.CartItem> CartList { get; set; }

        public HomeController(ILogger<HomeController> logger, IHttpClientFactory client)
        {
            _logger = logger;
            _client = client;
        }

        [HttpGet]
        public async Task<IActionResult> IndexAsync()
        {
            var client = _client.CreateClient("ProductApi");
            Message = await client.GetStringAsync("Product/List");
            ProductList = await client.GetFromJsonAsync<IEnumerable<Product.Api.Models.Product>>("Product/List");
            return View(ProductList);
        }

        [HttpGet]
        public async Task<IActionResult> CartAsync()
        {
            var client = _client.CreateClient("CartApi");
            Message = await client.GetStringAsync("cart/getcart");
            CartList = await client.GetFromJsonAsync<IEnumerable<Cart.Api.Models.CartItem>>("cart/getcart");
            return View(ProductList);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}