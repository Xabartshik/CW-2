using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService.Domain
{
    public class Service
    {

        public int Id { get; set; }

        public string Name { get; set; }

        [Range(0, double.MaxValue)]
        public double Price { get; set; }

        public string? Description { get; set; }

    }
}
