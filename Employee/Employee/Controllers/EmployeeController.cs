using System.Collections.Generic;
using System.Web.Mvc;
using Employee.ViewModel;
using Employee.Services;
using System.Net;

namespace Employee.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult Index()
        {
            List<EmployeeViewModel> employees = EmployeeServices.GetAllActiveEmployees();
            return View(employees);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeViewModel employee = EmployeeServices.GetEmployee(id.Value);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        public ActionResult Create()
        {
            return View(new EmployeeViewModel());
        }

        [HttpPost]
        public ActionResult Create(EmployeeViewModel model)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    EmployeeServices.AddNewEmployee(model);
                    return RedirectToAction("Index");
                }
                else
                {
                    return this.View(model);
                }
            }
            catch
            {
                return View("Error");
            }
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeViewModel employee = EmployeeServices.GetEmployee(id.Value);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        [HttpPost]
        public ActionResult Edit(int id, EmployeeViewModel model)
        {
            try
            {     
                    EmployeeServices.EditEmployee(id, model);
                    return RedirectToAction("Index");
            }
            catch
            {
                return View("Error");
            }
        }

        [HttpPost]
        public JsonResult Delete(int? id)
        {
            if (id != null)
            {
                EmployeeServices.DeleteEmployee(id.Value);
            }     
            return Json("",JsonRequestBehavior.AllowGet);  
        }
    }
}