using Employee.Services.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace Employee.ViewModel
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Display(Name = "Age")]
        public int? Age { get; set; }
        [Required]
        [Display(Name = "Birth Date")]
        public DateTime BirthDate { get; set; }
        [Display(Name = "Photo")]
        public string Photo { get; set; }
        [Required]
        [Display(Name = "Upload Photo")]
        [AllowFileSize(FileSize = 25 * 1024 * 1024, ErrorMessage = "Maximum allowed file size is 25 MB")]
        public HttpPostedFileBase FileAttach { get; set; }
        [Required(ErrorMessage = "Job Title is Required")]
        [Display(Name = "Job Title")]
        public string JobTitle { get; set; }
        public bool IsActive { get; set; } = true;
        public string FormattedBirthDate => BirthDate.ToString("dd/MM/yyyy");
    }
}