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

    public class AircraftRequestController : ControllerBase
    {
        private readonly IAirCraftRequestRepo _requestrepo;
        private readonly IUserRepo _userRepo;
        private readonly JwtService _jwtService;
        public AircraftRequestController(IAirCraftRequestRepo requestRepo,IUserRepo userRepo, JwtService jwtService)
        {
            _requestrepo = requestRepo;
            _userRepo = userRepo;
            _jwtService = jwtService;
        }
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult GetAirCraftRequests()
        {
            var requests = _requestrepo.GetAirCraftRequests();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(requests);
        }
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(AirCraftRequest))]
        [ProducesResponseType(400)]
        public IActionResult GetAirCraftRequest(int id)
        {
            if (!_requestrepo.AircraftRequestExists(id))
                return NotFound();
            var request = _requestrepo.GetAircraftRequest(id);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(request);
        }
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult CreateAircraftRequest([FromBody] AirCraftRequest CreateRequest)
        {
         
                if (!ModelState.IsValid) return BadRequest(ModelState);
                var requestadd = _requestrepo.CreateAircraftRequest(CreateRequest);
                return Ok(requestadd);

            
        }
        [HttpPut("Update/{id}")]
        [ProducesResponseType(400)]
        public IActionResult UpdateairCraftRequest(int id, [FromBody] AirCraftRequest UpdateRequest)
        {
       
                if (UpdateRequest == null)
                    return BadRequest("There is some unfilled field");
                if (id != UpdateRequest.id)
                    return BadRequest(ModelState);
                if (!_requestrepo.AircraftRequestExists(id))
                    return NotFound();
                if (!ModelState.IsValid)
                    return BadRequest();
                var _request = _requestrepo.UpdateAircraftRequest(UpdateRequest);
                return Ok(_request);


          
        }

        [HttpDelete("Aircraft/{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteAirCraftRequest(int id)
        {
     
                if (!_requestrepo.AircraftRequestExists(id))
                    return NotFound();
                var _request = _requestrepo.GetAircraftRequest(id);
                if (!_requestrepo.DeleteAircraftRequest(_request))
                {
                    ModelState.AddModelError("", "somthing is wrong");
                    return StatusCode(500, ModelState);
                }

                return NoContent();

            
    }
 

        
        [HttpGet("Accpted")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult GetAccpted()
        {
            var requests = _requestrepo.GetAccepted();
            if (requests == null)
                return NotFound();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(requests);
        }
        [HttpGet("AccptedArrived")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult GetAccptedArrived()
        {
            var requests = _requestrepo.GetAccptedArrived();
            if (requests == null)
                return NotFound();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(requests);
        }
        [HttpGet("Declined")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult GetDeclined()
        {
            var requests = _requestrepo.Getdeclined();
            if (requests == null)
                return NotFound();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(requests);
        }
        [HttpGet("Pending")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult GetPending()
        {
            var requests = _requestrepo.GetPendingRequests();
            if (requests == null)
                return NotFound();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(requests);
        }

    }
    
}
