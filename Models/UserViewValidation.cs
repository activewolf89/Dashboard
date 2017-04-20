using System.ComponentModel.DataAnnotations;

namespace Dashboard.Models
{
    public class UserViewValidation 
    {
        [Required]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Name can only contain letters")]
        [MinLength(2)]
        public string FirstName {get;set;}
        [Required]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Name can only contain letters")]
        [MinLength(2)]
        public string LastName {get;set;}
        
        [Required]
        [EmailAddress]
        public string Email {get;set;}
        [Required]
        [DataType(DataType.Password)]
        public string Password {get;set;}
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password and confirmation must match")]
        public string ConfirmationPassword{get;set;}

    }
}