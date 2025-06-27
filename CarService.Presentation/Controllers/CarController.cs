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
        public async Task<IEnumerable<CarDto>> GetAll()
        {
            return await _service.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CarDto?>> Get(int id)
        {
            var result = await _service.GetById(id);
            return result is null ? NotFound() : result;
        }

        [HttpPost]
        public async Task<ActionResult<CarDto>> Add([FromBody] CarDto carDto)
        {
            await _service.Add(carDto);
            return CreatedAtAction(nameof(Get), new { id = carDto.Id }, carDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.Remove(id);
            if (result == false)
                return NotFound();

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CarDto updatedCarDto)
        {
            // Валидация входных данных
            if (string.IsNullOrWhiteSpace(updatedCarDto.Brand) ||
                string.IsNullOrWhiteSpace(updatedCarDto.Model) ||
                updatedCarDto.Year < 1980)
                return BadRequest("Некорректные данные");

            var updated = await _service.Update(id, updatedCarDto);
            if (updated == false)
                return NotFound();
            return NoContent();
        }
    }
}