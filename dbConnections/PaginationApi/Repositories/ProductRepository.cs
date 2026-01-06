using Microsoft.Data.SqlClient;
using PaginationApi.Models;
using System.Data;

namespace PaginationApi.Repositories
{
    public class ProductRepository
    {
        private readonly string _connectionString;

        public ProductRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<(IEnumerable<Product> Products, int TotalCount)> GetPaginatedProductsAsync(int pageNumber, int pageSize, string sortField, string sortDirection)
        {
            var products = new List<Product>();
            int totalCount = 0;

            var validFields = new[] { "ProductId", "CreatedDate" };
            var validDirections = new[] { "asc", "desc" };

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();


                var sql = $@"SELECT ProductId, ProductName, Price, CreatedDate
                             FROM Products
                             ORDER BY {sortField} {sortDirection}
                             OFFSET (@PageNumber - 1) * @PageSize ROWS
                             FETCH NEXT @PageSize ROWS ONLY;
                             SELECT COUNT(*) FROM Products;";


                using (var command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@PageNumber", pageNumber);
                    command.Parameters.AddWithValue("@PageSize", pageSize);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            products.Add(new Product
                            {
                                ProductId = reader.GetInt32(0),
                                ProductName = reader.GetString(1),
                                Price = reader.GetDecimal(2),
                                CreatedDate = reader.GetDateTime(3)
                            });
                        }

                        await reader.NextResultAsync();
                        if (await reader.ReadAsync())
                        {
                            totalCount = reader.GetInt32(0);
                        }
                    }
                }
            }

            return (products, totalCount);
        }
    }
}
