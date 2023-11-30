using System.ComponentModel.DataAnnotations;

namespace Gateway
{
    public class ApiConfiguration
    {
        [Required]
        public string Host { get; set; }
    }
}
