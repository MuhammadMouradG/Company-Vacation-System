using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DestinationViews.Company.Models
{
    public class ViewModel
    {
        public Employee employee { get; set; }
        public User user { get; set; }
        public Request request { get; set; }
        public EmpVacationBalance empVacationBalance { get; set; }
        public EmpVacationType empVacationType { get; set; }
        public int NoOfRequests { get; set; }


        public List<User> Users { get; set; }
        public List<Employee> Employees { get; set; }
        public List<Request> Requests { get; set; }
        public List<EmpVacationBalance> EmpVacationBalances { get; set; }
        public List<EmpVacationType> EmpVacationTypes { get; set; }
        public List<int> NumbersOfRequsts { get; set; }
    }
}