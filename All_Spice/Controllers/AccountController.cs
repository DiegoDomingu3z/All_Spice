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
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly AccountService _accountService;

        private readonly FavoritesService _fs;

        public AccountController(AccountService accountService, FavoritesService fs)
        {
            _accountService = accountService;
            _fs = fs;
        }

        [HttpGet]
        [Authorize]
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

        [HttpGet("favorites")]
        [Authorize]
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