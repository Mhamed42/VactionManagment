using System.ComponentModel.DataAnnotations;

namespace VactionManagment.Models
{
    public class VacationType : EntiyBase
    {
        [StringLength(200)]
        [Display(Name = "Vacation Name")]
        public string VacationName { get; set; }
        [Display(Name = "Vacation Color")]
        [StringLength(7)]
        public string BackgroundColor { get; set; }=string.Empty;
        [Display(Name = "NumberDays")]
        public int NumberDays { get; set; }

    }
}
