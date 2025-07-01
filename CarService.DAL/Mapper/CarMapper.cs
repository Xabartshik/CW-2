using CarService.DAL.Model;
using CarService.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService.DAL.Mapper
{
    public static class CarMapper
    {
        public static Car ToDomain(this CarModel carModel)
        {
            return new Car
            {
                Id = carModel.Id,
                Brand = carModel.Brand,
                Model = carModel.Model,
                Year = carModel.Year,
                OwnerName = carModel.OwnerName

            };
        }
        public static CarModel ToModel(this Car carDomain)
        {
            return new CarModel
            {
                Id = carDomain.Id,
                Brand = carDomain.Brand,
                Model = carDomain.Model,
                Year = carDomain.Year,
                OwnerName = carDomain.OwnerName,
                CreatedAt = carDomain.CreatedAt

            };
        }

    }
}
