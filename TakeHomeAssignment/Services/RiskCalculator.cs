using System;
using System.Collections.Generic;
using System.Linq;
using TakeHomeAssignment.Helpers.Constants;
using TakeHomeAssignment.Models;

namespace TakeHomeAssignment.Services
{
    public class RiskCalculator
    {
        private User User { get; }
        public List<InsuranceLineScore> Scores { get; }

        public RiskCalculator(User user)
        {
            Scores = new List<InsuranceLineScore>
            {
                new (InsuranceLineConstant.Auto),
                new (InsuranceLineConstant.Disability),
                new (InsuranceLineConstant.Home),
                new (InsuranceLineConstant.Life)
            };

            User = user;
            Calculate();
        }

        private InsuranceLineScore FindInsuranceLineScore(string insuranceLine)
        {
            return Scores.FirstOrDefault(scores => scores.InsuranceLine == insuranceLine);
        }

        private void Calculate()
        {
            foreach (var riskQuestion in User.RiskQuestions)
                if (riskQuestion > 0)
                    Scores.ForEach(score => score.AddScore(1));
            
            if (User.Income == 0)
                FindInsuranceLineScore(InsuranceLineConstant.Disability).SetIneligible(true);
            
            if (User.Vehicle == null)
                FindInsuranceLineScore(InsuranceLineConstant.Auto).SetIneligible(true);
            
            if (User.House == null)
                FindInsuranceLineScore(InsuranceLineConstant.Home).SetIneligible(true);

            switch (User.Age)
            {
                case > 60:
                    FindInsuranceLineScore(InsuranceLineConstant.Disability).SetIneligible(true);
                    FindInsuranceLineScore(InsuranceLineConstant.Life).SetIneligible(true);
                    break;
                case < 30:
                    Scores.ForEach(score => score.DeductScore(2));
                    break;
                case >= 30 and <= 40:
                    Scores.ForEach(score => score.DeductScore(1));
                    break;
            }

            if (User.Income > 200000)
                Scores.ForEach(score => score.DeductScore(1));

            if (User.House is {OwnershipStatus: HouseOwnershipStatusConstant.Mortgaged})
            {
                FindInsuranceLineScore(InsuranceLineConstant.Home).AddScore(1);
                FindInsuranceLineScore(InsuranceLineConstant.Disability).AddScore(1);
            }

            if (User.Dependents > 0)
            {
                FindInsuranceLineScore(InsuranceLineConstant.Disability).AddScore(1);
                FindInsuranceLineScore(InsuranceLineConstant.Life).AddScore(1);
            }

            if (User.MaritalStatus == MaritalStatusConstant.Married)
            {
                FindInsuranceLineScore(InsuranceLineConstant.Life).AddScore(1);
                FindInsuranceLineScore(InsuranceLineConstant.Disability).DeductScore(1);
            }

            if (User.Vehicle != null && User.Vehicle.Year >= DateTime.Now.AddYears(-5).Year)
                FindInsuranceLineScore(InsuranceLineConstant.Auto).AddScore(1);
        }

        public RiskOutput GenerateOutput()
        {
            return new RiskOutput(FindInsuranceLineScore(InsuranceLineConstant.Auto).FinalScore,
                                        FindInsuranceLineScore(InsuranceLineConstant.Disability).FinalScore,
                                        FindInsuranceLineScore(InsuranceLineConstant.Home).FinalScore,
                                        FindInsuranceLineScore(InsuranceLineConstant.Life).FinalScore);
        }
    }
}