using System.ComponentModel.DataAnnotations;

namespace DataAccessLibrary.Models
{
    public class Appointment
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "The Job Start Date field is Required")]
        [DisplayFormat(DataFormatString = "{dd-MM-yyyy}", ApplyFormatInEditMode = true)]

        public DateTime wheredate { get; set; } = DateTime.Now;

        public int pslct { get; set; }

        [Required(ErrorMessage = "Please SL No here")]
        public int slno { get; set; }

        [Required(ErrorMessage = "Please Enter Time"), MaxLength(10)]
        public string fromtime { get; set; }

        [Required(ErrorMessage = "Please Appointment here"), MaxLength(500)]
        public string appointwith { get; set; }

        [Required(ErrorMessage = "Please Remarks here"), MaxLength(50)]
        public string remarks { get; set; }
		public string Name { get; set; }
		public bool IsUpdate { get; set; } = false;
    }
}
