using AutoMapper;
using backend.Data;
using backend.Dto;
using backend.Helper;
using backend.Interfaces;
using backend.Models;
using BCrypt.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography.X509Certificates;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class UserController:ControllerBase
    {
        private readonly IUserRepo _userRepo;
        private readonly JwtService _jwtService;
        private readonly IRole _role;
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public UserController(IUserRepo userRepo, JwtService jwtService, IRole role,DataContext dataContext,IMapper mapper)
        {
            _userRepo = userRepo;
            _jwtService = jwtService;
            _role = role;
            _context = dataContext;
            _mapper = mapper;
        }
        [HttpGet("user/")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]

        public IActionResult GetUsers()
        {
      

                var getusers = _userRepo.GetUsers();
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                return Ok(getusers);
         
        }
        [HttpGet("user/usernameandpassword")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult GetUsernameAndpasscode(string username, string passcode)
        {
      
            if (!(_userRepo.UserexistPassword(passcode) && _userRepo.UserexistUser(username))) 
                return  BadRequest("you are not in the system");
            var userr = _userRepo.GetUsernamePassword(username, passcode);

            return Ok(userr);
        }
        [HttpGet("user/emailget")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult GetByemail(string email)
        {

            var userr = _userRepo.GetCabinByUserEmail(email);

            return Ok(userr);
        }

        [HttpGet("user/userEmail")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult getUserEmail(string Email)
        {
            if(!_userRepo.UserEmail(Email))
                return BadRequest("you are not in the system");
            var userr = _userRepo.GetByEmail(Email);

            return Ok(userr.id);
        }


        [HttpGet("/loggedinUser")]
        [ProducesResponseType(200, Type = typeof(User))]
        [ProducesResponseType(400)]
        public IActionResult GetUser()
        {
            try
            {
                var jwt = Request.Cookies["jwt"];
                var token = _jwtService.Verify(jwt);
                int UserId = int.Parse(token.Issuer);
                var user = _userRepo.GetUser(UserId);
                //if (!_userRepo.Userexist(id))
                //    return NotFound();
                //var getuser = _userRepo.GetUser(id);
                //if (!ModelState.IsValid)
                //    return BadRequest(ModelState);
                return Ok(user);
            } catch (Exception _)
            {
                return Unauthorized();
            }
       
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
        [HttpPost("createUse")]
        [ProducesResponseType(200 , Type = typeof(User))]
        [ProducesResponseType(400)]
        public IActionResult CreateUser([FromQuery] [FromBody]int  userRole, UserRegisterDto createuser)
        {
            if (_userRepo.UserExist(createuser.UserName) || _userRepo.UserEmail(createuser.UserEmail))
                return BadRequest("User already Exist Choose Another"); 
            var User = new User {
              
                //UserRole =createuser.UserRole,
                UserEmail =createuser.UserEmail,
                UserName = createuser.UserName,
                Password = BCrypt.Net.BCrypt.HashPassword(createuser.Password),
                Group = createuser.Group,
                Role = _role.Getrole(userRole),
                Joinddate = createuser.Joinddate,
                LoggedInTime= DateTime.Now, 
                LoggedOutTime= DateTime.Now,
            
            };
            var created = _userRepo.CreateUser(User);
            var userr = _userRepo.GetByEmail(User.UserEmail);
            return Ok(userr.id);
 

        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public IActionResult UserLogin(UserDto userlogin)
        {
            var user = _userRepo.GetByEmail(userlogin.UserEmail);
            if (user == null) return BadRequest(new { message="Invalid User" });
            if (!BCrypt.Net.BCrypt.Verify(userlogin.Password, user.Password)) return BadRequest(new { message = "Invalid Password" });
            var role = _context.Users.Where(a => a.UserEmail == userlogin.UserEmail).Include(a => a.Role).FirstOrDefault();
            var jwt = _jwtService.Generate(user.id, role: role.Role.UserRole,user.Group, user.UserName, user.UserEmail,user.Joinddate ,user.LoggedInTime, user.LoggedOutTime);
            Response.Cookies.Append("jwt", jwt, new CookieOptions
            {
                 HttpOnly = true
            });
            return Ok( new
            {
                token = jwt,
               
            } );
        }
        [HttpPost("verifypassword")]
        public IActionResult verifyPassword(UserDto password)
        {
            var user = _userRepo.GetByEmail(password.UserEmail);
            
            if (!BCrypt.Net.BCrypt.Verify(password.Password, user.Password)) return BadRequest(new { message = "Invalid Password" });
            return Ok("Correct");
        }
        [HttpPut("user/{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateUser(int id, [FromBody] UserRegisterDto Updateuser)
        {
    
         
                var User = new User
                {
                    id = Updateuser.id,
                    //UserRole = Updateuser.UserRole,
                    UserEmail = Updateuser.UserEmail,
                    UserName = Updateuser.UserName,
                    Password = BCrypt.Net.BCrypt.HashPassword(Updateuser.Password),
                    Group= Updateuser.Group,
                    Joinddate = Updateuser.Joinddate,
                    LoggedInTime = DateTime.Now,
                    LoggedOutTime = DateTime.Now,

                };


                var updateuser = _userRepo.UpdateUser(User);



                return Ok("Updated Succesfully");
        
          
        }
        [HttpPut("userpassword/{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateUserPassword(int id, [FromBody] PasswordDto Updateuser)
        {
         
            var User = new User
            {
                id = Updateuser.id,
                Password = BCrypt.Net.BCrypt.HashPassword(Updateuser.Password),
          

            };

            var passwords = _mapper.Map<User>(Updateuser);
            var updateuser = _userRepo.UpdateUser(passwords);



            return Ok("Updated Succesfully");


        }

        [HttpDelete("{id}/User")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteUser(int id)
        {
          
                if (!_userRepo.Userexist(id))
                    return NotFound();
                var deleteuser = _userRepo.GetUser(id);
                if (!_userRepo.DeleteUser(deleteuser))
                {
                    ModelState.AddModelError("", "somthing is wrong");
                    return StatusCode(500, ModelState);
                }

                return NoContent();
         
           

        }
        [HttpPost("logoutUser")]
        public IActionResult Logoutuser()
        {
            Response.Cookies.Delete("jwt");
            return Ok(new
            {
                message = " Logout Success"
            });


        }
    }
}
