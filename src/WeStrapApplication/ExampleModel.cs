using System.ComponentModel.DataAnnotations;

namespace WeStrapApplication
{
    public class ExampleModel
    {
        [Required]
        [StringLength(10, ErrorMessage = "Name is too long.")]

        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }
    }
}
