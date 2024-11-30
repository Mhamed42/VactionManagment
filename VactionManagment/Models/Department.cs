using System.ComponentModel.DataAnnotations;

namespace VactionManagment.Models
{
    public class Department : EntiyBase
    {
        
        [Display(Name="Department Name")]
        [StringLength(150)]
        public string Name { get; set; }=string.Empty;
        public string? Description { get; set; }
    }
}
