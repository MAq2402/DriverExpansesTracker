using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DriverExpansesTracker.Services.Models.User
{
    public class LoginDto
    {
        [Required(ErrorMessage = "Nie podano nazwy użytkownika")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Nie podano hasła")]
        public string Password { get; set; }
    }
}
