using backend.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApplicationsController : ControllerBase
    {
        private readonly DataContext _context;
        public ApplicationsController(DataContext context) { _context = context; }
        
        /// <summary>
        /// Retrieves unsolved applications in descending order by resolution date
        /// </summary>
        /// <response code="200">Returns list of applications</response>
        /// <returns>List of applications</returns>
        [HttpGet]
        public IActionResult GetApplications() {
            return Ok(_context.Applications?
                    .Where(a => a.IsSolved == false)
                    .OrderByDescending(a => a.ResolutionDate).ToList());
        }

        /// <summary>
        /// Changes the status of an application with given id to true (solved) 
        /// </summary>
        /// <param name="id"> Id of the application which status to change </param>
        /// <response code="200">Returns the newly changed application</response>
        /// <response code="400">Bad request if the ID is not unique, description length is not between 1 and 500 characters, or the resolution date is in the past</response>
        /// <returns> Changed application </returns>
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

        /// <summary>
        /// Adds new application to the database
        /// </summary>
        /// <param name="application"> Application to add to the database </param>
        /// <response code="200">Returns the newly added application</response>
        /// <response code="400">Bad request if the ID is not unique, description length is not between 1 and 500 characters, or the resolution date is in the past</response>
        /// <returns> Added application </returns>
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

    // For testing purposes
    public class ApiResponse
    {
        public Application? Application { get; set; }
    }
}