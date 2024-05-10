using GallaryStore.DTOs;
using GallaryStore.Services;
using Microsoft.AspNetCore.Mvc;

namespace GallaryStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavouritesController : ControllerBase
    {
        FavouriteService FavouriteService;
        public FavouritesController(FavouriteService service)
        {
            FavouriteService = service;
        }
        [HttpGet]
        public ActionResult getAll()
        {
            List<FavouriteDTO>? Favourites = FavouriteService.GetAll();

            if (Favourites != null)
            {
                return Ok(Favourites);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpGet("{id}")]
        public ActionResult getById(string userId,int productId)
        {
            if (productId == null)
            {
                return BadRequest();
            }
            FavouriteDTO? Favourite = FavouriteService.GetById( productId,userId,null);
            if (Favourite != null)
            {
                return Ok(Favourite);
            }
            else
            {
                return NotFound();
            }

        }
        [HttpPut("{id}")]
        public ActionResult Update(int id, FavouriteDTO Favourite)
        {
            if(ModelState.IsValid)
            {
                if (id == null || id == 0 || id != Favourite.productId)
                {
                    return BadRequest();
                }
                else
                {
                    try
                    {
                        FavouriteService.Update(Favourite);
                        return Ok(Favourite);
                    }
                    catch (Exception ex)
                    {
                        return BadRequest(ex.Message);
                    }
                    
                }
            }
            return BadRequest(ModelState);
            
        }
        [HttpPost]
        public ActionResult Add(FavouriteDTO Favourite)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    FavouriteService.Add(Favourite);
                    return Ok(Favourite);
                }
                catch(Exception ex)
                {
                    return BadRequest(ex.Message);
                }
                
            }
            return BadRequest(ModelState);
        }
        [HttpDelete]
        public ActionResult Delete(FavouriteDTO favourite)
        {
            if(ModelState.IsValid)
            {

                FavouriteDTO? Favourite = FavouriteService.GetById(favourite.productId, favourite.userId, null);
                if (Favourite != null)
                {
                    FavouriteService.Delete(favourite);
                    return Ok(Favourite);
                }
                
                return NotFound();
                
            }
            return BadRequest(ModelState);
            
        }
    }
}
