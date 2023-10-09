using Carvromvroom;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly VROOOMRepo carRepository;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<VROOOMRepo>();
            services.AddControllers();
        }

        public ValuesController(VROOOMRepo carRepository)
        {
            this.carRepository = carRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var cars = carRepository.GetAll();
            return Ok(cars);
        }

        [HttpGet("{id}")]
        public IActionResult GetByID(int id)
        {
            var car = carRepository.GetBookByID(id);
            if (car == null)
                return NotFound();

            return Ok(car);
        }

        [HttpPost]
        public IActionResult Add([FromBody] Car car)
        {
            carRepository.Add(car);
            return CreatedAtAction(nameof(GetByID), new { id = car.Id }, car);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var deleted = carRepository.Delete(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }


        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Car updatedCar)
        {
            var updated = carRepository.Update(id, updatedCar);
            if (!updated)
                return NotFound();

            return Ok(updatedCar);
        }


    }
}
