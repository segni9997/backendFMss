using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;
using backend.Models;

namespace backend.Helper
{
    public class JwtService
    {private readonly IConfiguration _configuration;
        private string SecureKey = "jklsfh;skjfh89w735urhfg;iyr983204uojur0wr93ur'[www;aiofhwieurjg;wip5sdad5r56ar598375231025b;akjdbf3q/;bjafuA;ERBw";
        public JwtService(IConfiguration configuration)
        {
            _configuration = configuration;
            
        }
        public string Generate(int id, string role,string group, string username, string email , DateTime  joineddate, DateTime loggedin, DateTime loggedout)
        {
            var SKey = _configuration.GetValue<string>("JwtConfig:Key");
            var Keybytes = Encoding.ASCII.GetBytes(SKey);
            var tokenhandler = new JwtSecurityTokenHandler();
            var tokenDiscriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Role, role),
                    new Claim(ClaimTypes.GroupSid, group),
                    new Claim(ClaimTypes.NameIdentifier,id.ToString()),
                    new Claim(ClaimTypes.Surname, username),
                    new Claim(ClaimTypes.Email,email),     
                      new Claim(type:"joinedDate",value:joineddate.ToString()),
                      new Claim(type:"loggedin",value:loggedin.ToString()),
                      new Claim(type:"Loggedout",value:loggedout.ToString()),
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Keybytes),SecurityAlgorithms.HmacSha256Signature)


            };
            var token = tokenhandler.CreateToken(tokenDiscriptor);
            //var Credentails= new SigningCredentials(symmetricSecurityKey,SecurityAlgorithms.HmacSha256Signature);
            //var header = new JwtHeader(Credentails);
            //var payload= new JwtPayload(id.ToString(),role,null,null,DateTime.Today.AddDays(1)); 
            //var SecurityToken= new JwtSecurityToken(header,payload);

            return tokenhandler.WriteToken(token);
        }
        public JwtSecurityToken Verify(string jwt)
        {
            var tokenhandler = new JwtSecurityTokenHandler();
            var Key = Encoding.ASCII.GetBytes(SecureKey);
            tokenhandler.ValidateToken(jwt, new TokenValidationParameters
            {
                IssuerSigningKey = new SymmetricSecurityKey(Key),
                ValidateIssuerSigningKey = true,
                ValidateIssuer = false,
                ValidateAudience = false,
                

            }, out SecurityToken validatedToken);
            return (JwtSecurityToken)validatedToken;
        }
    }
}
