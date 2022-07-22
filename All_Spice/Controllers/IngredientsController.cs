using System.Threading.Tasks;
using All_Spice.Models;
using All_Spice.Services;
using CodeWorks.Auth0Provider;
using Microsoft.AspNetCore.Mvc;

namespace All_Spice.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class IngredientsController : ControllerBase
    {
        private readonly IngredientsService _ins;

        public IngredientsController(IngredientsService ins)
        {
            _ins = ins;
        }



        [HttpGet("{id}")]
        public async Task<ActionResult<Ingredient>> Get(int id)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                Ingredient ingredient = _ins.Get(id);
                return Ok(ingredient);
            }
            catch (System.Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Ingredient>> CreateAsync([FromBody] Ingredient ingredientData)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                Ingredient newIngredient = _ins.Create(ingredientData, userInfo.Id);
                newIngredient.Creator = userInfo;
                return Ok(newIngredient);
            }
            catch (System.Exception e)
            {

                return BadRequest(e.Message);
            }
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<Ingredient>> EditAsync(int id, [FromBody] Ingredient ingredientData)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                ingredientData.Id = id;
                Ingredient update = _ins.Edit(id, ingredientData, userInfo.Id);
                return Ok(update);
            }
            catch (System.Exception e)
            {

                return BadRequest(e.Message);
            }


        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<string>> Delete(int id)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                string deletedIngredient = _ins.Delete(id, userInfo.Id);
                return Ok("deleted");
            }
            catch (System.Exception e)
            {

                return BadRequest(e.Message);
            }
        }



    }
}