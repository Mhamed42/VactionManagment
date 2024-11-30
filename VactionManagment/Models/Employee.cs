using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.PortableExecutable;

namespace VactionManagment.Models
{
    public class Employee: EntiyBase
    {
        [Display(Name= "Employee Name")]
        [StringLength(200)]
        public string Name { get; set; } = string.Empty;
        [Display(Name= "Vacation Blance")]
        [Range(1,31)]
        public int VacationBlance { get; set; }
        [Display(Name= "Department")]
        public int DepartmentId { get; set; }
        [ForeignKey("DepartmentId")]
        public Department? Department { get; set; }
    }
}
