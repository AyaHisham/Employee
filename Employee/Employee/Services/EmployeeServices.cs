using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Employee.Models.EntityDataModel;
using Employee.ViewModel;

namespace Employee.Services
{
    public static class EmployeeServices
    {
        public static EmployeeViewModel GetEmployee(int id)
        {
            using (EmployeeEntities db = new EmployeeEntities())
            {
                EmployeeViewModel employee = db.Employees.Where(x => x.Id == id).Select(x => new EmployeeViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    BirthDate = x.BirthDate,
                    Age = DbFunctions.DiffYears(x.BirthDate, DateTime.Now),
                    Photo = x.Photo,
                    JobTitle = x.JobTitle,
                    IsActive = x.IsActive
                }).FirstOrDefault();
                return employee;
            }
        }

        public static List<EmployeeViewModel> GetAllActiveEmployees()
        {
            using (EmployeeEntities db = new EmployeeEntities())
            {
                List<EmployeeViewModel> employees = db.Employees.Where(x => x.IsActive == true).Select(x => new EmployeeViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    BirthDate = x.BirthDate,
                    Age = DbFunctions.DiffYears(x.BirthDate,DateTime.Now),
                    Photo = x.Photo,
                    JobTitle = x.JobTitle,
                    IsActive = x.IsActive
                }).ToList();
                return employees;
            }
        }

        public static void AddNewEmployee(EmployeeViewModel model)
        {
            using (EmployeeEntities db = new EmployeeEntities())
            {
                Models.EntityDataModel.Employee newEmployee = new Models.EntityDataModel.Employee();
                newEmployee.Name = model.Name;
                newEmployee.BirthDate = model.BirthDate;
                newEmployee.JobTitle = model.JobTitle;
                if (model.FileAttach != null)
                {
                    string nameForDataBase;
                    FileOperation.SaveFile(model.FileAttach, "~/UploadImage/", out nameForDataBase);
                    if (nameForDataBase != null)
                    {
                        newEmployee.Photo = nameForDataBase;
                    }
                }
                db.Employees.Add(newEmployee);
                db.SaveChanges();
            }
        }

        public static void EditEmployee(int id, EmployeeViewModel model)
        {
            using (EmployeeEntities db = new EmployeeEntities())
            {
                Models.EntityDataModel.Employee employee = db.Employees.Where(x=>x.Id == id).FirstOrDefault();
                db.Entry(employee).State = EntityState.Modified;
                employee.Name = model.Name;
                employee.BirthDate = model.BirthDate;
                employee.JobTitle = model.JobTitle;
                employee.IsActive = model.IsActive;
                if (model.FileAttach != null)
                {
                    string oldPhotoFullPath = (HttpContext.Current.Server.MapPath("~/UploadImage/" + model.Photo));
                    FileOperation.DeleteFile(oldPhotoFullPath);
                    string nameForDataBase;
                    FileOperation.SaveFile(model.FileAttach, "~/UploadImage/", out nameForDataBase);
                    if (nameForDataBase != null)
                    {
                        employee.Photo = nameForDataBase;
                    }
                }
             
                db.SaveChanges();
            }
        }

        public static void DeleteEmployee(int id)
        {
            using (EmployeeEntities db = new EmployeeEntities())
            {
                Models.EntityDataModel.Employee employee = db.Employees.Where(x => x.Id == id).FirstOrDefault();
                db.Entry(employee).State = EntityState.Modified;
                employee.IsActive = false;
                db.SaveChanges();
            }
        }
    }
}