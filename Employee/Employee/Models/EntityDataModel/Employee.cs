using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Employee.Models.EntityDataModel
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }
        [Required]
        public string Photo { get; set; }
        [Required]
        public string JobTitle { get; set; }
        [Required]
        public bool IsActive { get; set; } = true;
    }
}