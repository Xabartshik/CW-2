using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService.Application.DTOs
{
    public record CarServiceHistoryDto(int CarId, int ServiceId, string Brand, string Model, string ServiceName,
        double ServicePrice, string ServiceStatus, DateTime ServiceDate);
}
