using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using VactionManagment.Data;
using VactionManagment.Models;

namespace VactionManagment.Controllers
{
    public class VacationPlansController : Controller
    {
        private readonly VacationDbContext _vacationDb;

        public VacationPlansController(VacationDbContext vacationDb)
        {
            _vacationDb = vacationDb;
        }
        public IActionResult Index()
        {
            
            return View(_vacationDb.RequestVacations.Include(x => x.Employee).
                OrderByDescending(x => x.RequestDate).
                ToList());
        }

        public IActionResult Create()
        {
            return View();
        }
        [ HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(VacationPlan model, int[] DayOfWeekCheckbox)
        {
            if (ModelState.IsValid)
            {
                var results = _vacationDb.VacationPlans.
                    Where(x => x.requestVacation.EmployeeId == model.requestVacation.EmployeeId
                    && x.VacationDate >= model.requestVacation.StartDate 
                    && x.VacationDate <= model.requestVacation.EndDate).FirstOrDefault();
                if (results!=null)
                {
                    ViewBag.ErrorVaction = false;
                    return View(model);
                }
                for (DateTime Data =model.requestVacation.StartDate; Data <= model.requestVacation.EndDate; Data=Data.AddDays(1))
                {
                    if (Array.IndexOf(DayOfWeekCheckbox,(int)Data.DayOfWeek)!=-1)
                    {
                        model.Id=0;
                        model.VacationDate = Data;
                        model.requestVacation.RequestDate=DateTime.Now;
                        _vacationDb.VacationPlans.Add(model);
                        _vacationDb.SaveChanges();
                    }

                }
                return RedirectToAction("Index");   
            }
            return View(model);
        }

        public IActionResult Edit(int?id)
        {
            ViewBag.Employees=_vacationDb.Employees.OrderBy(x=>x.Name).ToList();
            ViewBag.VacationType = _vacationDb.VacationTypes.OrderBy(x => x.VacationName).ToList();
            return View(_vacationDb.RequestVacations.
                Include(x=>x.Employee).
                Include(x=>x.vacationType).
                Include(x=>x.vacationPlansList).
                FirstOrDefault(x=>x.Id==id));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(RequestVacation model)
        {
            if (ModelState.IsValid)
            {
                if (model.Approved ==true)
                {
                    model.DateApproved = DateTime.Now;  
                    _vacationDb.RequestVacations.Update(model);
                    _vacationDb.SaveChanges();
                    return RedirectToAction("Index");
                }

            }
            ViewBag.Employees = _vacationDb.Employees.OrderBy(x => x.Name).ToList();
            ViewBag.VacationType = _vacationDb.VacationTypes.OrderBy(x => x.VacationName).ToList();
            return View(model);
        }
        public IActionResult GetCountVacationEmployee(int?Id)
        {
            return Json(_vacationDb.VacationPlans.Where(x=>x.RequestVacationId==Id).Count());

        }
        public IActionResult GetVacationType()
        {
            return Json(_vacationDb.VacationTypes.OrderBy(x=>x.Id).ToList());

        }
        public IActionResult Delete(int ?id)
        {
            return View(_vacationDb.RequestVacations.
                Include(x => x.Employee).
                Include(x => x.vacationType).
               
                Include(x => x.vacationPlansList).
                FirstOrDefault(x => x.Id==id));
        }
        [HttpPost]
        public IActionResult Delete(RequestVacation model) {

            if (model!=null)
            {
                _vacationDb.RequestVacations.Remove(model);
                _vacationDb.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }
        //public IActionResult ViewReportVacationPlan()
        //{
        //    ViewBag.Employees = _vacationDb.Employees.ToList();

        //    return View();
        //}
        public IActionResult ViewReportVacationPlan2()
        {
            ViewBag.Employees = _vacationDb.Employees.ToList();

            return View();
        }
        public IActionResult GetReportVacationPlan
            (int EmployeeId, DateTime FromDate, DateTime ToDate)
        {
            string Id ="";
            if (EmployeeId !=0 && EmployeeId.ToString() !="")
            
                Id = $"and Employees.Id{EmployeeId}";
            //      var sqlQuery = _vacationDb.SqlDataTable($@"SELECT  distinct dbo.Employees.VacationBlance, dbo.Employees.Name, dbo.Employees.Id,
            //        sum(dbo.VacationTypes.NumberDays) as TtotalVacation,
            //dbo.Employees.VacationBlance-sum(dbo.VacationTypes.NumberDays) as Ttotal
            //                  FROM            dbo.Employees INNER JOIN
            //                   dbo.RequestVacations ON dbo.Employees.Id = dbo.RequestVacations.EmployeeId INNER JOIN
            //                   dbo.VacationPlans ON dbo.RequestVacations.Id = dbo.VacationPlans.RequestVacationId INNER JOIN
            //                   dbo.VacationTypes ON dbo.RequestVacations.VacationTypeId = dbo.VacationTypes.Id
            // where dbo.VacationPlans.VacationDate between
            //                     '" + FromDate.ToString("yyyy-MM-dd") + "' and '" + ToDate.ToString("yyyy-MM-dd") + "'"+ 

            //                   "RequestVacations.Approved='True' " +
            //                   $"{Id}group by dbo.Employees.VacationBlance, dbo.Employees.Name, dbo.Employees.Id");

            string sqlQuery =$@"SELECT  distinct dbo.Employees.VacationBlance, dbo.Employees.Name, dbo.Employees.Id,
            sum(dbo.VacationTypes.NumberDays) as TtotalVacation,
			   dbo.Employees.VacationBlance - sum(dbo.VacationTypes.NumberDays) as Ttotal
                        FROM dbo.Employees INNER JOIN
                         dbo.RequestVacations ON dbo.Employees.Id = dbo.RequestVacations.EmployeeId INNER JOIN
                         dbo.VacationPlans ON dbo.RequestVacations.Id = dbo.VacationPlans.RequestVacationId INNER JOIN
                         dbo.VacationTypes ON dbo.RequestVacations.VacationTypeId = dbo.VacationTypes.Id

                         where dbo.VacationPlans.VacationDate between
                           '" + FromDate.ToString("yyyy-MM-dd") + "' and '" + ToDate.ToString("yyyy-MM-dd") + "'"+ 

                         "RequestVacations.Approved='True' " +
                         $"{Id}group by dbo.Employees.VacationBlance, dbo.Employees.Name, dbo.Employees.Id";
                          var SpGetData = _vacationDb.SpGetReportVacationPlans.FromSqlRaw(sqlQuery).ToList();

            ViewBag.Employees = _vacationDb.Employees.ToList();
                return View("ViewReportVacationPlan", sqlQuery);
            
        
        }
    }
}
