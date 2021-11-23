using System.ComponentModel.DataAnnotations;

namespace TakeHomeAssignment.DTO
{
    public class VehicleDTO
    {
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "The Year field should be a positive number.")]
        public int? Year { get; set; }
    }
}