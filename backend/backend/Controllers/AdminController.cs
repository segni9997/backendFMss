using AutoMapper;
using backend.Dto;
using backend.Helper;
using backend.Interfaces;
using backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   
    public class AdminController : ControllerBase
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
        public AdminController(ITreasury treasury, IMapper mapper, ICabinRepo cabinRepo,
            IPilotRepo pilotRepo, ICopilotRepo copilotRepo, ITechnicianRepo technicianRepo,
            IAdmin admin, IUserRepo userRepo,JwtService jwtService )
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
        [HttpGet("/getAdmins")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]

        public IActionResult GetAdmins()
        {
            var admin = _mapper.Map<List<Admin>>(_admin.GetAdmins());

            return Ok(admin);
        }
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Tresury))]
        [ProducesResponseType(400)]
        public IActionResult GetAdmin(int id)
        {
            if (!_admin.AdmintExist(id))
                return NotFound();
            var admin = _mapper.Map<Admin>(_admin.GetADmin(id));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(admin);
        }
        [HttpGet("byemail")]
        [ProducesResponseType(200, Type = typeof(Tresury))]
        [ProducesResponseType(400)]
        public IActionResult GetAdminByEmail(string email)
        {
            var admin = _mapper.Map<Admin>(_admin.GetAdminByEmail(email));

           
            return Ok(admin);
        }
        [HttpPost]
        [ProducesResponseType(200)]

        public IActionResult CreateAdmin([FromQuery] int userid, [FromBody] AdminDto CreateAdmin)
        {
          
      

            var finds = _admin.GetAdmins().Where(c => c.FullName.Trim().ToUpper() == CreateAdmin.FullName.TrimEnd().ToUpper())
                 .FirstOrDefault();

            if (finds != null)
            {
                ModelState.AddModelError("", "Treasury already exists");
                return StatusCode(422, ModelState);
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var adminadd = _mapper.Map<Admin>(CreateAdmin);
            adminadd.User = _userRepo.GetUser(userid);
            var add = _admin.CreateAdmin(adminadd);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(add);

        }
        [HttpPut("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateAdmin(int id, [FromBody] AdminDto updateAdmin)
        {
            try
            {
                var jwt = Request.Cookies["jwt"];
                var token = _jwtService.Verify(jwt);
                int UserId = int.Parse(token.Issuer);
                var user = _userRepo.GetUser(UserId);
                if (updateAdmin == null)
                    return BadRequest(ModelState);

                if (id != updateAdmin.id)
                    return BadRequest(ModelState);
                if (!_admin.AdmintExist(id))
                    return NotFound();
                if (!ModelState.IsValid)
                    return BadRequest();
                var admin = _mapper.Map<Admin>(updateAdmin);
                var update = _admin.UpdateAdmin(admin);
                return Ok(update);

            }
            catch (Exception _)
            {
                return Unauthorized();
            }
          
        }
        [HttpDelete("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteAdmin(int id)
        {
            try
            {
                var jwt = Request.Cookies["jwt"];
                var token = _jwtService.Verify(jwt);
                int UserId = int.Parse(token.Issuer);
                var user = _userRepo.GetUser(UserId);
                if (!_admin.AdmintExist(id))
                    return NotFound();
                var admin = _admin.GetADmin(id);
                if (!_admin.DeleteAdmin(admin))
                {
                    ModelState.AddModelError("", "somthing is wrong");
                    return StatusCode(500, ModelState);
                }
                return Ok("Deleted");
            }
            catch (Exception _)
            {
                return Unauthorized();
            }
          

        }
    }
}
