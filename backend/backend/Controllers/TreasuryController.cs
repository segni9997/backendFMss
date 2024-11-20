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
    public class TreasuryController : ControllerBase
    {
        private readonly IPilotRepo _pilot;
        private readonly IUserRepo _userRepo;
        private readonly IMapper _mapper;
        private readonly ICabinRepo _cabinRepo;
        private readonly ICopilotRepo _copilotRepo;
        private readonly ITechnicianRepo _technicianRepo;
        private readonly ITreasury _treasury;
        private readonly IAdmin _admin;
        private readonly JwtService _jwtService;
        public TreasuryController( ITreasury treasury, IMapper mapper, ICabinRepo cabinRepo,
            IPilotRepo pilotRepo, ICopilotRepo copilotRepo, ITechnicianRepo technicianRepo,
            IAdmin admin, IUserRepo userRepo , JwtService jwtService)
        {
            _mapper = mapper;
            _cabinRepo = cabinRepo;
            _pilot = pilotRepo;
            _userRepo = userRepo;
            _treasury = treasury;
            _admin = admin;
            _copilotRepo = copilotRepo;
            _technicianRepo = technicianRepo;
            _jwtService = jwtService;
            
        }
        [HttpGet("/gettresuries")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]

        public IActionResult GetTreasuries()
        {
     
       

                var treasuries = _mapper.Map<List<Tresury>>(_treasury.GetTreasuries());

                return Ok(treasuries);
      
        }
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Tresury))]
        [ProducesResponseType(400)]
        public IActionResult GetTreasury(int id)
        {
         
    
                if (!_treasury.TreasurytExist(id))
                    return NotFound();
                var treasury = _mapper.Map<Tresury>(_treasury.GetTreasury(id));

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                return Ok(treasury);
      
         
        }
        [HttpPost]
        [ProducesResponseType(200)]

        public IActionResult CreateTreasury([FromQuery] int userid, [FromBody] TreasuryDto CreateTresury)
        {
          
                if (_copilotRepo.CopilotExist(userid) || _pilot.pilotUserExist(userid) || _technicianRepo.TechncianUserExist(userid) || _cabinRepo.cabinUserExist(userid) || _admin.AdminUserExist(userid) || _treasury.TreasryUserExist(userid))
                {
                    return BadRequest("User Already Taken");
                }

                var finds = _treasury.GetTreasuries().Where(c => c.FullName.Trim().ToUpper() == CreateTresury.FullName.TrimEnd().ToUpper())
                     .FirstOrDefault();

                if (finds != null)
                {
                    ModelState.AddModelError("", "Treasury already exists");
                    return StatusCode(422, ModelState);
                }
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                var treasuryadd = _mapper.Map<Tresury>(CreateTresury);
                treasuryadd.User = _userRepo.GetUser(userid);
                var add = _treasury.CreateTreasury(treasuryadd);
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                return Ok(add);
     
           

        }
        [HttpPut("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateTreasury(int id, [FromBody] TreasuryDto updatetreasury)
        {
            
                if (updatetreasury == null)
                    return BadRequest(ModelState);

                if (id != updatetreasury.id)
                    return BadRequest(ModelState);
                if (!_treasury.TreasurytExist(id))
                    return NotFound();
                if (!ModelState.IsValid)
                    return BadRequest();
                var treasury = _mapper.Map<Tresury>(updatetreasury);
                var update = _treasury.UpdateTreasury(treasury);
                return Ok(update);
     
        }
        [HttpDelete("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteTreasury(int id)
        {
           
                if (!_treasury.TreasurytExist(id))
                    return NotFound();
                var tresury = _treasury.GetTreasury(id);
                if (!_treasury.DeleteTreasury(tresury))
                {
                    ModelState.AddModelError("", "somthing is wrong");
                    return StatusCode(500, ModelState);
                }
                return Ok("Deleted");
      
        
           

        }
    }
}
