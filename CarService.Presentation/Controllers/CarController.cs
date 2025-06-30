using CarService.Application.DTOs;
using CarService.Application.Services;
using CarService.Application.Settings;
using CarService.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace CarService.Presentation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CarController : ControllerBase
    {
        private readonly CarService.Application.Services.CarService _service;
        private readonly ILogger<CarController> _logger;
        private readonly AppSettings _appSettings;
        public CarController(CarService.Application.Services.CarService service, ILogger<CarController> logger, IOptions<AppSettings> options)
        {
            _service = service;
            _logger = logger;
            _appSettings = options.Value;
        }



        [HttpGet]
        public async Task<IEnumerable<CarDto>> GetAll()
        {
            _logger.LogInformation("Получен HTTP запрос на получение всех машин");
            var result = await _service.GetAll();
            _logger.LogInformation("Возвращено {count} машин", result.Count());
            return result;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CarDto?>> Get(int id)
        {
            _logger.LogInformation("Получен HTTP запрос на получение машины по ID");
            var result = await _service.GetById(id);
            if (result is not null)
            {

                _logger.LogInformation("Машина с ID: {id} найдена", id);
                return result;
            }
            _logger.LogWarning("Машина с ID: {id} не найдена", id);
            return NotFound();


        }

        [HttpPost]
        public async Task<ActionResult<CarDto>> Add([FromBody] CarDto carDto)
        {
            _logger.LogInformation("Получен HTTP запрос на добавление машины");
            if (string.IsNullOrWhiteSpace(carDto.Brand) ||
    string.IsNullOrWhiteSpace(carDto.Model) ||
    carDto.Year < 1980 || carDto.Year > _appSettings.MaxYear)
            {
                _logger.LogWarning("Некорректные данные о машине не позволяют добавить данные в репозитории");
                return BadRequest("Некорректные данные");
            }
            await _service.Add(carDto);
            _logger.LogInformation("Машина добавлена в репозиторий");
            return CreatedAtAction(nameof(Get), new { id = carDto.Id }, carDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            _logger.LogInformation("Получен HTTP запрос на удаление машины");
            var result = await _service.Remove(id);
            if (result == false)
            {
                _logger.LogWarning("Машина не найдена в репозитории");
                return NotFound();
            }
            _logger.LogInformation("Машина удалена из репозитория");
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CarDto updatedCarDto)
        {
            _logger.LogInformation("Получен HTTP запрос на обновление данных о машине");
            // Валидация входных данных
            if (string.IsNullOrWhiteSpace(updatedCarDto.Brand) ||
                string.IsNullOrWhiteSpace(updatedCarDto.Model) ||
                updatedCarDto.Year < 1980 || updatedCarDto.Year > _appSettings.MaxYear)
            {
                _logger.LogWarning("Некорректные данные о машине не позволяют обновить данные в репозитории");
                return BadRequest("Некорректные данные");
            }

            var updated = await _service.Update(id, updatedCarDto);
            if (updated == false)
            {
                _logger.LogWarning("Машина не найдена в репозитории");
                return NotFound();
            }
            _logger.LogInformation("Машина была обновлена, новые значения {carBrand}, {carModel}", updatedCarDto.Brand, updatedCarDto.Model);
            return NoContent();
        }

        [HttpGet("page")]
        public async Task<ActionResult<List<CarDto>>> GetProductsPaged(int page = 1)
        {
            _logger.LogInformation("Запрос страницы {Page} машин", page);

            try
            {
                var cars = await _service.GetProductsPageAsync(page);
                return Ok(cars);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при получении страницы машин");

                return StatusCode(500, "Внутренняя ошибка сервера");
            }
        }
        [HttpGet("info")]
        public ActionResult<object> GetApplicationInfo()
        {
            var appName = _appSettings.AppName;
            var appVersion = _appSettings.AppVersion;
            var maxItems = _appSettings.MaxItemsPerPage;
            var enableDetailedLogging = _appSettings.EnableDetailedLogging;
            var maxYear = _appSettings.MaxYear;
            _logger.LogInformation("Запрос информации о приложении");

            return Ok(new
            {
                ApplicationName = appName,
                AppVersion = appVersion,
                MaxItemsPerPage = maxItems,
                EnableDetailedLogging = enableDetailedLogging,
                MaxYear = maxYear,
                Environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"
            });
        }
    }
}