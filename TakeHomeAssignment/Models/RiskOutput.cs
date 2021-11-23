using TakeHomeAssignment.DTO;
using TakeHomeAssignment.Helpers.Constants;

namespace TakeHomeAssignment.Models
{
    public class RiskOutput
    {
        public string Auto { get; }
        public string Disability { get; }
        public string Home { get; }
        public string Life { get; }

        public RiskOutput(string auto,
            string disability,
            string home,
            string life)
        {
            Auto = auto;
            Disability = disability;
            Home = home;
            Life = life;
        }

        public RiskOutputDTO GenerateDTO()
        {
            return new RiskOutputDTO
            {
                Auto = Auto,
                Disability = Disability,
                Home = Home,
                Life = Life
            };
        }
    }
}