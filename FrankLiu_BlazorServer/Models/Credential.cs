using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FrankLiu_BlazorServer.Models
{
    public class Credential
    {
        [Required]
        [DisplayName("User Name")]
        public string? Username { get; set; }

        [Required]
        [DisplayName("Password")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

    }
}
