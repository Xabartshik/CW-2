using CarService.DAL.Infrastructure;
using CarService.DAL.Interface;
using CarService.DAL.Mapper;
using CarService.DAL.Model;
using CarService.Domain;
using CarService.Domain.Interfaces;
using LinqToDB;
using Microsoft.Extensions.Logging;
using Npgsql;
using System.Data;

namespace CarService.DAL.Repositories
{
    public class CarRepository : ICarRepository
    {
        private readonly ICarDataConnection _db;
        private readonly ILogger<CarRepository> _logger;
        public CarRepository(ICarDataConnection db, ILogger<CarRepository> logger)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
            _logger = logger;
        }

        public async Task<Car?> GetByIdAsync(int id)
        {
            _logger.LogInformation("Поиск машины в БД по ID: {id}", id);
            try
            {
                var car = await _db.Cars.FirstOrDefaultAsync(p => p.Id == id);
                return car?.ToDomain();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при получении машины по ID");
                throw;
            }

        }

        public async Task<IEnumerable<Car>> GetAllAsync()
        {
            _logger.LogInformation("Поиск всех машин в БД");
            try
            {
                var carModel = await _db.Cars.ToListAsync();
                return carModel.Select(c => c.ToDomain());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при получении всех машин");
                throw;
            }
        }

        public async Task AddAsync(Car car)
        {
            _logger.LogInformation("Добавление машины в БД");
            try
            {
                if (car is null)
                {
                    throw new ArgumentNullException(nameof(car));
                }
                var model = car.ToModel();
                await _db.InsertAsync(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при добавлении машины в БД");
                throw;
            }

        }

        public async Task<bool> RemoveAsync(int id)
        {
            _logger.LogInformation("Удаление машины из БД");
            try
            {
                var car = await _db.Cars.FirstOrDefaultAsync(c => c.Id == id);
                if (car is null)
                    return false;
                return await _db.DeleteAsync(car) > 0;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при удалении машины из БД");
                throw;
            }

        }

        public async Task<bool> UpdateAsync(Car car)
        {
            _logger.LogInformation("Обновление машины в БД");
            try
            {
                if (car is null)
                    return false;
                return await _db.UpdateAsync(car.ToModel()) > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при обновлении машины в БД");
                throw;
            }

        }
        public async Task<IEnumerable<CarServiceHistory>> GetCarServiceHistoryAsync()
        {
            var query = from sr in _db.ServiceRecords
                        join c in _db.Cars on sr.CarId equals c.Id
                        join s in _db.Services on sr.ServiceId equals s.Id orderby c.Id
                        select new CarServiceHistory
                        {
                            CarId = c.Id, 
                            ServiceId = s.Id,
                            Brand = c.Brand,
                            Model = c.Model,
                            ServiceName = s.Name,
                            ServicePrice = s.Price,
                            ServiceStatus = sr.Status,
                            ServiceDate = sr.Date
                        };
            var results = await query.ToListAsync();

            return results;

            //    public record CarServiceHistoryDto(int CarId, int ServiceId, string Brand, string Model, string ServiceName,
            //double ServicePrice, string ServiceStatus, DateTime ServiceDate);
        }
    }
}