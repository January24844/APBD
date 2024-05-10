using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestApiDocker.Models;
using RestApiDocker.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestApiDocker.Controllers
{
    [ApiController]
    public class AnimalsController : ControllerBase
    {
        private readonly IDBService _dBService;

        public AnimalsController(IDBService dBService)
        {
            _dBService = dBService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAnimals([FromQuery] string orderBy)
        {
            try
            {
                List<Animal> animals = _dBService.GetAnimals(orderBy);
                return Ok(animals);
            }
            catch (Exception)
            {
                return BadRequest("ait");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateAnimal([FromBody] Animal animal)
        {
            try
            {
                _dBService.CreateAnimal(animal);
                return Ok("created");
            }
            catch (Exception)
            {
                return BadRequest("pluh");
            }
        }

        [HttpPut("{idAnimal}")]
        public async Task<IActionResult> ChangeAnimal([FromRoute] int idAnimal, [FromBody] Animal animal)
        {
            try
            {
                _dBService.ChangeAnimal(idAnimal, animal);
                return Ok("changed");
            }
            catch (Exception)
            {
                return BadRequest("bad");
            }
        }

        [HttpDelete("{idAnimal}")]
        public async Task<IActionResult> DeleteAnimal([FromRoute] int idAnimal)
        {
            try
            {
                _dBService.DeleteAnimal(idAnimal);
                return Ok("deleted");
            }
            catch (Exception)
            {
                return BadRequest("bad");
            }
        }
    }
}
