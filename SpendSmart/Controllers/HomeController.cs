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

        public IActionResult CreateEditExpense(int? id)
        {
            if (id != null)
            {
                var expenseInDb = _context.Expenses.SingleOrDefault(expense => expense.Id == id);
                return View(expenseInDb);
            }

            return View();
        }

        //EXPENSE VIEW
        public IActionResult Expenses()
        {
            var AllExpenses = _context.Expenses.ToList();

            var totalExpenses = AllExpenses.Sum(x => x.Value);

            ViewBag.Expenses = totalExpenses;

            return View(AllExpenses);
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
        public IActionResult CreateEditExpenseForm(Expense model)
        {

            if (!ModelState.IsValid)
            {

                TempData["ErrorMessage"] = "Please fix the validation errors before submitting the form.";

                return RedirectToAction("CreateEditExpense");
            }

            if (model.Id==0)
            {
                //Create
                _context.Expenses.Add(model);
            }
            else
            {
                //Editing
                _context.Expenses.Update(model);
            }

            _context.SaveChanges();

            return RedirectToAction("Expenses");
        }




        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
