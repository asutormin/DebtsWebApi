using System.ComponentModel.DataAnnotations;

namespace DebtsWebApi.Entities
{
    public class AuthInfo
    {
        [Required]
        public string Login { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
