using backend.Helper;
using backend.Interfaces;
using backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CabinGroupController : ControllerBase
    {
    
            private readonly ICabinGroup _cabingroup;
            private readonly JwtService _jwtService;
            private readonly IUserRepo _userRepo;
            public CabinGroupController(ICabinGroup group, JwtService jwtService, IUserRepo userRepo)
            {
                _cabingroup = group;
                _jwtService = jwtService;
                _userRepo = userRepo;
            }
            [HttpGet("Cabingroups/")]
            [ProducesResponseType(200)]
            [ProducesResponseType(400)]
            public IActionResult GetCabinGroups()
            {
                var getGroups = _cabingroup.GetGroups();
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                return Ok(getGroups);

            }
            [HttpGet("Cabingroup/{id}")]
            [ProducesResponseType(200)]
            [ProducesResponseType(400)]
            public IActionResult GetCabinGroup(int id)
            {
                var getgroup = _cabingroup.GetCabgroup(id);
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                return Ok(getgroup);
            }
        [HttpGet("CabingroupHome/")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult GetCabinGroupHome()
        {
            var getgroup = _cabingroup.GetCabinHome();
    
            return Ok(getgroup);
        }
        [HttpGet("Cabinbygroup/")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult GetCabinbyGroupHome(string group)
        {
            var getgroup = _cabingroup.GetByGroup(group);

            return Ok(getgroup);
        }
        [HttpPost]
            [ProducesResponseType(200)]
            public IActionResult CreateCabingroup([FromBody] Cabingroup Group)
            {

                var finds = _cabingroup.GetGroups().Where(c => c.CGroup.Trim().ToUpper() == Group.CGroup.TrimEnd().ToUpper())
                      .FirstOrDefault();
                if (finds != null)
                {
                    ModelState.AddModelError("", "Role already exists");
                    return StatusCode(422, ModelState);
                }
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                var add = _cabingroup.CreateCabinGroup(Group);
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                return Ok(add);

            }
            [HttpPut("{id}")]
            [ProducesResponseType(400)]
            [ProducesResponseType(204)]
            [ProducesResponseType(404)]
            public IActionResult UpDateCabinGroup(int id, [FromBody] Cabingroup updategroup)
            {
              
           
                    if (updategroup == null)
                        return BadRequest(ModelState);

                    if (id != updategroup.cId)
                        return BadRequest(ModelState);
                    if (!_cabingroup.CabinGroupExists(id))
                        return NotFound();
                    if (!ModelState.IsValid)
                        return BadRequest();

                    var update = _cabingroup.UpdateCabinGroup(updategroup);
                    return Ok(update);

             

            }
            [HttpDelete("{id}")]
            [ProducesResponseType(400)]
            [ProducesResponseType(204)]
            [ProducesResponseType(404)]
            public IActionResult DelteTeckgroup(int id)
            {
                
                    if (!_cabingroup.CabinGroupExists(id))
                        return NotFound();
                    var group = _cabingroup.GetCabgroup(id);
                    if (!_cabingroup.DeleteCabinGroup(group))
                    {
                        ModelState.AddModelError("", "somthing is wrong");
                        return StatusCode(500, ModelState);
                    }
                    return Ok("Deleted");

             


            }

        }
}
