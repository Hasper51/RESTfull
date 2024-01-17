using Microsoft.AspNetCore.Mvc;
using RESTfull.Domain;
using RESTfull.Infrastructure;

namespace RESTfull.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegistryController : ControllerBase
    {
        private readonly Context _context;
        private readonly RegistryRepository _registryRepository;
        public RegistryController(Context context)
        {
            _context = context;
           
            _registryRepository = new RegistryRepository(_context);
        }

        // GET: api/<ValuesController>
        [HttpGet]
        public async Task<IEnumerable<Registry>> Get()
        {
            return await _registryRepository.GetAllAsync();
        }

        // GET api/<ValuesController>/5
        [HttpGet("{Title}")]
        public async Task<Registry> Get(string Title)
        {
            return await _registryRepository.GetByNameAsync(Title);
        }
       
        // POST api/<ValuesController>
        [HttpPost]
        public async Task PostRegistry([FromBody] Registry value)
        {
            await _registryRepository.AddAsync(value);
            _registryRepository.ChangeTrackerClear();
        }

        // PUT api/<ValuesController>/5
        [HttpPut]
        public async Task Put([FromBody] Registry value)
        {
            await _registryRepository.UpdateAsync(value);
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public async Task Delete(Guid id)
        {
            await _registryRepository.DeleteAsync(id);
        }
    }
}