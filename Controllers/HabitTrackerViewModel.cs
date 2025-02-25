using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace SpendSmart.Models
{
    public class HabitTrackerViewModel
    {
        public List<Habits> HabitItems { get; set; } = new List<Habits>();
        public Habits HabitFormModel { get; set; } = new Habits(); // Ensure it's initialized

        public int Id { get; set; }

        [Required(ErrorMessage = "Habit name is required")]
        public string HabitName { get; set; } = "";

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