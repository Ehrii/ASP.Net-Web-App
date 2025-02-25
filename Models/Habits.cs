using System.ComponentModel.DataAnnotations;

namespace SpendSmart.Models
{
    public class Habits {
        public int Id { get; set; }

        [Required(ErrorMessage = "Habit name is required")]
        public string HabitName { get; set; } = ""; // Make sure this matches the form

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Due Date is required.")]
        public DateOnly? DueDate { get; set; } = null;


        [DataType(DataType.Time)]
        [Required(ErrorMessage = "Time In is required.")]
        public TimeOnly? TimeIn { get; set; } = null;

        [DataType(DataType.Time)]
        [Required(ErrorMessage = "Time Out is required.")]
        public TimeOnly? TimeOut { get; set; } = null;
    }
}
