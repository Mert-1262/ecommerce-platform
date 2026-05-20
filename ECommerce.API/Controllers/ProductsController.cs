using ECommerce.Business.Abstract;
using ECommerce.Entities.Concrete;
using ECommerce.Entities.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _productService.GetAll();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var result = _productService.GetById(id);

            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Add([FromBody] CreateProductDto dto)
        {
            var product = new Product
            {
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                Stock = dto.Stock,
                ImageUrl = dto.ImageUrl,
                CategoryId = dto.CategoryId
            };

            _productService.Add(product);

            return Ok("Ürün eklendi.");
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}/stock")]
        public IActionResult UpdateStock(int id, [FromBody] UpdateProductStockDto dto)
        {
            try
            {
                _productService.UpdateStock(id, dto.Stock);

                return Ok("Stok güncellendi.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        public IActionResult Update([FromBody] UpdateProductRequest request)
        {
            var product = new Product
            {
                Id = request.Id,
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                Stock = request.Stock,
                ImageUrl = request.ImageUrl,
                CategoryId = request.CategoryId
            };

            _productService.Update(product);

            return Ok("Ürün güncellendi.");
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _productService.Delete(id);

            return Ok("Ürün silindi.");
        }
        [HttpGet("search")]
        public IActionResult Search(string keyword)
        {
            return Ok(_productService.Search(keyword));
        }

        [HttpGet("category/{categoryId}")]
        public IActionResult GetByCategory(int categoryId)
        {
            return Ok(_productService.GetByCategory(categoryId));
        }
    }
}