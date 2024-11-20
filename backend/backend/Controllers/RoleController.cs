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
    public class RoleController : ControllerBase
    {
        private readonly IRole _role;
        private readonly JwtService _jwtService;
        private readonly IUserRepo _userRepo;   
        public RoleController(IRole role, JwtService jwtService, IUserRepo userRepo)
        {
            _role = role;
            _jwtService = jwtService;
            _userRepo = userRepo;
        }
        [HttpGet("Roles/")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult GetRoles()
        {
            var getusers = _role.Getroles();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(getusers);

        }
        [HttpGet("Role/{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult GetRole( int id)
        {
            var getRole = _role.Getrole(id);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(getRole);
        }
        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult CreateRole([FromBody] Role role)
        {
   
                var finds = _role.Getroles().Where(c => c.UserRole.Trim().ToUpper() == role.UserRole.TrimEnd().ToUpper())
                      .FirstOrDefault();
                if (finds != null)
                {
                    ModelState.AddModelError("", "Role already exists");
                    return StatusCode(422, ModelState);
                }
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                var add =_role.Createrole(role);
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                return Ok(add);
     
        }
        [HttpPut("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpDaterole(int id, [FromBody] Role updaterole)
        {
     
                if (updaterole == null)
                    return BadRequest(ModelState);

                if (id != updaterole.id)
                    return BadRequest(ModelState);
                if (!_role.RoleExist(id))
                    return NotFound();
                if (!ModelState.IsValid)
                    return BadRequest();
             
                var update = _role.UpdateRole(updaterole);
                return Ok(update);

      

        }
        [HttpDelete("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult Delterole(int id)
        {
            
                if (!_role.RoleExist(id))
                    return NotFound();
                var role = _role.Getrole(id);
                if (!_role.DeleteRole(role))
                {
                    ModelState.AddModelError("", "somthing is wrong");
                    return StatusCode(500, ModelState);
                }
                return Ok("Deleted");

      


        }
    }
}
