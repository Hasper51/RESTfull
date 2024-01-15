using Microsoft.AspNetCore.Mvc;
using RESTfull.Domain;
using RESTfull.Infrastructure;

namespace RESTfull.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DisciplineController : ControllerBase
    {
        private readonly Context _context;
        private readonly DisciplineRepository _disciplineRepository;
        public DisciplineController(Context context)
        {
            _context = context;
            _disciplineRepository = new DisciplineRepository(_context);
        }

        // GET: api/<ValuesController>
        [HttpGet]
        public async Task<IEnumerable<Discipline>> Get()
        {
            return await _disciplineRepository.GetAllAsync();
        }

        // GET api/<ValuesController>/5
        [HttpGet("{Title}")]
        public async Task<Discipline> Get(string Title)
        {
            return await _disciplineRepository.GetByTitleAsync(Title);
        }

        // POST api/<ValuesController>
        [HttpPost]
        public async Task Post([FromBody] Discipline value)
        {
            await _disciplineRepository.AddAsync(value);
        }
        // PUT api/<ValuesController>/5
        [HttpPut]
        public async Task Put([FromBody] Discipline value)
        {
            await _disciplineRepository.UpdateAsync(value);
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public async Task Delete(Guid id)
        {
            await _disciplineRepository.DeleteAsync(id);
        }
    }
}