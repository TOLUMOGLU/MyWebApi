using System.Text.Json;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using my_web_api.Data;
using my_web_api.Models;
using static my_web_api.Controllers.AboutController;

namespace my_web_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public ProjectController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetProject()
        {
            var project = _context.Projects.ToList();
            return Ok(project);
        }

        [Authorize]
        [HttpPost]
        public IActionResult PostProject([FromBody] Project newProject)
        {
            if (newProject == null)
                return BadRequest("AboutMe verisi boş olamaz.");

            _context.Projects.Add(newProject);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetProject), new { id = newProject.projectId }, newProject);
        }

        [Authorize]
        [HttpPut("{id}")]
        public IActionResult PutProject(int id , [FromBody] Project uptadeProject)
        {
            var existingProject = _context.Projects.FirstOrDefault(a => a.projectId == id);
            if(existingProject == null)
            {
                BadRequest("");
            }
            existingProject.title = uptadeProject.title;
            existingProject.projectUrl = uptadeProject.projectUrl;
            existingProject.imageUrl = uptadeProject.imageUrl;
            existingProject.description = uptadeProject.description;
            existingProject.createdAt = uptadeProject.createdAt;
            existingProject.category = uptadeProject.category;

            _context.SaveChanges();
            return Ok();
        }

        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult DeleteProject(int id) 
        {
            var existingProject = _context.Projects.FirstOrDefault(a => a.projectId.Equals(id));

            if (existingProject == null)
                return NotFound($"ID'si {id} olan kayıt bulunamadı.");

            _context.Projects.Remove(existingProject);

            _context.SaveChanges();
            return Ok($"ID'si {id} olan kayıt başarıyla silindi.");
        }

    }
}
