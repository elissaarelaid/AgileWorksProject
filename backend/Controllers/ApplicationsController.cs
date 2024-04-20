using backend.Model;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApplicationsController : ControllerBase
    {
        private readonly DataContext _context;
        public ApplicationsController(DataContext context) { _context = context; }
        
        [HttpGet]
        public IActionResult GetApplications() {
            return Ok(_context.Applications?
                    .Where(a => a.IsSolved == false)
                    .OrderByDescending(a => a.ResolutionDate).ToList());
        }

        [HttpPut("{id}")] //changes status to solved (true)
        public IActionResult SolveApplication(int id) {
            var applicationToChange = _context.Applications?.FirstOrDefault(a => a.Id == id);
            if (applicationToChange == null) {
                return NotFound("Application not found");
            }
            applicationToChange.IsSolved = true;
            _context.SaveChanges();
            return Ok(applicationToChange);
        }

        [HttpPost]
        public IActionResult AddApplication([FromBody] Application application) {
            if (_context.Applications!.Find(application.Id) != null) {
                return BadRequest();
            }
            application.EntryDate = DateTime.Now;
            _context.Applications!.Add(application);
            _context.SaveChanges();
            return Ok(new {application});
        }
    }
}