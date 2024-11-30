using Microsoft.AspNetCore.Mvc;
using VactionManagment.Data;
using VactionManagment.Models;

namespace VactionManagment.Controllers
{
    public class DepartmentsController : Controller
    {
        private readonly VacationDbContext _vacationDb;

        public DepartmentsController(VacationDbContext vacationDb)
        {
            _vacationDb = vacationDb;
        }
        public IActionResult Departments()
        {
            return View(_vacationDb.Departments.OrderBy(x => x.Name).ToList());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Department model)
        {
            if (ModelState.IsValid)
            {
                _vacationDb.Departments.Add(model);
                _vacationDb.SaveChanges();
                return RedirectToAction("Departments");
            }

            return View(model);
        }
        public IActionResult Edit(int? Id)
        {

            return View(_vacationDb.Departments.FirstOrDefault(x => x.Id == Id));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Department model)
        {
            if (ModelState.IsValid)
            {
                _vacationDb.Departments.Update(model);
                _vacationDb.SaveChanges();
                return RedirectToAction("Departments");
            }

            return View(model);
        }
        public IActionResult Delete(int? Id)
        {

            return View(_vacationDb.Departments.FirstOrDefault(x => x.Id == Id));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Department model)
        {
            if (model != null)
            {
                _vacationDb.Departments.Remove(model);
                _vacationDb.SaveChanges();
                return RedirectToAction("Departments");
            }

            return View(model);
        }
    }
}
