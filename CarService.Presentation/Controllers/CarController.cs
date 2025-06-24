using Microsoft.AspNetCore.Mvc;

namespace CarService.Presentation.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class CarController : ControllerBase
    {
        private static readonly List<Car> Cars = new List<Car>()
        {
            new Car(){ Model = "UAZ", Brand="469", Id = 1, Year = 1988 },
            new Car(){ Model = "VAZ", Brand="Москвич", Id = 2, Year = 2012 },
            new Car(){ Model = "MAZ", Brand="Жигуль", Id = 3, Year = 2023 },
            new Car(){ Model = "GAZ", Brand="УЗ-68900", Id = 4, Year = 1996 },
            new Car(){ Model = "TAZ", Brand="ЕН-98-У", Id = 5, Year = 2015 },
        };

        [HttpGet]
        public IEnumerable<Car> GetAll()
        {
            return Cars;
        }


        [HttpGet("{id}")]
        public ActionResult<Car> Get(int id)
        {
            var car = Cars.FirstOrDefault(p => p.Id == id);
            if (car == null)
                return NotFound();
            return car;
        }


        [HttpPost]
        public ActionResult<Car> Add([FromBody] Car car)
        {
            if (!Car.Validate(car))
                return BadRequest("Некорректные данные");
            Cars.Add(car);
            return CreatedAtAction(nameof(Get), new { Id = car.Id }, car);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var car = Cars.FirstOrDefault(p => p.Id == id);
            if (car == null)
                return NotFound();
            Cars.Remove(car);
            return NoContent();
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Car updatedCar)
        {
            var car = Cars.FirstOrDefault(p => p.Id == id);
            if (car == null)
                return NotFound();
            if (!Car.Validate(car))
                return BadRequest("Некорректные данные");
            car.Model = updatedCar.Model;
            car.Brand = updatedCar.Brand;
            car.Year = updatedCar.Year;
            car.OwnerName = updatedCar.OwnerName;
            return NoContent();
        }

    }
}
