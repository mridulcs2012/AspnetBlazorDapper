using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Models
{
    public class ShareTo
    {
        public int Id { get; set; }

        [Required]
        public int AppntId { get; set; }

        [Required]
        public int EmpId { get; set; }

        [Required]
        public string? Insrtuctions { get; set; }

        [Required]
        public string? Remarks { get; set; }
        public string? Name { get; set; }
        public bool IsUpdate { get; set; } = false;
    }
}
