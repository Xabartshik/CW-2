using CarService.Application.DTOs;
using CarService.Domain;
using CarService.Domain.Interfaces;

namespace CarService.Application.Services
{
    public class CarService
    {
        private readonly ICarRepository _repository;

        public CarService(ICarRepository repository)
        {
            _repository = repository;
        }
        private CarDto ToDto(Car car)
            => new CarDto(car.Id, car.Brand, car.Model, car.Year, car.OwnerName);

        public async Task<IEnumerable<CarDto>> GetAll()
        {
            var result = await _repository.GetAllAsync();
            return result.Select(ToDto);
        }

        public async Task<CarDto?> GetById(int id)
        {
            var car = await _repository.GetByIdAsync(id);
            return car == null ? null : ToDto(car);
        }

        public async Task Add(CarDto dto)
        {
            var car = new Car
            {
                Id = dto.Id,
                Brand = dto.Brand,
                Model = dto.Model,
                Year = dto.Year,
                OwnerName = dto.OwnerName
            };
            await _repository.AddAsync(car);
        }

        public async Task<bool> Remove(int id) => await _repository.RemoveAsync(id);

        public async Task<bool> Update(int id, CarDto dto)
        {
            var car = new Car
            {
                Id = id,
                Brand = dto.Brand,
                Model = dto.Model,
                Year = dto.Year,
                OwnerName = dto.OwnerName
            };
            return await _repository.UpdateAsync(car);
        }
    }
}