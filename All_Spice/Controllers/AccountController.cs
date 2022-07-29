using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using All_Spice.Models;
using All_Spice.Services;
using CodeWorks.Auth0Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace All_Spice.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]

    public class AccountController : ControllerBase
    {
        private readonly AccountService _accountService;

        private readonly FavoritesService _fs;

        private readonly RecipesService _rs;

        public AccountController(AccountService accountService, FavoritesService fs, RecipesService rs)
        {
            _accountService = accountService;
            _fs = fs;
            _rs = rs;
        }

        [HttpGet]

        public async Task<ActionResult<Account>> Get()
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                return Ok(_accountService.GetOrCreateProfile(userInfo));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [HttpGet("recipes")]
        public async Task<ActionResult<List<Recipe>>> GetrecipesByAccount()
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                List<Recipe> recipes = _rs.GetRecipesByAccount(userInfo.Id);
                return (recipes);
            }
            catch (System.Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [HttpGet("favorites")]

        public async Task<ActionResult<List<FavoriteRecipeViewModel>>> GetByAccount()

        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                return _fs.GetByAccount(userInfo.Id);

            }
            catch (System.Exception e)
            {

                return BadRequest(e.Message);
            }


        }
    }


}