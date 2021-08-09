using System.ComponentModel.DataAnnotations;

namespace FrankLiu.Models
{
    public class Credential
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
