using System;
using System.Collections.Generic;
using System.Linq;
using Domain;

namespace Persistence
{
    public class Seed
    {
        public static void SeedData(DataContext context)
        {
            if(!context.Tickets.Any())
            {
                var tickets = new List<Ticket>
                {
                    new Ticket
                    {
                        Title = "Past Ticket 1",
                        Description = "Ticket 1, Ticket 1, Ticket 1,",
                        Category = "Orders",
                        DateFirst = DateTime.Now.AddMonths(-1),
                        DateModified = DateTime.Now.AddMonths(-1),
                        DateDeadline = DateTime.Now.AddMonths(-1)
                    },
                    new Ticket
                    {
                        Title = "Past Ticket 2",
                        Description = "Ticket 2, Ticket 2, Ticket 2,",
                        Category = "Orders",
                        DateFirst = DateTime.Now.AddMonths(-2),
                        DateModified = DateTime.Now.AddMonths(-2),
                        DateDeadline = DateTime.Now.AddMonths(-2)
                    },
                    new Ticket
                    {
                        Title = "Past Ticket 4",
                        Description = "Ticket 4, Ticket 4, Ticket 4,",
                        Category = "Orders",
                        DateFirst = DateTime.Now.AddMonths(-2),
                        DateModified = DateTime.Now.AddMonths(-2),
                        DateDeadline = DateTime.Now.AddMonths(-2)
                    },
                    new Ticket
                    {
                        Title = "Past Ticket 5",
                        Description = "Ticket 1, Ticket 1, Ticket 1,",
                        Category = "Orders",
                        DateFirst = DateTime.Now.AddMonths(-2),
                        DateModified = DateTime.Now.AddMonths(-2),
                        DateDeadline = DateTime.Now.AddMonths(-2)
                    },
                    new Ticket
                    {
                        Title = "Past Ticket 6",
                        Description = "Ticket 6, Ticket 6, Ticket 6,",
                        Category = "Orders",
                        DateFirst = DateTime.Now.AddMonths(-2),
                        DateModified = DateTime.Now.AddMonths(-2),
                        DateDeadline = DateTime.Now.AddMonths(-2)
                    },
                    new Ticket
                    {
                        Title = "Past Ticket 7",
                        Description = "Ticket 7, Ticket 7, Ticket 7,",
                        Category = "Orders",
                        DateFirst = DateTime.Now.AddMonths(-2),
                        DateModified = DateTime.Now.AddMonths(-2),
                        DateDeadline = DateTime.Now.AddMonths(-2)
                    },
                };

                context.Tickets.AddRange(tickets);
                context.SaveChanges();
            }
        }
    }
}