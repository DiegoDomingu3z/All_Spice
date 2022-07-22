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
    [Route("api/[controller]")]

    public class RecipesController : ControllerBase
    {
        private readonly RecipesService _rs;

        private readonly IngredientsService _ins;

        public RecipesController(RecipesService rs, IngredientsService ins)
        {
            _rs = rs;
            _ins = ins;
        }

        [HttpGet]
        public async Task<ActionResult<List<Recipe>>> Get(string query = "")
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                List<Recipe> recipes = _rs.GetAll();
                return Ok(recipes);
            }
            catch (System.Exception e)
            {

                return BadRequest(e.Message);
            }
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Recipe>> Get(int id)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                Recipe recipe = _rs.GetById(id);
                return Ok(recipe);
            }
            catch (System.Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}/ingredients")]
        [Authorize]

        public async Task<ActionResult<List<Ingredient>>> GetIngredients(int id)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                List<Ingredient> ingredients = _ins.GetIngredientsByRecipeId(id, userInfo.Id);
                return ingredients;
            }
            catch (System.Exception e)
            {

                return BadRequest(e.Message);
            }
        }


        [HttpPost]
        [Authorize]

        public async Task<ActionResult<Recipe>> CreateAsync([FromBody] Recipe recipeData)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                recipeData.CreatorId = userInfo.Id;
                Recipe newRecipe = _rs.Create(recipeData);
                recipeData.CreatedAt = new DateTime();
                newRecipe.Creator = userInfo;
                recipeData.updatedAt = new DateTime();

                return Ok(newRecipe);
            }
            catch (System.Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<Recipe>> EditAsync(int id, [FromBody] Recipe recipeData)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                recipeData.Id = id;
                recipeData.CreatorId = userInfo.Id;
                Recipe update = _rs.Edit(recipeData);
                return Ok(update);
            }
            catch (System.Exception e)
            {

                return BadRequest(e.Message);
            }


        }


        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<Recipe>> DeleteAsync(int id)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                Recipe deletedRecipe = _rs.Delete(id, userInfo.Id);
                return Ok("deleted");
            }
            catch (System.Exception e)
            {

                return BadRequest(e.Message);
            }
        }


    }
}