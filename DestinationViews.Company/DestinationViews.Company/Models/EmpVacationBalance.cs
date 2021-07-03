using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DestinationViews.Company.Models
{
    public class EmpVacationBalance
    {
        [Key]
        public int ID { get; set; }

        public int EmployeeID { get; set; }

        public int EmpVacationTypeID { get; set; }

        [Required]
        public int NewBalance { get; set; }


        [ForeignKey("EmployeeID")]
        public Employee employee { get; set; }

        [ForeignKey("EmpVacationTypeID")]
        public EmpVacationType empVacationType { get; set; }
    }
}