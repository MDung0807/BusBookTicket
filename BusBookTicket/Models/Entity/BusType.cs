using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusBookTicket.Models.Entity
{
    public class BusType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int busTypeID {  get; set; }
        [Required]
        public string? name { get; set; }
        public string? description { get; set; }
        [Required]
        public int totalSeats { get; set; }
    }
}
