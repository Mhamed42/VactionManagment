using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VactionManagment.Models
{
    public class VacationPlan: EntiyBase
    {
        [Display(Name ="Vacation Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString ="{0:dd-MM-yyyy}")]
        public DateTime? VacationDate { get; set; }

        public int RequestVacationId { get; set; }
        [ForeignKey("RequestVacationId")]
        public RequestVacation? requestVacation { get; set; }
    }
}
