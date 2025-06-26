using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CarService.Domain;

namespace CarService.Domain.Interfaces
{
    public interface ICarRepository
    {
        Car? GetById(int id);
        IEnumerable<Car> GetAll();
        void Add(Car car);
        bool Remove(int id);
        bool Update(Car car);
    }
}