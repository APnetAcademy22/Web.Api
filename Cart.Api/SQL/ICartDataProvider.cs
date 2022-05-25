using Cart.Api.Models;

namespace Cart.Api.SQL
{
    public interface ICartDataProvider
    {
        void Add(CartItem cart);
        IEnumerable<CartItem> GetByUser(int userId);
        public IEnumerable<Models.CartItem> GetByUserAndProduct(int userId, int productId);
        void Update(CartItem cart);
        void Delete(int userId, int productId);
    }
}