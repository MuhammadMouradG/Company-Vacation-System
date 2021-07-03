using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DestinationViews.Company.Models
{
    public class EmpVacationType
    {
        // This ID used to optmize the code in finding related Employee.
        [Key]
        public int ID { get; set; }

        public string TypeName { get; set; }

        public int DefaultBalance { get; set; }
    }
}