using ServiceService.Domain;
using ServiceService.Domain.Interfaces;
using LinqToDB;
using Microsoft.Extensions.Logging;
using Npgsql;
using System.Data;
using CarService.Domain;
using CarService.DAL.Interface;
using CarService.DAL.Mapper;

namespace ServiceService.DAL.Repositories
{
    public class ServiceRepository : IServiceRepository
    {
        private readonly ICarDataConnection _db;
        private readonly ILogger<ServiceRepository> _logger;
        public ServiceRepository(ICarDataConnection db, ILogger<ServiceRepository> logger)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
            _logger = logger;
        }

        public async Task<Service?> GetByIdAsync(int id)
        {
            _logger.LogInformation("Поиск сервиса в БД по ID: {id}", id);
            try
            {
                var service = await _db.Services.FirstOrDefaultAsync(p => p.Id == id);
                return service?.ToDomain();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при получении сервиса по ID");
                throw;
            }

        }

        public async Task<IEnumerable<Service>> GetAllAsync()
        {
            _logger.LogInformation("Поиск всех сервисов в БД");
            try
            {
                var serviceModel = await _db.Services.ToListAsync();
                return serviceModel.Select(c => c.ToDomain());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при получении всех сервисов");
                throw;
            }
        }

        public async Task AddAsync(Service service)
        {
            _logger.LogInformation("Добавление сервиса в БД");
            try
            {
                if (service is null)
                {
                    throw new ArgumentNullException(nameof(service));
                }
                await _db.InsertAsync(service.ToModel());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при добавлении сервиса в БД");
                throw;
            }

        }

        public async Task<bool> RemoveAsync(int id)
        {
            _logger.LogInformation("Удаление сервиса из БД");
            try
            {
                var service = await _db.Services.FirstOrDefaultAsync(c => c.Id == id);
                if (service is null)
                    return false;
                return await _db.DeleteAsync(service) > 0;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при удалении сервиса из БД");
                throw;
            }

        }

        public async Task<bool> UpdateAsync(Service service)
        {
            _logger.LogInformation("Обновление сервиса в БД");
            try
            {
                if (service is null)
                    return false;
                return await _db.UpdateAsync(service.ToModel()) > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при обновлении сервиса в БД");
                throw;
            }

        }
    }
}