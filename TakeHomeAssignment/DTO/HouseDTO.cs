using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using TakeHomeAssignment.Helpers.Attributes;
using TakeHomeAssignment.Helpers.Constants;

namespace TakeHomeAssignment.DTO
{
    public class HouseDTO
    {
        [JsonPropertyName("ownership_status")]
        [Required]
        [StringRange(ValidValues = new[] {HouseOwnershipStatusConstant.Mortgaged, HouseOwnershipStatusConstant.Owned})]
        public string OwnershipStatus { get; set; }
    }
}