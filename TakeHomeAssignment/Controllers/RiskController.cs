using Microsoft.AspNetCore.Mvc;
using TakeHomeAssignment.DTO;
using TakeHomeAssignment.Services;

namespace TakeHomeAssignment.Controllers
{
    [ApiController]
    [Route("risk")]
    public class RiskController : Controller
    {
        [HttpPost]
        public ActionResult<RiskOutputDTO> CalculateRisk(UserInputDTO input)
        {
            var user = new Models.User(input);
            var riskCalculator = new RiskCalculator(user);
            var output = riskCalculator.GenerateOutput();

            return Ok(output.GenerateDTO());
        }
    }
}