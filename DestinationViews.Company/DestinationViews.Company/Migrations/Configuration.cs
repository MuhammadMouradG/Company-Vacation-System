namespace DestinationViews.Company.Migrations
{
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DestinationViews.Company.Models.EmployeeDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DestinationViews.Company.Models.EmployeeDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            List<EmpVacationType> types = new List<EmpVacationType>();
            types.Add(new EmpVacationType()
            {
                ID = 1,
                TypeName = "Casual",
            });
            types.Add(new EmpVacationType()
            {
                ID = 2,
                TypeName = "Schedule",
            });


            foreach (EmpVacationType type in types)
            {
                context.EmpVacationTypes.AddOrUpdate(type);
            }


            //add admin user
            User admin = new User();
            admin.ID = 1;
            admin.Full_Name = "Muhammed Samir Saleh Mourad";
            admin.Email = "admin@ceo.admin.dv.eg";
            admin.Password = "123567890";
            admin.IsAdmin = true;

            context.Users.AddOrUpdate(admin);
            context.SaveChanges();

        }
    }
}
