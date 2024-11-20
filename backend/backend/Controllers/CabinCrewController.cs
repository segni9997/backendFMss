using AutoMapper;
using backend.Dto;
using backend.Helper;
using backend.Interfaces;
using backend.Models;
using backend.Repository;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CabinCrewController :ControllerBase

    {
        private readonly ICabinRepo _cabinRepo;
        private readonly IMapper _mapper;
        private readonly IUserRepo _userRepo;
        private readonly IPilotRepo _piplotRepo;    
        private readonly ICopilotRepo _copilotRepo;  
        private readonly ITechnicianRepo _technicianRepo;
        private readonly JwtService _jwtService;
        private readonly ICabinGroup _cabinGroup;

        public CabinCrewController(ICabinGroup cabinGroup,ICabinRepo cabinRepo, IMapper mapper, IUserRepo userRepo, IPilotRepo piplotRepo, ICopilotRepo opilotRepo, ITechnicianRepo technicianRepo,JwtService jwtService)
        {
            _cabinGroup = cabinGroup;
            _cabinRepo = cabinRepo;
            _mapper = mapper;
            _userRepo = userRepo;
            _piplotRepo = piplotRepo;
            _copilotRepo = opilotRepo;
            _technicianRepo = technicianRepo;
            _jwtService = jwtService;
        }
        [HttpGet("CabinCrew")]
        [ProducesResponseType(200, Type = typeof(Cabincrew))]

        [ProducesResponseType(400)]

        public IActionResult GetCabins()
        {
            var Cabins = _mapper.Map<List<Cabincrew>>(_cabinRepo.GetCabins());
            
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(Cabins);
        }
        [HttpGet("CabinCrewhome")]
        [ProducesResponseType(200, Type = typeof(Cabincrew))]
        [ProducesResponseType(400)]
        public IActionResult GetCabinsHome()
        {
            var Cabins = _mapper.Map<List<Cabincrew>>(_cabinRepo.GetCabinsHome());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(Cabins);
        }
       

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Cabincrew))]
        [ProducesResponseType(400)]
        public IActionResult GetCabincrew(int id)
        {
            if (!_cabinRepo.CabinExist(id))
                return NotFound();
            var Cabins = _mapper.Map<CabinCrewDto>(_cabinRepo.GetCabin(id));

          
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(Cabins);
        }
        [HttpGet("cabinByUserId{id}")]
        [ProducesResponseType(200, Type = typeof(Cabincrew))]
        [ProducesResponseType(400)]
        public IActionResult GetCabinByUserId(int id)
        {
           
            var Cabins = _cabinRepo.GetCabinbyUserId(id);


            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(Cabins);
        }
        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult Createcabin([FromQuery] int userId , [FromBody] CabinCrewDto createCabin)
        {
        
             
                if (_copilotRepo.CopilotExist(userId) || _piplotRepo.pilotUserExist(userId) || _technicianRepo.TechncianUserExist(userId) || _cabinRepo.cabinUserExist(userId))
                {
                    return BadRequest("User Already Taken");
                }

                var cabinadd = _mapper.Map<Cabincrew>(createCabin);
                cabinadd.User = _userRepo.GetUser(userId);
               
                var add = _cabinRepo.CreateCabin(cabinadd);

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                return Ok(add);

     
           

        }

        [HttpGet("getCabinbyemail")]
        [ProducesResponseType(200, Type = typeof(Cabincrew))]
        [ProducesResponseType(400)]
        public IActionResult GetCabinByemail(string email)
        {

            var cabin = _cabinRepo.GetCabinByEmai(email);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(cabin);
        }
        [HttpPut("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateCabin(int id, [FromBody] CabinCrewDto ucabin)
        {

            if (ucabin == null)
                return BadRequest(ModelState);

            if (id != ucabin.id)
                return BadRequest(ModelState);
            if (!_cabinRepo.CabinExist(id))
                return NotFound();
            if (!ModelState.IsValid)
                return BadRequest();
            var cabinu = _mapper.Map<Cabincrew>(ucabin);
            var update = _cabinRepo.UpdateCabin(cabinu);
            return Ok(update);


        }


    }
}
