using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FrankLiu.Models
{
    public class Credential
    {
        [Required]    
        [DisplayName("User Name")]
        [MinLength(5, ErrorMessage = "Mai baga")]
        public string UserName { get; set; }

        [Required]
        [DisplayName("Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DisplayName("Remember Me")]
        public bool RememberMe { get; set; }

    }
}
