using AutoMapper;
using backend.Dto;
using backend.Helper;
using backend.Interfaces;
using backend.Models;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using MimeKit.Text;
using NuGet.Packaging;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightController : ControllerBase
    {
        private readonly IFlightRepo _flightRepo;
        private readonly ICabinRepo _cabinRepo;
        private readonly ITechnicianRepo _technicianRepo;
        private readonly IAirCraftRepo _aircraftRepo;
        private readonly IPilotRepo _pilotRepo;
        private readonly ICopilotRepo _coopilotRepo;
        private readonly IMapper _mapper;
        private readonly JwtService _jwtService;
        private readonly IUserRepo _userRepo;

        public FlightController(IFlightRepo flightRepo, ICopilotRepo copilotRepo
 , IPilotRepo pilotRepo, IAirCraftRepo airCraftRepo, IMapper mapper, ITechnicianRepo technicianRepo, ICabinRepo cabinRepo, JwtService jwtService, IUserRepo userRepo)
        {
            _flightRepo = flightRepo;

            _aircraftRepo = airCraftRepo;
            _coopilotRepo = copilotRepo;
            _pilotRepo = pilotRepo;
            _mapper = mapper;
            _technicianRepo = technicianRepo;
            _cabinRepo = cabinRepo;
            _jwtService = jwtService;
            _userRepo = userRepo;
        }

        [HttpPost("/AssignFlight")]
        [ProducesResponseType(200)]

        public IActionResult CreateFlight([FromQuery] int pilotID, [FromQuery] int CoPilotID, [FromQuery] int airId, [FromBody] FlightDto createFlight)
        {

            try
            {
                var flightadd = _mapper.Map<Flight>(createFlight);
                Pilot pilot = _pilotRepo.GetPilot(pilotID);
                flightadd.Pilot = pilot;
                flightadd.CoPilot = _coopilotRepo.GetCopilot(CoPilotID);
                flightadd.Aircraft = _aircraftRepo.GetAircraft(airId);

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
               
                List<string> useremails = new List<string>();
                useremails.Add(_pilotRepo.GetPilot(pilotID).User.UserEmail);
                useremails.Add(_coopilotRepo.GetCopilot(CoPilotID).User.UserEmail);
                var emails = new MimeMessage();
                foreach (var emaili in useremails)
                {
                    emails.From.Add(MailboxAddress.Parse("ethiopianairlinesflightoffice@gmail.com"));
                    emails.To.Add(MailboxAddress.Parse(emaili));
                    emails.Subject = "flightsechedule";
                    emails.Body = new TextPart(TextFormat.Html)
                    {
                        Text = "you have flight from " + createFlight.Origin + "  to  " + createFlight.Destination + " with departure date/time of  " + createFlight.DepartureDateTime + " ,  arrival date/time " + createFlight.ArrivalDateTime + "  and your aircarft no is  " + _aircraftRepo.GetAircraft(airId).AircraftNo + " . have a nice flight!!! "
                    };
                    using var Smtp = new SmtpClient();
                    Smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                    Smtp.Authenticate("ethiopianairlinesflightoffice@gmail.com", "iwhbcdfoucbdtkro");
                    Smtp.Send(emails);
                    Smtp.Disconnect(true);

                }
                _flightRepo.CreateFlight(flightadd);
            }catch (Exception _)
            {
                return  BadRequest("You are not connected to internet");
            }


            return Ok();

        
           
        }




        [HttpGet("/getFlights")]
        [ProducesResponseType(200, Type = typeof(Flight))]
        [ProducesResponseType(400)]
        public IActionResult GatFlights()
        {
         
               
                var flight = _mapper.Map<List<Flight>>(_flightRepo.GetFlights());

                //if (!ModelState.IsValid)
                //    return BadRequest(ModelState);
                return Ok(flight);
      
           
        }
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Flight))]
        [ProducesResponseType(400)]
        public IActionResult GetFlight(int id)

        {
          
      
                var flight = _mapper.Map<Flight>(_flightRepo.Flight(id));

                return Ok(flight);
       
          
        }
        [HttpGet("pilotID/{id}")]
        [ProducesResponseType(200, Type = typeof(Flight))]
        [ProducesResponseType(400)]
        public IActionResult GetFlightByPilotId(int id)

        {
            if(_flightRepo.GEtFlightByPilotID(id) == null)
            {
              return BadRequest("no flight is Specified");
            }
            var flight = _mapper.Map<List<Flight>>(_flightRepo.GEtFlightByPilotID(id));
            

            return Ok(flight);
        }
       
        [HttpGet("pilotIDDeparted/{id}")]
        [ProducesResponseType(200, Type = typeof(Flight))]
        [ProducesResponseType(400)]
        public IActionResult GetPilotDeparted(int id)

        {
            var flight = _mapper.Map<List<Flight>>(_flightRepo.GetFlightByPilotIDDeparted(id));

            return Ok(flight);
        }
 [HttpGet("CopilotID/{id}")]
        [ProducesResponseType(200, Type = typeof(Flight))]
        [ProducesResponseType(400)]
        public IActionResult GetFlightByCoPilotId(int id)

        {
            var flight = _mapper.Map<List<Flight>>(_flightRepo.GEtFlightByCoPilotID(id));

            return Ok(flight);
        }
        [HttpGet("AirID/{id}")]
        [ProducesResponseType(200, Type = typeof(Flight))]
        [ProducesResponseType(400)]
        public IActionResult GetFlightByAirID(int id)

        {
            var flight = _mapper.Map<List<Flight>>(_flightRepo.GEtFlightByCoPilotID(id));

            return Ok(flight);
        }
        [HttpGet("CopilotIDDeparted/{id}")]
        [ProducesResponseType(200, Type = typeof(Flight))]
        [ProducesResponseType(400)]
        public IActionResult GetCoPilotDeparted(int id)

        {
            var flight = _mapper.Map<List<Flight>>(_flightRepo.GetFlightByCoPilotIDDeparted(id));

            return Ok(flight);
        }

        [HttpGet("/FlightHome")]
        [ProducesResponseType(200, Type = typeof(Flight))]
        [ProducesResponseType(400)]
        public IActionResult GetFlightHome()

        {
            var flight =_flightRepo.getflighthome();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(flight);
        }
        [HttpGet("/Flightdeparted")]
        [ProducesResponseType(200, Type = typeof(Flight))]
        [ProducesResponseType(400)]
        public IActionResult GetflightDeparted()

        {
            var flight =_flightRepo.getFlightdeparted();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(flight);
        }
        [HttpGet("/FlightArrived")]
        [ProducesResponseType(200, Type = typeof(Flight))]
        [ProducesResponseType(400)]
        public IActionResult GetFloightArrived()

        {
            var flight = _flightRepo.getFlightArrived();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(flight);
        }
        [HttpPut("updateFlight{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateFlight(int id, [FromBody] FlightDto updateFlight)
        {
            try
            {
              
                if (updateFlight == null)
                    return BadRequest(ModelState);

                if (id != updateFlight.id)
                    return BadRequest(ModelState);
                if (!_flightRepo.FlightExists(id))
                    return NotFound();
                if (!ModelState.IsValid)
                    return BadRequest();
                var fli = _mapper.Map<Flight>(updateFlight);
                var update = _flightRepo.UpdateFlight(fli);
                return Ok(update);

            }
            catch (Exception _)
            {
                return Unauthorized();
            }
          
        }
        [HttpGet("CabinFlight/{cabingroup}")]
        [ProducesResponseType(200, Type = typeof(Flight))]
        [ProducesResponseType(400)]
        public IActionResult GetFlightByCabinGroup(string cabingroup)

        {
            var flight = _mapper.Map<List<Flight>>(_flightRepo.GetFlightByCabinGroup(cabingroup));

            return Ok(flight);
        }
        [HttpGet("CabinFlightDeparted/{cabingroup}")]
        [ProducesResponseType(200, Type = typeof(Flight))]
        [ProducesResponseType(400)]
        public IActionResult GetFlightDepartedByCabinGroup(string cabingroup)

        {
            var flight = _mapper.Map<List<Flight>>(_flightRepo.GetFlightByCabinGroupD(cabingroup));

            return Ok(flight);
        }
        [HttpGet("CabinFlightArrived/{cabingroup}")]
        [ProducesResponseType(200, Type = typeof(Flight))]
        [ProducesResponseType(400)]
        public IActionResult GetFlightarrivedByCabinGroup(string cabingroup)

        {
            var flight = _mapper.Map<List<Flight>>(_flightRepo.GetcabinArrived(cabingroup));

            return Ok(flight);
        }
        [HttpGet("CabinFlights/{cabingroup}")]
        [ProducesResponseType(200, Type = typeof(Flight))]
        [ProducesResponseType(400)]
        public IActionResult GetFlightByCabinGroupD(string cabingroupD)

        {
            var flight = _mapper.Map<List<Flight>>(_flightRepo.GetFlightByCabinGroup(cabingroupD));

            return Ok(flight);
        }
        [HttpGet("TechnichianFlight/{technicaingroup}")]
        [ProducesResponseType(200, Type = typeof(Flight))]
        [ProducesResponseType(400)]
        public IActionResult GetFlightByTechnicanGroup(string technicaingroup)

        {
            var flight = _mapper.Map<List<Flight>>(_flightRepo.GetFlightbyTechnicanGroup(technicaingroup));

            return Ok(flight);
        }
        [HttpGet("TechnichianFlightArrived/{technicaingroup}")]
        [ProducesResponseType(200, Type = typeof(Flight))]
        [ProducesResponseType(400)]
        public IActionResult GetFlightByTechnicanGroupArrived(string technicaingroup)

        {
            var flight = _mapper.Map<List<Flight>>(_flightRepo.GettechncianArrived(technicaingroup));

            return Ok(flight);
        }
        [HttpGet("TechnichianFlightDeparted/{technicaingroup}")]
        [ProducesResponseType(200, Type = typeof(Flight))]
        [ProducesResponseType(400)]
        public IActionResult GetFlightByTechnicanGroupDeparted(string technicaingroup)

        {
            var flight = _mapper.Map<List<Flight>>(_flightRepo.GettechncianDeparted(technicaingroup));

            return Ok(flight);
        }
        [HttpGet("TechnichianFlights/{technicaingroupD}")]
        [ProducesResponseType(200, Type = typeof(Flight))]
        [ProducesResponseType(400)]
        public IActionResult GetFlightByTechnicanGroupD(string technicaingroupD)

        {
            var flight = _mapper.Map<List<Flight>>(_flightRepo.GetFlightbyTechnicanGroup(technicaingroupD));

            return Ok(flight);
        }
        [HttpDelete("Flight/{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteFlight(int id)
        {


            if (!_flightRepo.FlightExists(id))
                return NotFound();
            var fli = _flightRepo.Flight(id);
            if (!_flightRepo.DeleteFlight(fli))
            {
                ModelState.AddModelError("", "somthing is wrong");
                return StatusCode(500, ModelState);
            }
            return Ok("Deleted");


        }
    }
}
