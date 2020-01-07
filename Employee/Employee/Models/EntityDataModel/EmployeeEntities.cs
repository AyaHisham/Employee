using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Employee.Models.EntityDataModel
{
    public class EmployeeEntities: DbContext
    {
        public virtual DbSet<Employee> Employees { get; set; }  
    }
}