using Core.Entities;
using Core.Interfaces;
using Core.Specification;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SkinetWebApi.Dtos;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkinetWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : Controller
    {
        private readonly IGenericRepository<Product> _productsRepo;
        private readonly IGenericRepository<ProductBrand> _productBrandRepo;
        private readonly IGenericRepository<ProductType> _productTypeRepo;


        public ProductsController(IGenericRepository<Product> productsRepo,
                                 IGenericRepository<ProductBrand> productBrandRepo,
                                 IGenericRepository<ProductType> productTypeRepo)
        {
            _productsRepo = productsRepo;
            _productBrandRepo = productBrandRepo;
            _productTypeRepo = productTypeRepo;
        }

        //private readonly IProductRepository _productRepository;

        //public ProductsController(IProductRepository productRepository)
        //{
        //    _productRepository = productRepository;
        //}






        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public async Task<ActionResult<List<ProductToReturnDto>>> GetProducts()
        {

            var spec = new ProductsWithTypesAndBrandsSpecification();

            var products = await _productsRepo.ListAsync(spec);

            return products.Select(productResult => new ProductToReturnDto
            {
                Id = productResult.Id,
                Name = productResult.Name,
                Description = productResult.Description,
                PictureUrl = productResult.PictureUrl,
                Price = productResult.Price,
                ProductBrand = productResult.ProductBrand.Name,
                ProductType = productResult.ProductType.Name
            }).ToList();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
        {
            var spec = new ProductsWithTypesAndBrandsSpecification(id);

            var productResult =  await _productsRepo.GetEntityWithSpec(spec);

            return new ProductToReturnDto
            {
                Id = productResult.Id,
                Name = productResult.Name,
                Description = productResult.Description,
                PictureUrl = productResult.PictureUrl,
                Price = productResult.Price,
                ProductBrand = productResult.ProductBrand.Name,
                ProductType = productResult.ProductType.Name
            };
        }

        [HttpGet("brands")]
        public async Task<ActionResult<List<ProductBrand>>> GetProductBrands()
        {
            var productBrands = await _productBrandRepo.ListAllAsync();

            return Ok(productBrands);
        }


        [HttpGet("types")]
        public async Task<ActionResult<List<ProductType>>> GetProductTypes()
        {
            var productTypes = await _productTypeRepo.ListAllAsync();

            return Ok(productTypes);
        }

    }
}
