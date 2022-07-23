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
    public class StepsController : ControllerBase
    {

        private readonly StepsService _ss;

        public StepsController(StepsService ss)
        {
            _ss = ss;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Step>> Get(int id)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                Step step = _ss.GetStepById(id);
                return Ok(step);
            }
            catch (System.Exception e)
            {

                return BadRequest(e.Message);
            }
        }



        [HttpPost]
        public async Task<ActionResult<Step>> CreateAsync([FromBody] Step stepsData)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                Step steps = _ss.Create(stepsData, userInfo.Id);
                steps.Creator = userInfo;
                return Ok(steps);
            }
            catch (System.Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Step>> Edit(int id, [FromBody] Step stepData)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                stepData.Id = id;
                Step update = _ss.Edit(id, stepData, userInfo.Id);
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
                string step = _ss.Delete(id, userInfo.Id);
                return Ok("Delorted");
            }
            catch (System.Exception e)
            {

                return BadRequest(e.Message);
            }
        }
    }
}