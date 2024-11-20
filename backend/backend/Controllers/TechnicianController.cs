using AutoMapper;
using backend.Dto;
using backend.Helper;
using backend.Interfaces;
using backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TechnicianController : ControllerBase
    {
        private readonly ITechnicianRepo _technicianRepo;
        private readonly IUserRepo _userRepo;
        private readonly IMapper _mapper;
        private readonly ICabinRepo _cabinRepo;
        private readonly JwtService _jwtService;
        private readonly IPilotRepo _piplotRepo;
        private readonly ICopilotRepo _copilotRepo;
        private readonly ITeckGroup _teckGroup;

        public TechnicianController(ITeckGroup teckGroup,IUserRepo userRepo, ITechnicianRepo technicianRepo,JwtService jwtService, IMapper mapper, IPilotRepo pilotRepo,ICopilotRepo copilotRepo,  ICabinRepo cabin)
        {

            _teckGroup = teckGroup;
            _userRepo = userRepo;
            _technicianRepo = technicianRepo;
            _mapper = mapper;
            _piplotRepo = pilotRepo;
            _cabinRepo = cabin;
            _copilotRepo = copilotRepo;
            _jwtService = jwtService;
            
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(Technician))]
        [ProducesResponseType(400)]
        public IActionResult GetTechnicains()
        {
          
          
       
                var tecks = _mapper.Map<List<Technician>>(_technicianRepo.GetTechnicians());
                if (tecks == null)
                {
                    return BadRequest("No Element");
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                return Ok(tecks);
      
         
           
        }
        [HttpGet("HomeTechnicain")]
        [ProducesResponseType(200, Type = typeof(Technician))]
        [ProducesResponseType(400)]
        public IActionResult GetHomeTechnincian()
        {
            var tecks = _mapper.Map<List<Technician>>(_technicianRepo.getHomeTEchnicain());
            if (tecks == null)
            {
                return BadRequest("No Element");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(tecks);



        }
        [HttpGet("EmailTechnicain")]
        [ProducesResponseType(200, Type = typeof(Technician))]
        [ProducesResponseType(400)]
        public IActionResult GetemailsTechnincian(string group)
        {
            var tecks = _mapper.Map<List<Technician>>(_technicianRepo.GetTEchncianByGroup(group));
            if (tecks == null)
            {
                return BadRequest("No Element");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok( new
            {
                Tecks = tecks,
                Count = tecks.Count

            });



        }
        [HttpGet("getTechncianbyemail")]
        [ProducesResponseType(200, Type = typeof(Technician))]
        [ProducesResponseType(400)]
        public IActionResult GetTechniacnByemail(string email)
        {

            var techncian = _technicianRepo.GetTechncainByEmail(email);
    
            return Ok(techncian);
        }
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Technician))]
        [ProducesResponseType(400)]
        public IActionResult GetTechnician(int id)
        {
          
             

                if (!_technicianRepo.TechnchianExist(id))
                    return NotFound();
                var tecks = _mapper.Map<Technician>(_technicianRepo.GetTechnician(id));
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                return Ok(tecks);
      
        

        }
        [HttpPost]
        [ProducesResponseType(200)]

        public IActionResult CreateTechnician([FromQuery] int userId, [FromBody] TechncianDto createtechncian)
        {
          
    
                if (_copilotRepo.CopilotExist(userId) || _piplotRepo.pilotUserExist(userId) || _technicianRepo.TechncianUserExist(userId) || _cabinRepo.cabinUserExist(userId))
                {
                    return BadRequest("User Already Taken");
                }


                var finds = _technicianRepo.GetTechnicians().Where(c => c.FullName.Trim().ToUpper() == createtechncian.FullName.TrimEnd().ToUpper())
               .FirstOrDefault();
                if (finds != null)
                {
                    ModelState.AddModelError("", "Pilot already exists");
                    return StatusCode(422, ModelState);
                }
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                var teck = _mapper.Map<Technician>(createtechncian);
                teck.User = _userRepo.GetUser(userId);
      
                var add = _technicianRepo.CreateTechnician(teck);
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                return Ok(add);

            
       
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteTechnician(int id)
        {
              
                if (!_technicianRepo.TechnchianExist(id))
                    return NotFound();
                var teck = _technicianRepo.GetTechnician(id);
                if (!_technicianRepo.DeleteTechnician(teck))
                {
                    ModelState.AddModelError("", "somthing is wrong");
                    return StatusCode(500, ModelState);
                }

                return Ok("Deleted");

         
         

        }
    }
}
