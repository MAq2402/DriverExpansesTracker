using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DriverExpansesTracker.Services.Models.User
{
    public class UserForCreationDto
    {
        [Required(ErrorMessage = "Nie podano imienia")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Nie podano nazwiska")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Nie podano nazwy użytkownika")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Nie wprowadzono hasła"), DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Potwierdź hasło"), DataType(DataType.Password), Compare("Password",ErrorMessage ="Hasła nie są takie same")]
        public string ConfirmPassword { get; set; }
    }
}
