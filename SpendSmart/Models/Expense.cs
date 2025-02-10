using System.ComponentModel.DataAnnotations;

namespace SpendSmart.Models
{
    public class Expense
    {

        public int Id { get; set; }


        [Required(ErrorMessage = "Value is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Value must be greater than 0.")]
        public decimal Value { get; set; }


        [Required(ErrorMessage = "Description is required.")]
        [StringLength(100, ErrorMessage = "Description cannot exceed 100 characters.")]
        public string Description { get; set; }

    }
}
