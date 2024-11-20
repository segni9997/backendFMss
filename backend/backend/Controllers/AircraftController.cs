using AutoMapper;
using backend.Dto;
using backend.Helper;
using backend.Interfaces;
using backend.Models;
using backend.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AircraftController : ControllerBase
    {
        private readonly IAirCraftRepo _airCraftRepo;
        private readonly IMapper _mapper;
        private readonly IUserRepo _userRepo;
        private readonly JwtService _jwtService;
        public AircraftController(IAirCraftRepo airCraftRepo, IMapper mapper, IUserRepo userRepo, JwtService jwtService)
        {
            _airCraftRepo = airCraftRepo;
            _mapper = mapper;
            _userRepo = userRepo;
            _jwtService = jwtService;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(Aircraft))]
        [ProducesResponseType(400)]

        public IActionResult GetAirCrafts()
        { 
            //var Air = _mapper.Map<List<Aircraft>>(_airCraftRepo.GetAirCrafts());
            var air = _airCraftRepo.GetAirCrafts();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(air);
        }


        [HttpGet("airbyid/{id}")]
        [ProducesResponseType(200, Type = typeof(Aircraft))]
        [ProducesResponseType(400)]
        public IActionResult GetAirCraft(int id)
        {
            if (!_airCraftRepo.AircraftExists(id))
                return NotFound();
            var Aircraft = _airCraftRepo.GetAircraft(id);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(Aircraft);
        }

        /*    [HttpGet ("{AirCraft/AirCraftModel}")]
            [ProducesResponseType(200, Type = typeof(Aircraft))]
            [ProducesResponseType(400)]
            public IActionResult GetAirCraft(string name)
            {
                if (!_airCraftRepo.AircraftExists(id))
                    return NotFound();
                var AirCraftmodel = _airCraftRepo.GetAircraft(name);
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                return Ok(Aircraft);

       
        */
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult CreateAirCraft([FromBody] AirCraftDto Createaircraft)
        {
            if (_airCraftRepo == null)
                return BadRequest("its Empity");
            
            if (_airCraftRepo.getAircarftByNo(Createaircraft.AircraftNo) == true)
            {
                return BadRequest("AirCarft No is already taken");
            }

            var air = _mapper.Map<Aircraft>(Createaircraft);
            var add = _airCraftRepo.CreateAircraft(air);
                if (!ModelState.IsValid)
                return BadRequest(ModelState);
                
            return Ok("created");

        }
        [HttpPut("update/{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
      
        public IActionResult UpdateAircraft(int id, [FromBody] AirCraftDto Updateaircraft)
        {
    
                if (Updateaircraft == null)
                    return BadRequest(ModelState);

                if (id != Updateaircraft.id)
                    return BadRequest(ModelState);
                if (!_airCraftRepo.AircraftExists(id))
                    return NotFound();


                var _aircraft = _mapper.Map<Aircraft>(Updateaircraft);
                var update = _airCraftRepo.UpdateAircraft(_aircraft);

                if (!ModelState.IsValid)
                    return BadRequest();

                return Ok("Updated Succesfully");

        

        }


        [HttpDelete("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteAircraft(int id)
        {
            try
            {
               
                if (!_airCraftRepo.AircraftExists(id))
                    return NotFound();
                var _airCraft = _airCraftRepo.GetAircraft(id);
                if (!_airCraftRepo.DeleteAircraft(_airCraft))
                {
                    ModelState.AddModelError("", "somthing is wrong");
                    return StatusCode(500, ModelState);
                }
                if (!ModelState.IsValid)
                    return BadRequest();
                return NoContent();

            }
            catch (Exception _)
            {
                return Unauthorized();
            }
           

        }
        [HttpGet("ArrivedWell")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult GetArrivedWell()
        {
            var requests = _airCraftRepo.GetArrivedwell();
            if (requests == null)
                return NotFound();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(requests);
        }
        [HttpGet("ArrivedPending")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult GetArrivedPending()
        {
            var requests = _airCraftRepo.GetArrivedPending();
            if (requests == null)
                return NotFound();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(requests);
        }
        [HttpGet("Departured")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult GetDepartured()
        {
            var requests = _airCraftRepo.Getdepartured();
            if (requests == null)
                return NotFound();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(requests);
        }

    }
}
