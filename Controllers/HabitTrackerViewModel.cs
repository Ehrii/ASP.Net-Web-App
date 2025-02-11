using System.Collections.Generic;
namespace SpendSmart.Models
{
    public class HabitTrackerViewModel
    {
        public List<Habits> HabitItems { get; set; } = new List<Habits>();
        public Habits HabitFormModel { get; set; } = new Habits(); // Ensure it's initialized


        public int Id { get; set; }
        public string HabitName { get; set; } = "";
        public DateTime? DueDate { get; set; } = null;
    }
}