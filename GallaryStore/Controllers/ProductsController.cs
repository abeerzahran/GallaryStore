using GallaryStore.DTOs.product;
using GallaryStore.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GallaryStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        ProductService productService;
        public ProductsController(ProductService service)
        {
            productService = service;
        }
        [HttpGet]

        public ActionResult getAll()
        {
            List<ProductDTO>? products = productService.GetAll();

            if (products != null)
            {
                return Ok(products);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpGet("{id}")]
        public ActionResult getById(int id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            ProductDTO? product = productService.GetById(id);
            if (product != null)
            {
                return Ok(product);
            }
            else
            {
                return NotFound();
            }

        }
        [HttpPut("{id}")]
        public ActionResult Update(int id, ProductDTO product)
        {
            if (ModelState.IsValid)
            {
                if (id == null || id == 0 || id != product.id)
                {
                    return BadRequest();
                }
                else
                {
                    try
                    {
                        productService.Update(product);

                    }
                    catch (Exception ex)
                    {
                        return BadRequest(ex.Message);
                    }
                    return CreatedAtAction("getById", new { id = product.id }, product);
                }
            }
            return BadRequest(ModelState);

        }
        [HttpPost]
        public ActionResult Add(AddProductDTO product)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    productService.Add(product);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
                return Ok(product);
            }
            return BadRequest(ModelState);

        }
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            if (id == null)
            {
                return BadRequest("id is requred");
            }
            ProductDTO? product = productService.GetById(id);
            if (product != null)
            {
                try
                {
                    productService.Delete(id);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
                return Ok(product);
            }
            return NotFound();

        }

        [HttpGet]
        [Route("productPage/{page}/{categoryName}")]
        public ActionResult Get( string searchTerm = "",  int page = 1, int pageSize = 6, string categoryName="all")
        {
            try
            {
                List<ProductDTO> productsDTO = productService.getProductPage(searchTerm, page, pageSize, categoryName);
                return Ok(productsDTO);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
           
        }

        
    }
}
