using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService.Domain
{
    public class CarServiceHistory
    {
        public int CarId { get; set; }
        [Required]
        public int ServiceId { get; set; }
        [Required]
        public string Brand { get; set; }
        [Required]
        public string Model { get; set; }
        public string ServiceName { get; set; }

        [Range(0, double.MaxValue)]
        public double ServicePrice { get; set; }
        public string ServiceStatus { get; set; }
        public DateTime ServiceDate { get; set; }


    }
}
