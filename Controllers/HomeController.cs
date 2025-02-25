using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SpendSmart.Models;

namespace SpendSmart.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SpendSmartDBContext _context;


        private readonly Dictionary<string, decimal> _exchangeRates = new()
         {
        { "USD_EUR", 0.92m },
        { "EUR_USD", 1.09m },
        { "USD_GBP", 0.78m },
        { "GBP_USD", 1.28m },
        { "USD_PHP", 58.00m },
        { "PHP_USD", 0.018m }
        };


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
            var habitsInDb = _context.NumberofHabits.SingleOrDefault(habits => habits.Id == id);
            _context.NumberofHabits.Remove(habitsInDb);
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


        public IActionResult CreateEditHabits(int? id)
        {

            return View();

        }

        public IActionResult ConvertCurrency(string from, string to, decimal amount)
        {
            string key = $"{from.ToUpper()}_{to.ToUpper()}";
            if (_exchangeRates.TryGetValue(key, out decimal rate))
            {
                decimal convertedAmount = amount * rate;
                ViewBag.Result = $"{amount} {from} = {convertedAmount} {to}";
            }

            else
            {
                ViewBag.Result = "Exchange rate not available.";
            }
            return View("CurrencyConverter");
        }

        public IActionResult CurrencyConverter()
        {
            return View();
        }


        public IActionResult ViewProduct()
        {
            return View();
        }


        public IActionResult Search(string queryString)
        {
            var results = string.IsNullOrEmpty(queryString)
                ? _context.Expenses.ToList()
                : _context.Expenses.Where(t => t.Description.Contains(queryString)).ToList();

            return View("Expenses", results);
        }



        public IActionResult Expenses()
        {
            var AllExpenses = _context.Expenses?.ToList() ?? new List<Expense>();
            var totalExpenses = AllExpenses.Sum(x => x.Value);
            ViewBag.Expenses = totalExpenses;

            return View(AllExpenses);
        }
        //EXPENSE VIEW
        public IActionResult HabitTracker(DateTime? selectedDate)
        {
            var habitsQuery = _context.NumberofHabits.AsQueryable();

            if (selectedDate.HasValue)
            {
                DateOnly selectedDateOnly = DateOnly.FromDateTime(selectedDate.Value);
                habitsQuery = habitsQuery.Where(h => h.DueDate >= selectedDateOnly);
            }

            var viewModel = new HabitTrackerViewModel
            {
                HabitItems = habitsQuery.ToList(), // Assign filtered list here
                HabitFormModel = new Habits() // Empty form model for creating new habits
            };

            return View(viewModel); // Pass only ONE model
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

            if (viewModel.HabitFormModel.TimeIn >= viewModel.HabitFormModel.TimeOut)
            {
                ModelState.AddModelError("HabitFormModel.TimeOut", "Time Out must be later than Time In.");
                viewModel.HabitItems = _context.NumberofHabits.ToList();
                return View("HabitTracker", viewModel);
            }


            if (viewModel.HabitFormModel.Id == 0)
            {
                // Create new habit
                var newHabit = new Habits
                {
                    HabitName = viewModel.HabitFormModel.HabitName,
                    DueDate = viewModel.HabitFormModel.DueDate,
                    TimeIn = viewModel.HabitFormModel.TimeIn,
                    TimeOut = viewModel.HabitFormModel.TimeOut,

                };
                _context.NumberofHabits.Add(newHabit);
            }
            else
            {
                var existingHabit = _context.NumberofHabits.Find(viewModel.HabitFormModel.Id);
                if (existingHabit != null)
                {
                    existingHabit.HabitName = viewModel.HabitFormModel.HabitName;
                    existingHabit.DueDate = viewModel.HabitFormModel.DueDate;
                    existingHabit.TimeIn = viewModel.HabitFormModel.TimeIn;
                    existingHabit.TimeOut = viewModel.HabitFormModel.TimeOut;

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


        public IActionResult Marketplace()
        {
            return View();
        }


        public IActionResult CreateEditExpenseForm(Expense model)
        {
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
