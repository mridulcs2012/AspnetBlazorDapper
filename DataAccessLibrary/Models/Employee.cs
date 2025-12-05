using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLibrary.Models
{
    //using System.ComponentModel.DataAnnotations;
    public class Employee
    {
        public int Id { get; set; }

        [Required]
        [DisplayName]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; }

        [Required]
        [Phone]
        [StringLength(20)]
        public string PhoneNumer { get; set; }

        [Required]
        [StringLength(30)]
        public string CreditCardNumer { get; set; }

        public bool IsUpdate { get; set; } = false;
    }
}
