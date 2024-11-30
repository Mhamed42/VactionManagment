using Microsoft.AspNetCore.Mvc;
using VactionManagment.Data;
using VactionManagment.Models;

namespace VactionManagment.Controllers
{
	public class VacationTypesController : Controller
	{
		private readonly VacationDbContext _vacation;

		public VacationTypesController(VacationDbContext vacation)
		{
			_vacation = vacation;
		}
		public IActionResult VacationTypes()
		{
			return View(_vacation.VacationTypes.OrderBy(x=>x.VacationName).ToList());
		}
        public IActionResult Create()
        {
           // ViewBag.Departments = _vacation.Departments.OrderBy(x => x.Name).ToList();

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(VacationType model)
        {
            var result = _vacation.VacationTypes.FirstOrDefault(x => x.VacationName.Contains(model.VacationName.ToLower()));
            if (result==null)
            {
                _vacation.VacationTypes.Add(model);
                _vacation.SaveChanges();
                return RedirectToAction("VacationTypes");
            }
            ViewBag.ErrorMsg = false;

            return View(model);
        }
        public IActionResult Edit( int? id)
        {
            // ViewBag.Departments = _vacation.Departments.OrderBy(x => x.Name).ToList();

            return View(_vacation.VacationTypes.FirstOrDefault(x=>x.Id==id));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(VacationType model)
        {
            if (ModelState.IsValid)
            {
                _vacation.VacationTypes.Update(model);
                _vacation.SaveChanges();
                return RedirectToAction("VacationTypes");
            }

            return View(model);
        }
        public IActionResult Delete(int? id)
        {
           

            return View(_vacation.VacationTypes.FirstOrDefault(x => x.Id == id));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(VacationType model)
        {
            if (model !=null)
            {
                _vacation.VacationTypes.Remove(model);
                _vacation.SaveChanges();
                return RedirectToAction("VacationTypes");
            }

            return View(model);
        }
    }
}
