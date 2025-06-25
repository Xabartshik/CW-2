using CarService.Application.DTOs;
using CarService.DAL.Repositories;
using CarService.Domain;

namespace CarService.Application.Services
{
    public class CarService
    {
        private readonly CarRepository _repository = new CarRepository();

        private CarDto ToDto(Car car)
            => new CarDto(car.Id, car.Brand, car.Model, car.Year, car.OwnerName);

        public IEnumerable<CarDto> GetAll()
            => _repository.GetAll().Select(ToDto);

        public CarDto? GetById(int id)
        {
            var car = _repository.GetById(id);
            return car == null ? null : ToDto(car);
        }

        public void Add(CarDto dto)
        {
            var car = new Car
            {
                Id = dto.Id,
                Brand = dto.Brand,
                Model = dto.Model,
                Year = dto.Year,
                OwnerName = dto.OwnerName
            };
            _repository.Add(car);
        }

        public bool Remove(int id) => _repository.Remove(id);

        public bool Update(int id, CarDto dto)
        {
            var car = new Car
            {
                Id = id,
                Brand = dto.Brand,
                Model = dto.Model,
                Year = dto.Year,
                OwnerName = dto.OwnerName
            };
            return _repository.Update(car);
        }
    }
}