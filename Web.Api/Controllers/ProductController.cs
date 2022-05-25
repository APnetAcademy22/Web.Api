using Microsoft.AspNetCore.Mvc;
using Product.Api.Models;
using Product.Api.SQL;

namespace Product.Api.Controllers
{
    [ApiController]
    [Route(template: "Product")]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private IProductDataProvider _productDataProvider;

        public ProductController(ILogger<ProductController> logger, IProductDataProvider productDataProvider)
        {
            _logger = logger;
            _productDataProvider = productDataProvider;
        }

        [HttpGet(template: "{Id}")]
        public Models.Product Get(int Id)
        {
            return _productDataProvider.GetById(Id);
        }

        [HttpPut(template: "Add")]
        public void Add(Models.Product product)
        {
            _productDataProvider.Add(product);
        }

        [HttpPost(template: "Edit")]
        public void Edit(Models.Product product)
        {
            _productDataProvider.Edit(product);
        }

        [HttpDelete(template: "Delete")]
        public void Delete(int id)
        {
            _productDataProvider.Delete(id);
        }

        [HttpGet(template: "List")]
        public IEnumerable<Models.Product> GetList(int? number)
        {
            return number == null ? _productDataProvider.GetAll() : _productDataProvider.GetMany(number.GetValueOrDefault());
        }
    }
}