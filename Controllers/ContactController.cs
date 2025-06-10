using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using my_web_api.Data;
using my_web_api.Models;

namespace my_web_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ContactController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var messages = _context.ContactMessages.ToList();
            return Ok(messages);
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var message = _context.ContactMessages.FirstOrDefault(m => m.contactMessageId == id);
            if (message == null)
                return NotFound($"ID'si {id} olan mesaj bulunamadı.");
            return Ok(message);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Post([FromBody] ContactMessage newMessage)
        {
            if (newMessage == null)
                return BadRequest("Mesaj verisi boş olamaz.");

            newMessage.createdAt = DateTime.UtcNow;
            _context.ContactMessages.Add(newMessage);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetById), new { id = newMessage.contactMessageId }, newMessage);
        }

        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] ContactMessage updatedMessage)
        {
            var existing = _context.ContactMessages.FirstOrDefault(m => m.contactMessageId == id);
            if (existing == null)
                return NotFound($"ID'si {id} olan mesaj bulunamadı.");

            existing.name = updatedMessage.name;
            existing.email = updatedMessage.email;
            existing.subject = updatedMessage.subject;
            existing.message = updatedMessage.message;
            existing.createdAt = updatedMessage.createdAt;

            _context.SaveChanges();

            return Ok(existing);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var existing = _context.ContactMessages.FirstOrDefault(m => m.contactMessageId == id);
            if (existing == null)
                return NotFound($"ID'si {id} olan mesaj bulunamadı.");

            _context.ContactMessages.Remove(existing);
            _context.SaveChanges();

            return Ok($"ID'si {id} olan mesaj silindi.");
        }
    }
}
