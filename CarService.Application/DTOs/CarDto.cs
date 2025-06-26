using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService.Application.DTOs
{
    public record CarDto(int Id, string Brand, string Model, int Year, string? OwnerName);
}
