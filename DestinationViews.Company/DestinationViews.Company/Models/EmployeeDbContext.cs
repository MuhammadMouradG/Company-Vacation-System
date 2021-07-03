using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DestinationViews.Company.Models
{
    public class EmployeeDbContext : DbContext
    {
        public EmployeeDbContext() : base("DestinationViews.CompanyDbContext")
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmpVacationBalance> EmpVacationBalance { get; set; }
        public DbSet<EmpVacationType> EmpVacationTypes { get; set; }
        public DbSet<Request> Requests { get; set; }
    }
}