using GallaryStore.DTOs;
using GallaryStore.DTOs.favourite;
using GallaryStore.DTOs.product;
using GallaryStore.Models;
using GallaryStore.Services;
using Microsoft.AspNetCore.Authorization;
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
        [HttpGet]
        [Route("getById")]
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

        [HttpGet]
        [Route("getByUserId")]
        [Authorize]
        public ActionResult getByUserId()
        {
            List<ProductDTO> favourites = FavouriteService.GetByUserId(User.Claims.FirstOrDefault(user=>user.Type=="userId").Value, "product");
            if (favourites != null)
            {
                return Ok(favourites);
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
        [Authorize]
        public ActionResult Add(addFavouriteProductDTO Favourite)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    string userId = User.Claims.FirstOrDefault(c => c.Type == "userId").Value;
                    FavouriteService.Add(new FavouriteDTO(userId,Favourite.productId));
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
        [Authorize]
        public ActionResult Delete(addFavouriteProductDTO favourite)
        {
            if(ModelState.IsValid)
            {
                string userId = User.Claims.FirstOrDefault(c => c.Type == "userId").Value;

                FavouriteDTO? Favourite = FavouriteService.GetById(favourite.productId, userId, null);
                if (Favourite != null)
                {
                    FavouriteService.Delete(new FavouriteDTO(userId,favourite.productId));
                    return Ok(Favourite);
                }
                
                return NotFound();
                
            }
            return BadRequest(ModelState);
            
        }
    }
}
