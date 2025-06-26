using CarService.Application.DTOs;
using CarService.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace CarService.Presentation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CarController : ControllerBase
    {
        private readonly CarService.Application.Services.CarService _service;

        public CarController(CarService.Application.Services.CarService service)
        {
            _service = service;
        }
        [HttpGet]
        public IEnumerable<CarDto> GetAll()
        {
            return _service.GetAll();
        }

        [HttpGet("{id}")]
        public ActionResult<CarDto?> Get(int id)
        {
            return _service.GetById(id);
        }

        [HttpPost]
        public ActionResult<CarDto> Add([FromBody] CarDto carDto)
        {
            _service.Add(carDto);
            return CreatedAtAction(nameof(Get), new { id = carDto.Id }, carDto);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _service.Remove(id);
            if (result == false)
                return NotFound();

            return NoContent();
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] CarDto updatedCarDto)
        {
            // Валидация входных данных
            if (string.IsNullOrWhiteSpace(updatedCarDto.Brand) ||
                string.IsNullOrWhiteSpace(updatedCarDto.Model) ||
                updatedCarDto.Year < 1980)
                return BadRequest("Некорректные данные");

            var updated = _service.Update(id, updatedCarDto);
            if (updated == false)
                return NotFound();
            return NoContent();
        }
    }
}