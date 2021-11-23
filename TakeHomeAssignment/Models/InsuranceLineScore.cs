using TakeHomeAssignment.Helpers.Constants;

namespace TakeHomeAssignment.Models
{
    public class InsuranceLineScore
    {
        private bool Ineligible;
        public string InsuranceLine { get; }
        public int Score { get; private set; }

        public string FinalScore
        {
            get
            {
                if (Ineligible)
                    return RiskScoreConstant.Ineligible;
                
                return Score switch
                {
                    <= 0 => RiskScoreConstant.Economic,
                    >= 3 => RiskScoreConstant.Responsible,
                    _ => RiskScoreConstant.Regular
                };
            }
        }

        public InsuranceLineScore(string insuranceLine)
        {
            InsuranceLine = insuranceLine;
            Score = 0;
            Ineligible = false;
        }

        public int AddScore(int value)
        {
            Score += value;
            return Score;
        }

        public int DeductScore(int value)
        {
            Score -= value;
            return Score;
        }

        public void SetIneligible(bool ineligible)
        {
            Ineligible = ineligible;
        }
    }
}