using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DestinationViews.Company.Models
{
    public class Request
    {
        // This ID used to optmize the code in finding the Employee.
        public int ID { get; set; }

        public int EmployeeID { get; set; }

        public int EmpVacationTypeID { get; set; }

        [Required(ErrorMessage = "Please, enter the number of days that you want.")]
        public int NumberOfDays { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        public bool? Accepted { get; set; }


        // The following properties are to link between Employee and EmpVacationType with Request:
        [ForeignKey("EmployeeID")]
        public Employee employee { get; set; }

        [ForeignKey("EmpVacationTypeID")]
        public EmpVacationType empVacationType { get; set; }
    }
}