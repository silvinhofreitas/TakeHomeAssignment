using System.Collections.Generic;
using TakeHomeAssignment.DTO;

namespace TakeHomeAssignment.Models
{
    public class User
    {
        public int Age { get; }
        public int Dependents { get; }
        public double Income { get; }
        public House House { get; }
        public Vehicle Vehicle { get; }
        public string MaritalStatus { get; }
        public IEnumerable<int> RiskQuestions { get; }

        public User(UserInputDTO input)
        {
            Age = (int)input.Age;
            Dependents = (int)input.Dependents;
            Income = (double)input.Income;
            MaritalStatus = input.MaritalStatus;
            RiskQuestions = input.RiskQuestions;

            if (input.House != null)
                House = new House(input.House.OwnershipStatus);

            if (input.Vehicle != null)
                Vehicle = new Vehicle((int)input.Vehicle.Year);
        }
    }
}