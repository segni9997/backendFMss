using backend.Helper;
using backend.Interfaces;
using backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TechnicianGroupController : ControllerBase
    {
      private readonly ITeckGroup _technicalGroup;
        private readonly JwtService _jwtService;
        private readonly IUserRepo _userRepo;
        public TechnicianGroupController(ITeckGroup group, JwtService jwtService, IUserRepo userRepo)
        {
            _technicalGroup = group;
            _jwtService = jwtService;
            _userRepo = userRepo;
        }
        [HttpGet("teckgroups/")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult GetTechGroups()
        {
            var getGroups = _technicalGroup.GetGroups();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(getGroups);

        }
        [HttpGet("teckgroupshome/")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult GetTechGroupsHome()
        {
            var getGroups = _technicalGroup.GetTeckhome();
           
            return Ok(getGroups);

        }
        [HttpGet("Teckgroup/{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult GetTeckGroup(int id)
        {
            var getgroup = _technicalGroup.GetTeckgroup(id);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(getgroup);
        }
        [HttpGet("TeckBygroup/{group}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult GetTeckByGroup(string group)
        {
            var getgroup = _technicalGroup.GetTeckbygroup(group);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(getgroup);
        }
        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult CreateTeckgroup([FromBody] TeckGroup Group)
        {

            var finds = _technicalGroup.GetGroups().Where(c => c.TGroup.Trim().ToUpper() == Group.TGroup.TrimEnd().ToUpper())
                  .FirstOrDefault();
            if (finds != null)
            {
                ModelState.AddModelError("", "Role already exists");
                return StatusCode(422, ModelState);
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var add = _technicalGroup.CreateTeckGroup(Group);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(add);

        }
        [HttpPut("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpDateTechGroup(int id, [FromBody] TeckGroup updategroup)
        {
          
                if (updategroup == null)
                    return BadRequest(ModelState);

                if (id != updategroup.tid)
                    return BadRequest(ModelState);
                if (!_technicalGroup.TeckTeckExists(id))
                    return NotFound();
                if (!ModelState.IsValid)
                    return BadRequest();

                var update = _technicalGroup.UpdateTeckGroup(updategroup);
                return Ok(update);

        

        }
        [HttpDelete("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DelteTeckgroup(int id)
        {
            
                if (!_technicalGroup.TeckTeckExists(id))
                    return NotFound();
                var group = _technicalGroup.GetTeckgroup(id);
                if (!_technicalGroup.DeleteTeckGroup(group))
                {
                    ModelState.AddModelError("", "somthing is wrong");
                    return StatusCode(500, ModelState);
                }
                return Ok("Deleted");

           


        }
    }
}
