using CarService.DAL.Model;
using CarService.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CarService.DAL.Mapper
{
    public static class ServiceMapper
    {
        public static Service ToDomain(this ServiceModel model)
        {
            return new Service
            {
                Id = model.Id,
                Name = model.Name,
                Price = model.Price,
                Description = model.Description

            };
        }
        public static ServiceModel ToModel(this Service domain)
        {
            return new ServiceModel
            {
                Id = domain.Id,
                Name = domain.Name,
                Price = domain.Price,
                Description = domain.Description

            };
        }
    }
}
