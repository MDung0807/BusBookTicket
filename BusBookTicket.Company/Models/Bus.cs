
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusBookTicket.CompanyManage.Models
{
    [Table("Buses")]
    public class Bus
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int busID { get; set; }
        [Column(name:"busNumber")]
        [Required]
        public string? busNumber { get; set; }
        public Company? company { get; set; }
        public BusType? busType { get; set; }
    }
}
