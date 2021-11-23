using TakeHomeAssignment.Controllers;
using TakeHomeAssignment.test.TestHelpers;
using Xunit;

namespace TakeHomeAssignment.test.Controllers
{
    public class RiskControllerTest
    {
        [Fact]
        public void ShouldMakeTheCallToControllerAndReturnAResult()
        {
            var user = UserInputDTOHelper.GenerateUserInputDTO();
            var controller = new RiskController();

            var result = controller.CalculateRisk(user);
            Assert.NotNull(result);
        }
    }
}