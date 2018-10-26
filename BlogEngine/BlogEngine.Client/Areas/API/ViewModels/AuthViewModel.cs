namespace BlogEngine.Client.Areas.API.ViewModels
{
    using System.ComponentModel.DataAnnotations;

    public class AuthViewModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}