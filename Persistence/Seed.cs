using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Microsoft.AspNetCore.Identity;

namespace Persistence
{
    public class Seed
    {
        public static async Task SeedData(DataContext context, UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var users = new List<AppUser>
                {
                    new AppUser
                    {
                        DisplayName = "Bob",
                        UserName = "bob",
                        Email = "bob@test.com"
                    },
                    new AppUser
                    {
                        DisplayName = "James",
                        UserName = "james",
                        Email = "James@test.com"
                    },
                    new AppUser
                    {
                        DisplayName = "Vaso",
                        UserName = "vaso",
                        Email = "vaso@test.com"
                    }
                };
                foreach (var user in users)
                {
                    await userManager.CreateAsync(user, "Pa$$w0rd");
                }
            }

            if (!context.Tickets.Any())
            {
                var activities = new List<Ticket>
                {
                    new Ticket
                    {
                        Title = "Past Ticket 1",
                        Description = "Ticket 2 months ago",
                        Category = "culture",
                        
                    },
                    new Ticket
                    {
                        Title = "Past Ticket 2",
                        Description = "Ticket 1 month ago",
                        Category = "culture",
                        DateFirst = DateTime.Now.AddMonths(-2),
                        DateModified = DateTime.Now.AddMonths(-2),
                        DateDeadline = DateTime.Now.AddMonths(-2)
                    },
                    new Ticket
                    {
                        Title = "Future Ticket 1",
                        Description = "Ticket 1 month in future",
                        Category = "culture",
                        DateFirst = DateTime.Now.AddMonths(-2),
                        DateModified = DateTime.Now.AddMonths(-2),
                        DateDeadline = DateTime.Now.AddMonths(-2)
                    },
                    new Ticket
                    {
                        Title = "Future Ticket 2",
                        Description = "Ticket 2 months in future",
                        Category = "music",
                        DateFirst = DateTime.Now.AddMonths(-2),
                        DateModified = DateTime.Now.AddMonths(-2),
                        DateDeadline = DateTime.Now.AddMonths(-2)
                    },
                    new Ticket
                    {
                        Title = "Future Ticket 3",
                        Description = "Ticket 3 months in future",
                        Category = "drinks",
                        DateFirst = DateTime.Now.AddMonths(-2),
                        DateModified = DateTime.Now.AddMonths(-2),
                        DateDeadline = DateTime.Now.AddMonths(-2)
                    },
                    new Ticket
                    {
                        Title = "Future Ticket 4",
                        Description = "Ticket 4 months in future",
                        Category = "drinks",
                        DateFirst = DateTime.Now.AddMonths(-2),
                        DateModified = DateTime.Now.AddMonths(-2),
                        DateDeadline = DateTime.Now.AddMonths(-2)
                    },
                    new Ticket
                    {
                        Title = "Future Ticket 5",
                        Description = "Ticket 5 months in future",
                        Category = "drinks",
                        DateFirst = DateTime.Now.AddMonths(-2),
                        DateModified = DateTime.Now.AddMonths(-2),
                        DateDeadline = DateTime.Now.AddMonths(-2)
                    },
                    new Ticket
                    {
                        Title = "Future Ticket 6",
                        Description = "Ticket 6 months in future",
                        Category = "music",
                        DateFirst = DateTime.Now.AddMonths(-2),
                        DateModified = DateTime.Now.AddMonths(-2),
                        DateDeadline = DateTime.Now.AddMonths(-2)
                    },
                    new Ticket
                    {
                        Title = "Future Ticket 7",
                        Description = "Ticket 2 months ago",
                        Category = "travel",
                        DateFirst = DateTime.Now.AddMonths(-2),
                        DateModified = DateTime.Now.AddMonths(-2),
                        DateDeadline = DateTime.Now.AddMonths(-2)
                    },
                    new Ticket
                    {
                        Title = "Future Ticket 8",
                        Description = "Ticket 8 months in future",
                        Category = "film",
                        DateFirst = DateTime.Now.AddMonths(-2),
                        DateModified = DateTime.Now.AddMonths(-2),
                        DateDeadline = DateTime.Now.AddMonths(-2)
                    }
                };

                context.Tickets.AddRange(activities);
                context.SaveChanges();
            }
        }
    }
}