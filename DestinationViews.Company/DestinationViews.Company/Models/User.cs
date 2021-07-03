using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DestinationViews.Company.Models
{
    public class User
    {
        public int ID { get; set; }

        // This full name act as a UserName; it must be unique for each employee in database.
        [Required(ErrorMessage = "Please, enter your full name; at least in quadrant form.")]
        public string Full_Name { get; set; }

        [EmailAddress(ErrorMessage = "Please, enter a valid E-mail.")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        public bool IsAdmin { get; set; }
    }
}