namespace TakeHomeAssignment.Models
{
    public class House
    {
        public string OwnershipStatus { get; }

        public House(string ownershipStatus)
        {
            OwnershipStatus = ownershipStatus;
        }
    }
}