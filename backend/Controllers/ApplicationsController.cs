using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    }
}