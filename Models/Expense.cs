using System.ComponentModel.DataAnnotations;

namespace SpendSmart.Models
{
    public class Expense
    {

        public int Id { get; set; }

        [Required]
        public double Value { get; set; }

        [Required]
        [MaxLength(100)]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Only letters are allowed.")]
        public string Description { get; set; } = "";
    }
}
