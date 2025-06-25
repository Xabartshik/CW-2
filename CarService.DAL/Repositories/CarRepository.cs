using CarService.Domain;
using System.Xml.Linq;

namespace CarService.DAL.Repositories
{
    public class CarRepository
    {
        private static readonly List<Car> _cars = new List<Car>
        {
            new Car { Id = 1, Brand = "Toyota", Model = "Camry", Year = 2020, OwnerName = "Иван Иванов" },
            new Car { Id = 2, Brand = "BMW", Model = "X5", Year = 2019, OwnerName = "Петр Петров" },
            new Car { Id = 3, Brand = "Mercedes", Model = "C-Class", Year = 2021, OwnerName = null }
        };

        public Car? GetById(int id)
            => _cars.FirstOrDefault(c => c.Id == id);

        public IEnumerable<Car> GetAll()
            => _cars;

        public void Add(Car car)
            => _cars.Add(car);

        public bool Remove(int id)
        {
            var car = GetById(id);
            if (car != null)
                return _cars.Remove(car);
            return false;
        }

        public bool Update(Car car)
        {
            var existing = GetById(car.Id);
            if (existing == null)
                return false;

            existing.Brand = car.Brand;
            existing.Model = car.Model;
            existing.Year = car.Year;
            existing.OwnerName = car.OwnerName;
            return true;
        }
    }
}