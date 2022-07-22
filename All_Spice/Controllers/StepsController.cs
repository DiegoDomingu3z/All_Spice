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
    public class StepsController : Controller
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
                return step;
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
                return steps;
            }
            catch (System.Exception e)
            {

                return BadRequest(e.Message);
            }
        }
    }
}