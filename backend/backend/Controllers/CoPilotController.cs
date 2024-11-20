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
    public class CoPilotController : ControllerBase
    {
        private readonly ICabinRepo _cabinRepo;
        private readonly IMapper _mapper;
        private readonly IUserRepo _userRepo;
        private readonly IPilotRepo _piplotRepo;
        private readonly ICopilotRepo _copilot;
        private readonly ITechnicianRepo _technicianRepo;
        private readonly JwtService _jwtService;
        public CoPilotController(ICopilotRepo copilot, IUserRepo userRepo, IMapper mapper, IPilotRepo pilot, ICabinRepo cabin, ITechnicianRepo technicianRepo, JwtService jwtService)
        {
            _copilot = copilot;
            _userRepo = userRepo;
            _mapper = mapper;
            _piplotRepo = pilot;
            _cabinRepo = cabin;
            _technicianRepo = technicianRepo;
            _jwtService = jwtService;
        }
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]

        public IActionResult GetCopilots()
        {
            var copilots = _copilot.GetCopilots();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(copilots);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(CoPilot))]
        [ProducesResponseType(400)]
        public IActionResult GetCoPilot(int id)
        {
            if (!_copilot.CopilotExist(id))
                return NotFound();
            var Cabins = _copilot.GetCopilot(id);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(Cabins);
        }
        [HttpGet("getCoPilotbyemail")]
        [ProducesResponseType(200, Type = typeof(CoPilot))]
        [ProducesResponseType(400)]
        public IActionResult GetcoPilotByemail(string email)
        {
            
            var CoPilot = _copilot.GetByEmail(email);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(CoPilot);
        }
        [HttpPost]
        [ProducesResponseType(200)]
 
        public IActionResult CreateCopilot([FromQuery] int userid, [FromBody] CoPilotDto Createcopilot)
        {
            if (_copilot.CopilotExist(userid) || _piplotRepo.pilotUserExist(userid) || _technicianRepo.TechncianUserExist(userid) || _cabinRepo.cabinUserExist(userid))
            {
                return BadRequest("User Already Taken");
            }
            var copilotadd = _mapper.Map<CoPilot>(Createcopilot);
            copilotadd.User = _userRepo.GetUser(userid);
            var add = _copilot.CreateCoPilot(copilotadd);
            return Ok(add);

        }
        [HttpPut("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateCoPilot(int id, [FromBody] CoPilotDto updateCopilot)
        {
 
                if (updateCopilot == null)
                    return BadRequest(ModelState);

                if (id != updateCopilot.id)
                    return BadRequest(ModelState);
                if (!_copilot.CopilotExist(id))
                    return NotFound();
                if (!ModelState.IsValid)
                    return BadRequest();
                var copilot = _mapper.Map<CoPilot>(updateCopilot);
                var update = _copilot.UpdateCoPilot(copilot);




                return Ok(update);
        
         
        }


        [HttpDelete("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteCopilot(int id)
        {
          
              
                if (!_copilot.CopilotExist(id))
                    return NotFound();
                var copilot = _copilot.GetCopilot(id);
                if (!_copilot.DeleteCoPilot(copilot))
                {
                    ModelState.AddModelError("", "somthing is wrong");
                    return StatusCode(500, ModelState);
                }
                return Ok("Deleted");
       

        }
        [HttpGet("/CoPilottHome")]
        [ProducesResponseType(200, Type = typeof(Pilot))]
        [ProducesResponseType(400)]
        public IActionResult GEtHomeCopilot()
        {

            var copilot = _copilot.getHomeCopilot();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(copilot);
        }
    }
}
