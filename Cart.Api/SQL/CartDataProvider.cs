using Cart.Api.Models;
using System.Data.SqlClient;

namespace Cart.Api.SQL
{ 
    public class CartDataProvider : ICartDataProvider
    {
        private string _connectionString;
        public CartDataProvider(string connectionString)
        {
            _connectionString = connectionString;
        }
        public IEnumerable<Models.CartItem> GetByUserAndProduct(int userId, int productId)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();
            var query = "SELECT [UserId], [ProductId], [Quantity] FROM Cart WHERE userId=@UserId AND productId=@ProductId; ";
            using var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("UserId", userId);
            command.Parameters.AddWithValue("ProductId", productId);

            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                yield return new Models.CartItem()
                {
                    UserId = int.Parse(reader["UserId"].ToString()),
                    ProductId = int.Parse(reader["ProductId"].ToString()),
                    Quantity = int.Parse(reader["Quantity"].ToString())
                };
            }
        }

        public IEnumerable<Models.CartItem> GetByUser(int userId)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();
            var query = "SELECT [UserId], [ProductId], [Quantity] FROM Cart WHERE userId=@UserId; ";
            using var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("UserId", userId);

            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                yield return new Models.CartItem()
                {
                    UserId = int.Parse(reader["UserId"].ToString()),
                    ProductId = int.Parse(reader["ProductId"].ToString()),
                    Quantity = int.Parse(reader["Quantity"].ToString())
                };
            }
        }

        public void Add(CartItem cart)
        {
            IEnumerable<CartItem> alreadyPresent = GetByUserAndProduct(cart.UserId, cart.ProductId);
            if (alreadyPresent.Any())
            {
                cart.Quantity += alreadyPresent.First().Quantity;
                Update(cart);
            }
            else
            {
                using var connection = new SqlConnection(_connectionString);
                connection.Open();
                var query = "INSERT INTO Cart VALUES ( @UserId, @ProductId, @Quantity ); ";
                using var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("UserId", cart.UserId);
                command.Parameters.AddWithValue("ProductId", cart.ProductId);
                command.Parameters.AddWithValue("Quantity", cart.Quantity);

                command.ExecuteNonQuery();
            }
        }

        public void Update(CartItem cart)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();
            var query = @"UPDATE Cart
                            SET Quantity = @Quantity
                            WHERE UserId = @UserId AND ProductId = @ProductId;";
            using var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("UserId", cart.UserId);
            command.Parameters.AddWithValue("ProductId", cart.ProductId);
            command.Parameters.AddWithValue("Quantity", cart.Quantity);

            command.ExecuteNonQuery();
        }

        public void Delete(int userId, int productId)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();
            var query = @"DELETE FROM Cart
                            WHERE UserId = @UserId AND ProductId = @ProductId;";
            using var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("UserId", userId);
            command.Parameters.AddWithValue("ProductId", productId);

            command.ExecuteNonQuery();
        }
    }
}
