using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using TakeHomeAssignment.Helpers.Attributes;
using TakeHomeAssignment.Helpers.Constants;

namespace TakeHomeAssignment.DTO
{
    public class UserInputDTO
    {
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "The Age field should be equal or greater than 0.")]
        public int? Age { get; set; }
        
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "The Dependents field should be equal or greater than 0.")]
        public int? Dependents { get; set; }
        
        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "The Income field should be equal or greater than 0.")]
        public double? Income { get; set; }
        
        [JsonPropertyName("marital_status")]
        [Required]
        [StringRange(ValidValues = new[] {MaritalStatusConstant.Single, MaritalStatusConstant.Married})]
        public string MaritalStatus { get; set; }
        
        [JsonPropertyName("risk_questions")]
        [Required]
        [MaxLength(3)]
        [MinLength(3)]
        public IEnumerable<int> RiskQuestions { get; set; }
        
        public HouseDTO House { get; set; }
        public VehicleDTO Vehicle { get; set; }
    }
}