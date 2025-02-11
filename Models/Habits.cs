using System.ComponentModel.DataAnnotations;

namespace SpendSmart.Models
{
    public class Habits
    {

        public int Id { get; set; }

        [Required(ErrorMessage = "Habit name is required")]
        public string HabitName { get; set; } = ""; // Make sure this matches the form

        [DataType(DataType.Date)]
        public DateTime? DueDate { get; set; } = null;
    }
}
