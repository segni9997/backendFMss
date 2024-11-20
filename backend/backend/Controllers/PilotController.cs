using AutoMapper;
using backend.Dto;
using backend.Helper;
using backend.Interfaces;
using backend.Models;
using backend.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PilotController : ControllerBase
    {
        private readonly IPilotRepo _pilot;
        private readonly IUserRepo _userRepo;
        private readonly IMapper _mapper;
        private readonly ICabinRepo _cabinRepo;
        private readonly JwtService _jwtService;
        private readonly ICopilotRepo _copilotRepo;
        private readonly ITechnicianRepo _technicianRepo;
        public PilotController(IPilotRepo pilot,JwtService jwtService, IUserRepo userRepo, IMapper mapper,ITechnicianRepo technicianRepo, ICabinRepo cabin, ICopilotRepo copilotRepo)
        {
            _pilot = pilot;
            _jwtService = jwtService;
            _userRepo = userRepo;
            _mapper = mapper;
            _cabinRepo = cabin;
             _technicianRepo = technicianRepo;
            _copilotRepo = copilotRepo;

        }
        [HttpGet("/getpilots")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]

        public IActionResult Getpilots() { 
       
                var pilots = _mapper.Map<List<Pilot>>(_pilot.GetPilots());

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                return Ok(pilots);

 
         
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Pilot))]
        [ProducesResponseType(400)]
        public IActionResult GetPilot(int id)
        {
      
          
                if (!_pilot.PilotExist(id))
                    return NotFound();
                var pilot = _mapper.Map<Pilot>(_pilot.GetPilot(id));

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                return Ok(pilot);

            
    
           
        }
        [HttpGet("getByEmail")]
        [ProducesResponseType(200, Type = typeof(Pilot))]
        [ProducesResponseType(400)]
        public IActionResult GetByEmail(string email)
        {


        
            var pilot = _mapper.Map<Pilot>(_pilot.GetByemail(email));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(pilot);




        }
        [HttpPost]
        [ProducesResponseType(200)]
   
        public IActionResult Createpilot([FromQuery] int userid, [FromBody] PilotDto Createpilot)
        {
        
           
                if (_copilotRepo.CopilotExist(userid) || _pilot.pilotUserExist(userid) || _technicianRepo.TechncianUserExist(userid) || _cabinRepo.cabinUserExist(userid))
                {
                    return BadRequest("User Already Taken");
                }
                var finds = _pilot.GetPilots().Where(c => c.FullName.Trim().ToUpper() == Createpilot.FullName.TrimEnd().ToUpper())
                     .FirstOrDefault();
                if (finds != null)
                {
                    ModelState.AddModelError("", "Pilot already exists");
                    return StatusCode(422, ModelState);
                }
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                var pilotadd = _mapper.Map<Pilot>(Createpilot);
                pilotadd.User = _userRepo.GetUser(userid);
                var add = _pilot.CreatePilot(pilotadd);
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                return Ok(add);

     
        }
        [HttpPut("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateCoPilot(int id, [FromBody] PilotDto updatepilot)
        {
         
                if (updatepilot == null)
                    return BadRequest(ModelState);

                if (id != updatepilot.id)
                    return BadRequest(ModelState);
                if (!_pilot.PilotExist(id))
                    return NotFound();
                if (!ModelState.IsValid)
                    return BadRequest();
                var pilot = _mapper.Map<Pilot>(updatepilot);
                var update = _pilot.UpdatePilot(pilot);
                return Ok(update);

          
        }


        [HttpDelete("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteCopilot(int id)
        {
      
                if (!_pilot.PilotExist(id))
                    return NotFound();
                var pilot = _pilot.GetPilot(id);
                if (!_pilot.DeletePilot(pilot))
                {
                    ModelState.AddModelError("", "somthing is wrong");
                    return StatusCode(500, ModelState);
                }
                return Ok("Deleted");

      
          

        }
        [HttpGet("pilotHome")]
        [ProducesResponseType(200, Type = typeof(Pilot))]
        [ProducesResponseType(400)]
        public IActionResult getHomePilot() {
        
            
                var pilot = _pilot.GetHomePilot();

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                return Ok(pilot);
      

           
        }

    }
}
