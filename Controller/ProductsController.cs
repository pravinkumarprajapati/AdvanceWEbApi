using Asp.Versioning;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AdvanceWEbApi.Controller
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/products")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ProductsV1Controller : ControllerBase
    {
        public ProductsV1Controller()
        {
            
        }
        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = new[]
            {
                new { Id = 1, Name = "Product V1 A", Price = 10.0 },
                new { Id = 2, Name = "Product V1  B", Price = 20.0 },
                new { Id = 3, Name = "Product V1  C", Price = 30.0 }
            };
            return await Task.FromResult( Ok(products));
        }
    }

    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/products")]
    [ApiController]
    public class ProductsV2Controller : ControllerBase
    {
        public ProductsV2Controller()
        {

        }
        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = new[]
            {
                new { Id = 1, Name = "Product V2 A", Price = 10.0 },
                new { Id = 2, Name = "Product V2 B", Price = 20.0 },
                new { Id = 3, Name = "Product V2 C", Price = 30.0 },
                 new { Id = 3, Name = "Product V1 C", Price = 30.0 }
            };
            return await Task.FromResult(Ok(products));
        }
    }
}
