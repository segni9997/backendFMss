using backend.Interfaces;
using backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactUsController : ControllerBase
    {
        private readonly IContactUs _contactus;
        public ContactUsController(IContactUs contactUs)
        {
            _contactus = contactUs;

        }
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult GetContacts()
        {
            var contacts = _contactus.GetContacts();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(contacts);
        }
        /* [HttpGet("contactus/Review")]
         [ProducesResponseType(200 ,Type = typeof(ContactUs))]
         [ProducesResponseType(400)]
         public IActionResult GetByreview(string review)
         {
             if (review == null)
                 return Ok("all are Reviewed");
             var reviews = _contactus.GetReviewedContact(review);
             if (!ModelState.IsValid)
                 return BadRequest(ModelState);

             return Ok(reviews);
         }*/
        [HttpPost("submit")]
        [ProducesResponseType(200, Type = typeof(ContactUs))]
        [ProducesResponseType(400)]
        public IActionResult CreateContact(ContactUs contact)
        {
            var createCo = _contactus.CreateContact(contact);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(createCo);

        }
    }
}
