namespace CrystalBallpro.Migrations
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<CrystalBallpro.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(CrystalBallpro.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            context.Roles.AddOrUpdate(
                s => s.Name,
                    new IdentityRole { Name = "Admin" },
                    new IdentityRole { Name = "Employee" }
                );

            context.Weeks.AddOrUpdate(
                w => w.Day,
                    new Models.Week { Day = "Monday" },
                    new Models.Week { Day = "Tuesday" },
                    new Models.Week { Day = "Wednesday" },
                    new Models.Week { Day = "Thursday" },
                    new Models.Week { Day = "Friday" },
                    new Models.Week { Day = "Saturday" },
                    new Models.Week { Day = "Sunday" }
                );

            context.StartTimes.AddOrUpdate(
                s => s.Start,
                    new Models.StartTime { Start = "9:00 AM" },
                    new Models.StartTime { Start = "3:30 PM" }
                );

            context.EndTimes.AddOrUpdate(
                e => e.End,
                    new Models.EndTime { End = "3:30 PM" },
                    new Models.EndTime { End = "10:00 PM" }
                );

            context.SalesHistoricals.AddOrUpdate(
            d => d.Id,
                    new Models.SalesHistorical { NumberOfEvents = 0, NumberOfEmployees = 2, SalesIncreasePercent = 0 },
                    new Models.SalesHistorical { NumberOfEvents = 1, NumberOfEmployees = 3, SalesIncreasePercent = 8 },
                    new Models.SalesHistorical { NumberOfEvents = 2, NumberOfEmployees = 3, SalesIncreasePercent = 10 },
                    new Models.SalesHistorical { NumberOfEvents = 3, NumberOfEmployees = 4, SalesIncreasePercent = 15 },
                    new Models.SalesHistorical { NumberOfEvents = 4, NumberOfEmployees = 4, SalesIncreasePercent = 20 },
                    new Models.SalesHistorical { NumberOfEvents = 5, NumberOfEmployees = 5, SalesIncreasePercent = 30 });
        }
    }
}
