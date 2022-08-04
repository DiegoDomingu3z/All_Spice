using System;
using System.Threading.Tasks;
using All_Spice.Models;
using All_Spice.Services;
using CodeWorks.Auth0Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace All_Spice.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class FavoritesController : ControllerBase
    {
        private readonly FavoritesService _fs;

        public FavoritesController(FavoritesService fs)
        {
            _fs = fs;
        }





        [HttpGet("{id}")]
        public async Task<ActionResult<Favorite>> GetById(int id)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                Favorite favorite = _fs.GetById(id);
                return favorite;

            }
            catch (System.Exception e)
            {

                return BadRequest(e.Message);
            }


        }


        [HttpPost]
        public async Task<ActionResult<Favorite>> Create([FromBody] Favorite favoriteData)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                favoriteData.AccountId = userInfo.Id;
                favoriteData.Creator = userInfo;
                favoriteData.CreatedAt = new DateTime();
                favoriteData.UpdatedAt = new DateTime();
                Favorite favorite = _fs.Create(favoriteData);
                return Ok(favorite);


            }
            catch (System.Exception e)
            {

                return BadRequest(e.Message);
            }

        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<Favorite>> Delete(int id)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                _fs.Delete(id, userInfo.Id);
                return Ok("deleted");
            }
            catch (System.Exception e)
            {

                return BadRequest(e.Message);
            }
        }



    }
}