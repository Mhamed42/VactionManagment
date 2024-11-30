using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VactionManagment.Data;
using VactionManagment.Models;

namespace VactionManagment.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly VacationDbContext _vacation;

        public EmployeesController(VacationDbContext vacation)
        {
            _vacation = vacation;
        }
        public IActionResult Employees()
        {
            return View(_vacation.Employees.Include(x => x.Department).OrderBy(x => x.Name).ToList());

        }
        public IActionResult Create()
        {
            ViewBag.Departments = _vacation.Departments.OrderBy(x => x.Name).ToList();
            
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Employee model)
        {
            if (ModelState.IsValid)
            {
                _vacation.Employees.Add(model);
                _vacation.SaveChanges();
                return RedirectToAction("Employees");
            }

            return View(model);
        }
        public IActionResult Edit(int? Id)
        {

            return View(_vacation.Employees.FirstOrDefault(x => x.Id == Id));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Employee model)
        {
            if (ModelState.IsValid)
            {
                _vacation.Employees.Update(model);
                _vacation.SaveChanges();
                return RedirectToAction("Employees");
            }

            return View(model);
        }
        public IActionResult Delete(int? Id)
        {

            return View(_vacation.Employees.FirstOrDefault(x => x.Id == Id));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Employee model)
        {
            if (model != null)
            {
                _vacation.Employees.Remove(model);
                _vacation.SaveChanges();
                return RedirectToAction("Employees");
            }

            return View(model);
        }
    }
}
