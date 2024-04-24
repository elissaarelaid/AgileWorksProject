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
            if (applicationToChange.IsSolved) {
                return BadRequest("Application is already solved");
            }
            applicationToChange.IsSolved = true;
            _context.SaveChanges();
            return Ok(new ApiResponse { Application = applicationToChange });
        }

        [HttpPost]
        public IActionResult AddApplication([FromBody] Application application) {
            if (_context.Applications!.Find(application.Id) != null) {
                return BadRequest("There is already an application with this id");
            }
            if (string.IsNullOrEmpty(application.Description) || application.Description.Length > 500) {
                return BadRequest("Description length must be between 1 and 500 characters");
            }
            application.EntryDate = DateTime.Now;
            if (application.ResolutionDate < DateTime.Now) { 
                return BadRequest("Resolution date cannot be past");
            }
            _context.Applications.Add(application);
            _context.SaveChanges();
            return Ok(new ApiResponse { Application = application });
        }
    }

    public class ApiResponse
    {
        public Application Application { get; set; }
    }
}