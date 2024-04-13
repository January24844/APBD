using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class AnimalsController : ControllerBase
{
    private readonly List<Animal> _animals;
    private int _nextAnimalId = 1;

    public AnimalsController(List<Animal> animals)
    {
        _animals = animals;
    }

    [HttpGet]
    public IActionResult GetAllAnimals()
    {
        return Ok(_animals);
    }

    [HttpGet("{id}")]
    public IActionResult GetAnimalById(int id)
    {
        var animal = _animals.Find(a => a.Id == id);
        if (animal != null)
        {
            return Ok(animal);
        }
        else
        {
            return NotFound();
        }
    }

    [HttpPost]
    public IActionResult AddAnimal([FromBody] Animal animal)
    {
        animal.Id = _nextAnimalId++;
        _animals.Add(animal);
        return Created($"/animals/{animal.Id}", animal);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateAnimal(int id, [FromBody] Animal updatedAnimal)
    {
        var animalIndex = _animals.FindIndex(a => a.Id == id);
        if (animalIndex != -1)
        {
            updatedAnimal.Id = id;
            _animals[animalIndex] = updatedAnimal;
            return Ok();
        }
        else
        {
            return NotFound();
        }
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteAnimal(int id)
    {
        var animalIndex = _animals.FindIndex(a => a.Id == id);
        if (animalIndex != -1)
        {
            _animals.RemoveAt(animalIndex);
            return Ok();
        }
        else
        {
            return NotFound();
        }
    }
}

[ApiController]
[Route("[controller]")]
public class VisitsController : ControllerBase
{
    private readonly List<Visit> _visits;
    private int _nextVisitId = 1;

    public VisitsController(List<Visit> visits)
    {
        _visits = visits;
    }

    [HttpPost]
    public IActionResult AddVisit([FromBody] Visit visit)
    {
        visit.Id = _nextVisitId++;
        _visits.Add(visit);
        return Created($"/visits/{visit.Id}", visit);
    }
}
