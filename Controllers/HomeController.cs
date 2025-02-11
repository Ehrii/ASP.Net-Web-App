using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SpendSmart.Models;

namespace SpendSmart.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SpendSmartDBContext _context;

        public HomeController(ILogger<HomeController> logger, SpendSmartDBContext context)
        {
            _logger = logger;
            _context = context;
        }
        //ACTION BUTTONS
        public IActionResult DeleteExpense(int id)
        {
            var expenseInDb = _context.Expenses.SingleOrDefault(expense => expense.Id == id);
            _context.Expenses.Remove(expenseInDb);
            _context.SaveChanges();
            return RedirectToAction("Expenses");
        }

        public IActionResult DeleteHabits(int id)
        {
            var expenseInDb = _context.NumberofHabits.SingleOrDefault(habits => habits.Id == id);
            _context.NumberofHabits.Remove(expenseInDb);
            _context.SaveChanges();
            return RedirectToAction("HabitTracker");
        }

        public IActionResult CreateEditExpense(int? id)
        {
            if (id != null)
            {
                var expenseInDb = _context.Expenses.SingleOrDefault(expense => expense.Id == id);
                return View(expenseInDb);
            }

            return View();
        }

        // public IActionResult CreateEditHabits(int? id)
        // {
        //     if (id != null)
        //     {
        //         var habitsInDb = _context.NumberofHabits.SingleOrDefault(habits => habits.Id == id);
        //         return View("HabitTracker", habitsInDb);
        //     }

        //     return RedirectToAction("HabitTracker");
        // }





        //EXPENSE VIEW
        public IActionResult Expenses()
        {
            var AllExpenses = _context.Expenses.ToList();
            var totalExpenses = AllExpenses.Sum(x => x.Value);
            ViewBag.Expenses = totalExpenses;

            return View(AllExpenses);
        }
        //EXPENSE VIEW
        public IActionResult HabitTracker()
        {
            var habitItems = _context.NumberofHabits?.ToList() ?? new List<Habits>();

            var viewModel = new HabitTrackerViewModel
            {
                HabitItems = habitItems,
                HabitFormModel = new Habits() // Empty form model for creating new habits
            };

            return View(viewModel);
        }


        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        // FORM ACTION
        [HttpPost]
        public IActionResult CreateEditHabitsForm(HabitTrackerViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                Console.WriteLine("Model is NOT valid!");
                return View("HabitTracker", viewModel); // Reload the page with errors
            }
            if (viewModel.HabitFormModel.Id == 0)
            {
                // Create new habit
                var newHabit = new Habits
                {
                    HabitName = viewModel.HabitFormModel.HabitName,
                    DueDate = viewModel.HabitFormModel.DueDate
                };

                _context.NumberofHabits.Add(newHabit);
            }
            else
            {
                // Update existing habit
                var existingHabit = _context.NumberofHabits.Find(viewModel.HabitFormModel.Id);
                if (existingHabit != null)
                {
                    existingHabit.HabitName = viewModel.HabitFormModel.HabitName;
                    existingHabit.DueDate = viewModel.HabitFormModel.DueDate;

                    _context.NumberofHabits.Update(existingHabit);
                }
                else
                {
                    Console.WriteLine("Habit not found!");
                    return View("HabitTracker", viewModel); // Return view if update fails
                }
            }
            _context.SaveChanges();
            return RedirectToAction("HabitTracker"); // Redirect to the main page
        }

        public IActionResult CreateEditExpenseForm(Expense model)
        {
            if (!ModelState.IsValid)
            {
                Console.WriteLine("Model is NOT valid!");
                return View("Expenses", model); // Reload the page with errors
            }

            if (model.Id == 0)
            {
                _context.Expenses.Add(model);
            }
            else
            {
                _context.Expenses.Update(model);
            }

            _context.SaveChanges();
            return RedirectToAction("Expenses");
        }

        // TRACKER VIEW
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
