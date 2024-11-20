using System.ComponentModel;

namespace backend.Dto
{
    public class UserRegisterDto
    {
        public int id { get; set; }
        public string UserEmail { get; set; }
        public string UserName { get; set; }

        public string Password { get; set; }
        [DefaultValue("None")]
        public string Group { get; set; } = "None";

        public DateTime Joinddate { get; set; }
        public DateTime LoggedInTime { get; set; }
        public DateTime LoggedOutTime { get; set; }
    }
}
