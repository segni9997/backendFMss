using backend.Interfaces;
using backend.Models;
using backend.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedRequestController : ControllerBase
    {
        private readonly IMedicalRequestRepo _mediicalRequestRepo;
        public MedRequestController(IMedicalRequestRepo medicalRequestRepo)
        {
            _mediicalRequestRepo = medicalRequestRepo;
            
        }
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]

        public IActionResult Getmedreqs()
        {
            var reqs = _mediicalRequestRepo.GetMedicalRequests();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(reqs);
        }
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Pilot))]
        [ProducesResponseType(400)]
        public IActionResult GetMedreq(int id)
        {
            if (!_mediicalRequestRepo.MedrequestExist(id))
                return NotFound();
            var req= _mediicalRequestRepo.GetMedrequest(id);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(req);
        }
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult CreateReq([FromBody] MedicalRequest CreateRequest)
        {
            /* if(Createaircraft == null)
                 return BadRequest(ModelState);  
              var  airCraft =_airCraftRepo.GetAirCrafts().Where(air => air.AirCraftName.Trim() == Createaircraft.AirCraftName).FirstOrDefault();
             if (airCraft != null)
                 ModelState.AddModelError("", "AirCraft is Already Registered");
             return StatusCode(500, ModelState);*/
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var AddMedreq = _mediicalRequestRepo.CreateMedrequest(CreateRequest);
            /*  if (!_airCraftRepo.CreateAircraft(AirCraftadd))
                 ModelState.AddModelError("", "Something went Wrong While Saving");
              return StatusCode(500, ModelState);*/
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok("Succesfully Created");

        }
        [HttpPut("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateMedreq(int id, [FromBody] MedicalRequest updatemedreq)
        {
            if (updatemedreq == null)
                return BadRequest(ModelState);

            if (id != updatemedreq.id)
                return BadRequest(ModelState);
            if (!_mediicalRequestRepo.MedrequestExist(id))
                return NotFound();
            if (!ModelState.IsValid)
                return BadRequest();

            var reqs = _mediicalRequestRepo.UpdateMedReq(updatemedreq);



            return Ok("Updated Succesfully");
        }
    }
}
