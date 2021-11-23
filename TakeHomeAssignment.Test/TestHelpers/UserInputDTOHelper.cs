using TakeHomeAssignment.DTO;
using TakeHomeAssignment.Helpers.Constants;

namespace TakeHomeAssignment.test.TestHelpers
{
    public static class UserInputDTOHelper
    {
        public static UserInputDTO GenerateUserInputDTO(int? age = 50,
            int? dependents = 0,
            double? income = 100,
            string houseOwnershipStatus = HouseOwnershipStatusConstant.Owned,
            int? vehicleYear = 2000,
            string maritalStatus = MaritalStatusConstant.Single,
            int riskQuestion1 = 0,
            int riskQuestion2 = 0,
            int riskQuestion3 = 0)
        {
            var user = new UserInputDTO
            {
                Age = age,
                Dependents = dependents,
                Income = income,
                MaritalStatus = maritalStatus,
                RiskQuestions = new[]
                {
                    riskQuestion1,
                    riskQuestion2,
                    riskQuestion3
                }
            };
            
            if (houseOwnershipStatus != null)
               user.House = new HouseDTO
               {
                   OwnershipStatus = houseOwnershipStatus
               };

            if (vehicleYear != null)
                user.Vehicle = new VehicleDTO
                {
                    Year = (int) vehicleYear
                };

            return user;
        }
    }
}