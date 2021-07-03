using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DestinationViews.Company.Models
{
    public class Employee
    {
        /// <summary>
        ///  This class is a discribe for Employee.
        /// </summary>

        // This ID used to optmize the code in finding the Employee.
        [Key]
        public int ID { get; set; }

        public int UserID { get; set; }

        [Required(ErrorMessage = "Please, choose your Birthdate.")]
        [DataType(DataType.Date)]
        public DateTime Birthdate { get; set; }

        [Required]
        public string Gender { get; set; }


        // The following properties are to link between User Data and other data:
        [ForeignKey("UserID")]
        public User user { get; set; }

        public List<Request> Requests { get; set; }

        /*
        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
        */
    }

    public enum gender
    {
        Male,
        Female,
        Other,
    }
}