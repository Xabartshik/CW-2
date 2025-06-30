using CarService.Application.DTOs;
using CarService.Application.Settings;
using CarService.Domain;
using CarService.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.VisualBasic;

namespace CarService.Application.Services
{
    public class CarService
    {
        private readonly ICarRepository _repository;
        private readonly ILogger<CarService> _logger;
        private readonly AppSettings _appSettings;

        public CarService(ICarRepository repository, ILogger<CarService> logger, IOptions<AppSettings> options)
        {
            _repository = repository;
            _logger = logger;
            _appSettings = options.Value;
        }
        private CarDto ToDto(Car car)
            => new CarDto(car.Id, car.Brand, car.Model, car.Year, car.OwnerName);

        public async Task<IEnumerable<CarDto>> GetAll()
        {
            if (_appSettings.EnableDetailedLogging)
            {
            _logger.LogTrace("Вызов процедуры GetAll");
            _logger.LogDebug("Процедура для получения всех машин в репозитории");
            }
            _logger.LogInformation("Получение всех машин");
            try
            {
                var result = await _repository.GetAllAsync();
                _logger.LogInformation("Все машины получены");
                return result.Select(ToDto);
            }
            catch (Exception ex) {
                _logger.LogError(ex, "Ошибка в GetAll при получении всех машин");
                throw;
            }

        }

        public async Task<CarDto?> GetById(int id)
        {
            if (_appSettings.EnableDetailedLogging)
            {
                _logger.LogTrace("Вызов процедуры GetById");
                _logger.LogDebug("Процедура для получения машины по ID:{ShopID} из репозитория", id);
            }
            _logger.LogInformation("Получение машины по {shopId}", id);
            try
            {
                var car = await _repository.GetByIdAsync(id);
                if (car is not null)
                {
                    _logger.LogInformation("Машина найдена");
                    return ToDto(car);
                }
                _logger.LogWarning("Машина не найдена");
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка в GetById при получении машины по {ShopID}", id);
                throw;
            }
        }

        public async Task Add(CarDto dto)
        {
            if (_appSettings.EnableDetailedLogging)
            {
                _logger.LogTrace("Вызов процедуры Add");
                _logger.LogDebug("Процедура для добавления машины в репозиторий");
            }
            _logger.LogInformation("Добавление машины");
            try
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
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка в Add при добавлении машины");
                throw;
            }

        }

        public async Task<bool> Remove(int id)
        {
            if (_appSettings.EnableDetailedLogging)
            {
                _logger.LogTrace("Вызов процедуры Remove");
                _logger.LogDebug("Процедура для удаления машины по ID:{ShopID} из репозитория", id);
            }
            _logger.LogInformation("Удаление машины по {shopId}", id);
            try
            {
                if (await _repository.RemoveAsync(id))
                {
                    _logger.LogInformation("Машина удалена");
                    return true;
                }
                _logger.LogWarning("Машина не найдена");
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка в Remove при удалении машины по {ShopID}", id);
                throw;
            }
        }


        public async Task<bool> Update(int id, CarDto dto)
        {
            if (_appSettings.EnableDetailedLogging)
            {
                _logger.LogTrace("Вызов процедуры Update");
                _logger.LogDebug("Процедура для обновления машины по ID:{ShopID} в репозитории", id);
            }
            _logger.LogInformation("Обновление данных машины");
            try
            {
                var car = new Car
                {
                    Id = id,
                    Brand = dto.Brand,
                    Model = dto.Model,
                    Year = dto.Year,
                    OwnerName = dto.OwnerName
                };
                if (await _repository.UpdateAsync(car))
                {
                    _logger.LogInformation("Машина обновлена по ID: {ShopID}", id);
                    return true;
                }
                _logger.LogWarning("Машина не найдена");
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка в Update при обновлении машины по {ShopID}", id);
                throw;
            }
        }

        public async Task<IEnumerable<CarDto>> GetProductsPageAsync(int page)
        {
            if (_appSettings.EnableDetailedLogging)
            {
                _logger.LogTrace("Вызов процедуры GetProductsPageAsync");
                _logger.LogDebug("Процедура для получения страницы {pageNumber} машин из репозитория", page);
            }
            var pageLength = _appSettings.MaxItemsPerPage;
            _logger.LogInformation("Запрос страницы {Page} машин (размер страницы: {pageLength})", page, pageLength);

            try
            {
                var cars = await _repository.GetAllAsync();
                _logger.LogInformation("Получено {Count} машин", cars.ToList().Count);

                var pagedCars = cars
                .Skip((page - 1) * pageLength)
                .Take(pageLength)
                .ToList();

                return pagedCars.Select(ToDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при получении постраничного списка продуктов");
                throw;
            }
        }
    }
}