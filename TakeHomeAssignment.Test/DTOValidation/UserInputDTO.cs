using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TakeHomeAssignment.DTO;
using TakeHomeAssignment.Helpers.Constants;
using TakeHomeAssignment.test.TestHelpers;
using Xunit;

namespace TakeHomeAssignment.test.DTOValidation
{
    public class UserInputDTOTest
    {
        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        public void ShouldReturnSuccessValidationIfAgeIsZeroOrGreater(int age)
        {
            var user = UserInputDTOHelper.GenerateUserInputDTO(age: age);
            var validationContext = new ValidationContext(user);

            var result = new List<ValidationResult>();
            Validator.TryValidateObject(user, validationContext, result, true);
            
            Assert.Empty(result);
        }
        
        [Fact]
        public void ShouldReturnValidationErrorIfAgeIsNull()
        {
            var user = UserInputDTOHelper.GenerateUserInputDTO(age: null);
            var validationContext = new ValidationContext(user);

            var result = new List<ValidationResult>();
            Validator.TryValidateObject(user, validationContext, result, true);
            
            Assert.Single(result);
            Assert.Equal("The Age field is required.", result[0].ErrorMessage);
        }
        
        [Fact]
        public void ShouldReturnValidationErrorIfAgeIsLessThanZero()
        {
            var user = UserInputDTOHelper.GenerateUserInputDTO(age: -1);
            var validationContext = new ValidationContext(user);

            var result = new List<ValidationResult>();
            Validator.TryValidateObject(user, validationContext, result, true);
            
            Assert.Single(result);
            Assert.Equal("The Age field should be equal or greater than 0.", result[0].ErrorMessage);
        }
        
        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        public void ShouldReturnSuccessValidationIfDependentsIsZeroOrGreater(int dependents)
        {
            var user = UserInputDTOHelper.GenerateUserInputDTO(dependents: dependents);
            var validationContext = new ValidationContext(user);

            var result = new List<ValidationResult>();
            Validator.TryValidateObject(user, validationContext, result, true);
            
            Assert.Empty(result);
        }
        
        [Fact]
        public void ShouldReturnValidationErrorIfDependentsIsNull()
        {
            var user = UserInputDTOHelper.GenerateUserInputDTO(dependents: null);
            var validationContext = new ValidationContext(user);

            var result = new List<ValidationResult>();
            Validator.TryValidateObject(user, validationContext, result, true);
            
            Assert.Single(result);
            Assert.Equal("The Dependents field is required.", result[0].ErrorMessage);
        }
        
        [Fact]
        public void ShouldReturnValidationErrorIfDependentsIsLessThanZero()
        {
            var user = UserInputDTOHelper.GenerateUserInputDTO(dependents: -1);
            var validationContext = new ValidationContext(user);

            var result = new List<ValidationResult>();
            Validator.TryValidateObject(user, validationContext, result, true);
            
            Assert.Single(result);
            Assert.Equal("The Dependents field should be equal or greater than 0.", result[0].ErrorMessage);
        }
        
        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        public void ShouldReturnSuccessValidationIfIncomeIsZeroOrGreater(int income)
        {
            var user = UserInputDTOHelper.GenerateUserInputDTO(income: income);
            var validationContext = new ValidationContext(user);

            var result = new List<ValidationResult>();
            Validator.TryValidateObject(user, validationContext, result, true);
            
            Assert.Empty(result);
        }
        
        [Fact]
        public void ShouldReturnValidationErrorIfIncomeIsNull()
        {
            var user = UserInputDTOHelper.GenerateUserInputDTO(income: null);
            var validationContext = new ValidationContext(user);

            var result = new List<ValidationResult>();
            Validator.TryValidateObject(user, validationContext, result, true);
            
            Assert.Single(result);
            Assert.Equal("The Income field is required.", result[0].ErrorMessage);
        }
        
        [Fact]
        public void ShouldReturnValidationErrorIfIncomeIsLessThanZero()
        {
            var user = UserInputDTOHelper.GenerateUserInputDTO(income: -1);
            var validationContext = new ValidationContext(user);

            var result = new List<ValidationResult>();
            Validator.TryValidateObject(user, validationContext, result, true);
            
            Assert.Single(result);
            Assert.Equal("The Income field should be equal or greater than 0.", result[0].ErrorMessage);
        }
        
        [Fact]
        public void ShouldReturnSuccessValidationIfRiskQuestionsHasThreeAnswers()
        {
            var user = UserInputDTOHelper.GenerateUserInputDTO();
            user.RiskQuestions = new[] {0, 0, 0};
            var validationContext = new ValidationContext(user);

            var result = new List<ValidationResult>();
            Validator.TryValidateObject(user, validationContext, result, true);
            
            Assert.Empty(result);
        }
        
        [Fact]
        public void ShouldReturnValidationErrorIfRiskAnswersArrayIsNull()
        {
            var user = UserInputDTOHelper.GenerateUserInputDTO();
            user.RiskQuestions = null;
            var validationContext = new ValidationContext(user);

            var result = new List<ValidationResult>();
            Validator.TryValidateObject(user, validationContext, result, true);
            
            Assert.Single(result);
            Assert.Equal("The RiskQuestions field is required.", result[0].ErrorMessage);
        }
        
        [Fact]
        public void ShouldReturnValidationErrorIfRiskAnswersArrayIsGreaterThanThree()
        {
            var user = UserInputDTOHelper.GenerateUserInputDTO();
            user.RiskQuestions = new[] {0, 0, 0, 0};
            var validationContext = new ValidationContext(user);

            var result = new List<ValidationResult>();
            Validator.TryValidateObject(user, validationContext, result, true);
            
            Assert.Single(result);
            Assert.Equal("The field RiskQuestions must be a string or array type with a maximum length of '3'.", result[0].ErrorMessage);
        }
        
        [Fact]
        public void ShouldReturnValidationErrorIfRiskAnswersArrayIsLessThanThree()
        {
            var user = UserInputDTOHelper.GenerateUserInputDTO();
            user.RiskQuestions = new[] {0, 0};
            var validationContext = new ValidationContext(user);

            var result = new List<ValidationResult>();
            Validator.TryValidateObject(user, validationContext, result, true);
            
            Assert.Single(result);
            Assert.Equal("The field RiskQuestions must be a string or array type with a minimum length of '3'.", result[0].ErrorMessage);
        }
        
        [Theory]
        [InlineData(MaritalStatusConstant.Married)]
        [InlineData(MaritalStatusConstant.Single)]
        public void ShouldReturnSuccessValidationIfMaritalStatusIsOneOfValidValues(string maritalStatus)
        {
            var user = UserInputDTOHelper.GenerateUserInputDTO(maritalStatus: maritalStatus);
            var validationContext = new ValidationContext(user);

            var result = new List<ValidationResult>();
            Validator.TryValidateObject(user, validationContext, result, true);
            
            Assert.Empty(result);
        }
        
        [Theory]
        [InlineData("not married")]
        [InlineData("relationship")]
        [InlineData("boyfriend")]
        public void ShouldReturnValidationErrorIfMaritalStatusIsInvalid(string maritalStatus)
        {
            var user = UserInputDTOHelper.GenerateUserInputDTO(maritalStatus: maritalStatus);
            var validationContext = new ValidationContext(user);

            var result = new List<ValidationResult>();
            Validator.TryValidateObject(user, validationContext, result, true);
            
            Assert.Single(result);
            Assert.Equal("Please enter one of the valid values: single, married.", result[0].ErrorMessage);
        }
        
        [Theory]
        [InlineData(HouseOwnershipStatusConstant.Mortgaged)]
        [InlineData(HouseOwnershipStatusConstant.Owned)]
        public void ShouldReturnSuccessValidationIfHouseOwnershipStatusIsOneOfValidValues(string houseOwnershipStatus)
        {
            var user = UserInputDTOHelper.GenerateUserInputDTO(houseOwnershipStatus: houseOwnershipStatus);
            var validationContext = new ValidationContext(user);

            var result = new List<ValidationResult>();
            Validator.TryValidateObject(user, validationContext, result, true);
            
            Assert.Empty(result);
        }
        
        [Theory]
        [InlineData("leased")]
        [InlineData("parents")]
        [InlineData("family")]
        public void ShouldReturnValidationErrorIfHouseOwnershipStatusIsInvalid(string houseOwnershipStatus)
        {
            var user = UserInputDTOHelper.GenerateUserInputDTO(houseOwnershipStatus: houseOwnershipStatus);
            var validationContext = new ValidationContext(user.House);

            var result = new List<ValidationResult>();
            Validator.TryValidateObject(user.House, validationContext, result, true);
            
            Assert.Single(result);
            Assert.Equal("Please enter one of the valid values: mortgaged, owned.", result[0].ErrorMessage);
        }
        
        [Fact]
        public void ShouldReturnSuccessValidationIfHouseIsNull()
        {
            var user = UserInputDTOHelper.GenerateUserInputDTO(houseOwnershipStatus: null);
            var validationContext = new ValidationContext(user);

            var result = new List<ValidationResult>();
            Validator.TryValidateObject(user, validationContext, result, true);
            
            Assert.Empty(result);
        }
        
        [Fact]
        public void ShouldReturnValidationErrorIfHouseIsNotNullButOwnershipStatusIsNull()
        {
            var user = UserInputDTOHelper.GenerateUserInputDTO();
            user.House = new HouseDTO
            {
                OwnershipStatus = null
            };
            var validationContext = new ValidationContext(user.House);

            var result = new List<ValidationResult>();
            Validator.TryValidateObject(user.House, validationContext, result, true);
            
            Assert.Single(result);
            Assert.Equal("The OwnershipStatus field is required.", result[0].ErrorMessage);
        }
        
        [Fact]
        public void ShouldReturnSuccessValidationIfVehicleIsNull()
        {
            var user = UserInputDTOHelper.GenerateUserInputDTO(vehicleYear: null);
            var validationContext = new ValidationContext(user);

            var result = new List<ValidationResult>();
            Validator.TryValidateObject(user, validationContext, result, true);
            
            Assert.Empty(result);
        }
        
        [Fact]
        public void ShouldReturnSuccessValidationIfVehicleYearIsEqualOrGreaterThanZero()
        {
            var user = UserInputDTOHelper.GenerateUserInputDTO(vehicleYear: 1);
            var validationContext = new ValidationContext(user);

            var result = new List<ValidationResult>();
            Validator.TryValidateObject(user, validationContext, result, true);
            
            Assert.Empty(result);
        }
        
        [Fact]
        public void ShouldReturnValidationErrorIfVehicleIsNotNullButYearIsNull()
        {
            var user = UserInputDTOHelper.GenerateUserInputDTO();
            user.Vehicle = new VehicleDTO()
            {
                Year = null
            };
            var validationContext = new ValidationContext(user.Vehicle);

            var result = new List<ValidationResult>();
            Validator.TryValidateObject(user.Vehicle, validationContext, result, true);
            
            Assert.Single(result);
            Assert.Equal("The Year field is required.", result[0].ErrorMessage);
        }
        
        [Fact]
        public void ShouldReturnValidationErrorIfVehicleYearIsLessThanZero()
        {
            var user = UserInputDTOHelper.GenerateUserInputDTO(vehicleYear: -1);
            var validationContext = new ValidationContext(user.Vehicle);

            var result = new List<ValidationResult>();
            Validator.TryValidateObject(user.Vehicle, validationContext, result, true);
            
            Assert.Single(result);
            Assert.Equal("The Year field should be a positive number.", result[0].ErrorMessage);
        }
    }
}