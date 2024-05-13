using GallaryStore.DTOs.category;
using GallaryStore.Models;
using GallaryStore.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GallaryStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        CategoryService categoryService;
        public CategoriesController(CategoryService service)
        {
            categoryService = service;
        }
        [HttpGet]
        public ActionResult getAll()
        {
            List<CategoryDTO>? categorys = categoryService.GetAll();

            if (categorys != null)
            {
                return Ok(categorys);
            }
            
            return NotFound();
            
        }
        [HttpGet("{id}")]
        public ActionResult getById(int id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            CategoryDTO? category = categoryService.GetById(id);
            if (category != null)
            {
                return Ok(category);
            }
           
            return NotFound();
            

        }
        [HttpPut("{id}")]
        public ActionResult Update(int id,CategoryDTO category)
        {
            if(ModelState.IsValid)
            {
                if (id != null && id != 0 &&  category.id == id)
                {
                    try
                    {
                        categoryService.Update(category);

                    }
                    catch (Exception ex)
                    {
                        return BadRequest(ex.Message);
                    }
                    return CreatedAtAction("getById", new { id = category.id }, category);
                }
                return BadRequest();
            }
            return BadRequest(ModelState);
            
        }
        [HttpPost]
        public ActionResult Add(AddCategoryDTO category)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    categoryService.Add(category);
                    return Ok(category);
                }
                catch(Exception ex)
                {
                    return BadRequest(ex.Message);
                }
                
            }
            return BadRequest(ModelState);
        }
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            CategoryDTO? category = categoryService.GetById(id);
            if (category != null)
            {
                categoryService.Delete(id);
                return Ok(category);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("categoryProducts/{id}")]
        public ActionResult getCategoryProducts(int id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            CategoryDTO? category = categoryService.GetById(id);
            if (category != null)
            {
                return Ok(categoryService.getCategoryProducts(id));
            }
            else
            {
                return NotFound();
            }
        }
    }
}
