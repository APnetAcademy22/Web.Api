using Product.Api.Models;

namespace Product.Api.SQL
{
    public interface IProductDataProvider
    {
        public Models.Product GetById(int id);
        public void Add(Models.Product product);
        public void Edit(Models.Product product);
        public void Delete(int id);
        public IEnumerable<Models.Product> GetMany(int limit);
        public IEnumerable<Models.Product> GetAll();
    }
}
