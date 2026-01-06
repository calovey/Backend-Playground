using Microsoft.AspNetCore.Mvc;
using PaginationApi.Repositories;

namespace PaginationApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ProductRepository _repository;

        public ProductsController(ProductRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts([FromQuery] int page = 1, [FromQuery] int size = 10, [FromQuery] string sortField = "ProductId", [FromQuery] string sortDirection = "asc")
        {
            if (page < 1 || size < 1)
                return BadRequest("Page and size must be greater than 0");

            var (products, totalCount) = await _repository.GetPaginatedProductsAsync(page, size, sortField, sortDirection);

            var response = new
            {
                Data = products,
                Total = totalCount,
                Page = page,
                PageSize = size,
                TotalPages = (int)Math.Ceiling(totalCount / (double)size)
            };

            return Ok(response);
        }
    }
}