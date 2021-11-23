using System;
using System.Collections.Generic;
using System.Linq;
using TakeHomeAssignment.Helpers.Constants;
using TakeHomeAssignment.Models;
using TakeHomeAssignment.Services;
using TakeHomeAssignment.test.TestHelpers;
using Xunit;
// ReSharper disable PossibleNullReferenceException

namespace TakeHomeAssignment.Test.Services
{
    public class RiskCalculatorTest
    {
        private List<string> insuranceLineList = new List<string>
        {
            InsuranceLineConstant.Auto,
            InsuranceLineConstant.Disability,
            InsuranceLineConstant.Home,
            InsuranceLineConstant.Life
        };

        [Theory]
        [InlineData(new[] {1, 0, 1}, 29, 0, 25000, "owned", 2005, "single", new[] {0, 0, 0, 0}, new [] {RiskScoreConstant.Economic, RiskScoreConstant.Economic, RiskScoreConstant.Economic, RiskScoreConstant.Economic})]
        [InlineData(new[] {1, 1, 1}, 56, 2, 340000, "mortgaged", 1998, "married", new[] {2, 3, 3, 4}, new[] {RiskScoreConstant.Regular, RiskScoreConstant.Responsible, RiskScoreConstant.Responsible, RiskScoreConstant.Responsible})]
        [InlineData(new[] {0, 0, 1}, 32, 0, 193000, null, null, "single", new[] {0, 0, 0, 0}, new[] {RiskScoreConstant.Ineligible, RiskScoreConstant.Economic, RiskScoreConstant.Ineligible, RiskScoreConstant.Economic})]
        [InlineData(new[] {0, 0, 0}, 61, 1, 320000, "owned", null, "married", new[] {-1, -1, -1, 1}, new[] {RiskScoreConstant.Ineligible, RiskScoreConstant.Ineligible, RiskScoreConstant.Economic, RiskScoreConstant.Ineligible})]
        public void ShouldCalculateTheRiskCorrectly(int[] riskQuestions,
            int age,
            int dependents,
            double income,
            string houseOwnershipStatus,
            int? vehicleYear,
            string maritalStatus,
            int[] expectedScore,
            string[] expectedFinalScore)
        {
            var user = UserInputDTOHelper.GenerateUserInputDTO(age,
                                                               dependents,
                                                               income,
                                                               houseOwnershipStatus,
                                                               vehicleYear,
                                                               maritalStatus,
                                                               riskQuestions[0],
                                                               riskQuestions[1],
                                                               riskQuestions[2]);

            var calculator = new RiskCalculator(new User(user));

            for (var i = 0; i < insuranceLineList.Count; i++)
            {
                Assert.Equal(expectedScore[i], calculator.Scores
                    .FirstOrDefault(scores => scores.InsuranceLine == insuranceLineList[i]).Score);
                
                Assert.Equal(expectedFinalScore[i], calculator.Scores
                    .FirstOrDefault(scores => scores.InsuranceLine == insuranceLineList[i]).FinalScore);
            }
                
        }
        
        [Fact]
        public void IfUserDoesNotHaveIncome_ThenIsIneligibleForDisability()
        {
            var user = UserInputDTOHelper.GenerateUserInputDTO(income: 0);
            var calculator = new RiskCalculator(new User(user));
            
            Assert.Equal(RiskScoreConstant.Ineligible, calculator.Scores
                .FirstOrDefault(scores => scores.InsuranceLine == InsuranceLineConstant.Disability).FinalScore);
        }
        
        [Fact]
        public void IfUserDoesNotHaveVehicle_ThenIsIneligibleForAuto()
        {
            var user = UserInputDTOHelper.GenerateUserInputDTO(vehicleYear: null);
            var calculator = new RiskCalculator(new User(user));
            
            Assert.Equal(RiskScoreConstant.Ineligible, calculator.Scores
                .FirstOrDefault(scores => scores.InsuranceLine == InsuranceLineConstant.Auto).FinalScore);
        }
        
        [Fact]
        public void IfUserDoesNotHaveHouse_ThenIsIneligibleForHome()
        {
            var user = UserInputDTOHelper.GenerateUserInputDTO(houseOwnershipStatus: null);
            var calculator = new RiskCalculator(new User(user));
            
            Assert.Equal(RiskScoreConstant.Ineligible, calculator.Scores
                .FirstOrDefault(scores => scores.InsuranceLine == InsuranceLineConstant.Home).FinalScore);
        }

        [Fact]
        public void IfUserOverSixtyYearsOld_ThenIsIneligibleForDisabilityAndLifeInsurance()
        {
            var user = UserInputDTOHelper.GenerateUserInputDTO(age: 61);
            var calculator = new RiskCalculator(new User(user));
            
            Assert.Equal(RiskScoreConstant.Ineligible, calculator.Scores
                .FirstOrDefault(scores => scores.InsuranceLine == InsuranceLineConstant.Disability).FinalScore);
            
            Assert.Equal(RiskScoreConstant.Ineligible, calculator.Scores
                .FirstOrDefault(scores => scores.InsuranceLine == InsuranceLineConstant.Life).FinalScore);
        }
        
        [Fact]
        public void IfUserUnderThirtyYearsOld_ThenDeductTwoPointsFromAllLines()
        {
            var user = UserInputDTOHelper.GenerateUserInputDTO(age: 29);
            var calculator = new RiskCalculator(new User(user));

            foreach (var insuranceLine in insuranceLineList)
                Assert.Equal(-2, calculator.Scores
                    .FirstOrDefault(scores => scores.InsuranceLine == insuranceLine).Score);
        }
        
        [Fact]
        public void IfUserBetweenThirtyAndFortyYearsOld_ThenDeductOnePointFromAllLines()
        {
            var user = UserInputDTOHelper.GenerateUserInputDTO(age: 35);
            var calculator = new RiskCalculator(new User(user));

            foreach (var insuranceLine in insuranceLineList)
                Assert.Equal(-1, calculator.Scores
                    .FirstOrDefault(scores => scores.InsuranceLine == insuranceLine).Score);
        }
        
        [Fact]
        public void IfUserAboveFortyYearsOld_ThenWillNotDeductPointFromAllLines()
        {
            var user = UserInputDTOHelper.GenerateUserInputDTO(age: 41);
            var calculator = new RiskCalculator(new User(user));

            foreach (var insuranceLine in insuranceLineList)
                Assert.Equal(0, calculator.Scores
                    .FirstOrDefault(scores => scores.InsuranceLine == insuranceLine).Score);
        }
        
        [Fact]
        public void IfUserIncomeAbove200K_ThenDeductOnePointFromAllLines()
        {
            var user = UserInputDTOHelper.GenerateUserInputDTO(income: 201000);
            var calculator = new RiskCalculator(new User(user));

            foreach (var insuranceLine in insuranceLineList)
                Assert.Equal(-1, calculator.Scores
                    .FirstOrDefault(scores => scores.InsuranceLine == insuranceLine).Score);
        }
        
        [Fact]
        public void IfUserHouseMortgaged_ThenAddsOnePointToHomeAndDisability()
        {
            var user = UserInputDTOHelper.GenerateUserInputDTO(houseOwnershipStatus: HouseOwnershipStatusConstant.Mortgaged);
            var calculator = new RiskCalculator(new User(user));

            Assert.Equal(1, calculator.Scores
                .FirstOrDefault(scores => scores.InsuranceLine == InsuranceLineConstant.Home).Score);
            
            Assert.Equal(1, calculator.Scores
                .FirstOrDefault(scores => scores.InsuranceLine == InsuranceLineConstant.Disability).Score);
        }
        
        [Fact]
        public void IfUserHasDependents_ThenAddsOnePointToDisabilityAndLife()
        {
            var user = UserInputDTOHelper.GenerateUserInputDTO(dependents: 2);
            var calculator = new RiskCalculator(new User(user));

            Assert.Equal(1, calculator.Scores
                .FirstOrDefault(scores => scores.InsuranceLine == InsuranceLineConstant.Disability).Score);
            
            Assert.Equal(1, calculator.Scores
                .FirstOrDefault(scores => scores.InsuranceLine == InsuranceLineConstant.Life).Score);
        }
        
        [Fact]
        public void IfUserIsMarried_ThenAddsOnePointToLifeAndDeductOnePointFromDisability()
        {
            var user = UserInputDTOHelper.GenerateUserInputDTO(maritalStatus: MaritalStatusConstant.Married);
            var calculator = new RiskCalculator(new User(user));

            Assert.Equal(1, calculator.Scores
                .FirstOrDefault(scores => scores.InsuranceLine == InsuranceLineConstant.Life).Score);
            
            Assert.Equal(-1, calculator.Scores
                .FirstOrDefault(scores => scores.InsuranceLine == InsuranceLineConstant.Disability).Score);
        }
        
        [Fact]
        public void IfUserVehicleHasProducedLastFiveYears_ThenAddsOnePointToVehicle()
        {
            var user = UserInputDTOHelper.GenerateUserInputDTO(vehicleYear: DateTime.Now.AddYears(-3).Year);
            var calculator = new RiskCalculator(new User(user));

            Assert.Equal(1, calculator.Scores
                .FirstOrDefault(scores => scores.InsuranceLine == InsuranceLineConstant.Auto).Score);
        }
    }
}