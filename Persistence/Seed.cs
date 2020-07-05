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
        public static async Task SeedData(DataContext context,
            UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var users = new List<AppUser>
                {
                    new AppUser
                    {
                        Id = "a",
                        DisplayName = "Bob",
                        UserName = "bob",
                        Email = "bob@test.com"
                    },
                    new AppUser
                    {
                        Id = "b",
                        DisplayName = "Jane",
                        UserName = "jane",
                        Email = "jane@test.com"
                    },
                    new AppUser
                    {
                        Id = "c",
                        DisplayName = "Tom",
                        UserName = "tom",
                        Email = "tom@test.com"
                    },
                };

                foreach (var user in users)
                {
                    await userManager.CreateAsync(user, "Pa$$w0rd");
                }
            }

            if (!context.Tickets.Any())
            {
                var tickets = new List<Ticket>
                {
                    new Ticket
                    {
                        Title = "Past Ticket 1",
                        DateFirst = DateTime.Now.AddMonths(-3),
                        DateModified = DateTime.Now.AddMonths(-2),
                        DateDeadline = DateTime.Now.AddMonths(2),
                        Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec eu ultricies orci. Aliquam cursus nulla lectus, id suscipit justo fringilla sed. Donec et aliquet sapien. Nunc et eleifend sem, sed dapibus lectus. Curabitur consectetur augue urna, et mattis felis congue in. Nullam non nisi lacus. Mauris id accumsan est. Mauris accumsan pulvinar mi non vulputate. Aliquam tincidunt tempor luctus. Quisque varius tellus quis augue sollicitudin hendrerit. Aliquam porta nibh nunc, ut volutpat tellus lacinia elementum. Pellentesque quis quam vel elit sollicitudin sodales. ",
                        Category = "Sales",
                        UserTickets = new List<UserTicket>
                        {
                            new UserTicket
                            {
                                AppUserId = "a",
                                IsHost = true,
                                DateJoined = DateTime.Now.AddMonths(-2)
                            }
                        }
                    },
                    new Ticket
                    {
                        Title = "Past Ticket 2",
                         DateFirst = DateTime.Now.AddMonths(-5),
                        DateModified = DateTime.Now.AddMonths(-3),
                        DateDeadline = DateTime.Now.AddMonths(2),
                        Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec eu ultricies orci. Aliquam cursus nulla lectus, id suscipit justo fringilla sed. Donec et aliquet sapien. Nunc et eleifend sem, sed dapibus lectus. Curabitur consectetur augue urna, et mattis felis congue in. Nullam non nisi lacus. Mauris id accumsan est. Mauris accumsan pulvinar mi non vulputate. Aliquam tincidunt tempor luctus. Quisque varius tellus quis augue sollicitudin hendrerit. Aliquam porta nibh nunc, ut volutpat tellus lacinia elementum. Pellentesque quis quam vel elit sollicitudin sodales. ",
                        Category = "Back Office",
                        UserTickets = new List<UserTicket>
                        {
                            new UserTicket
                            {
                                AppUserId = "b",
                                IsHost = true,
                                DateJoined = DateTime.Now.AddMonths(-1)
                            },
                            new UserTicket
                            {
                                AppUserId = "a",
                                IsHost = false,
                                DateJoined = DateTime.Now.AddMonths(-1)
                            },
                        }
                    },
                    new Ticket
                    {
                        Title = "Future Ticket 1",
                         DateFirst = DateTime.Now.AddMonths(-10),
                        DateModified = DateTime.Now.AddMonths(-6),
                        DateDeadline = DateTime.Now.AddMonths(2),
                        Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec eu ultricies orci. Aliquam cursus nulla lectus, id suscipit justo fringilla sed. Donec et aliquet sapien. Nunc et eleifend sem, sed dapibus lectus. Curabitur consectetur augue urna, et mattis felis congue in. Nullam non nisi lacus. Mauris id accumsan est. Mauris accumsan pulvinar mi non vulputate. Aliquam tincidunt tempor luctus. Quisque varius tellus quis augue sollicitudin hendrerit. Aliquam porta nibh nunc, ut volutpat tellus lacinia elementum. Pellentesque quis quam vel elit sollicitudin sodales. ",
                        Category = "Back Office",
                        UserTickets = new List<UserTicket>
                        {
                            new UserTicket
                            {
                                AppUserId = "b",
                                IsHost = true,
                                DateJoined = DateTime.Now.AddMonths(1)
                            },
                            new UserTicket
                            {
                                AppUserId = "a",
                                IsHost = false,
                                DateJoined = DateTime.Now.AddMonths(1)
                            },
                        }
                    },
                    new Ticket
                    {
                        Title = "Future Ticket 2",
                         DateFirst = DateTime.Now.AddMonths(-2),
                        DateModified = DateTime.Now.AddMonths(-1),
                        DateDeadline = DateTime.Now.AddMonths(2),
                        Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec eu ultricies orci. Aliquam cursus nulla lectus, id suscipit justo fringilla sed. Donec et aliquet sapien. Nunc et eleifend sem, sed dapibus lectus. Curabitur consectetur augue urna, et mattis felis congue in. Nullam non nisi lacus. Mauris id accumsan est. Mauris accumsan pulvinar mi non vulputate. Aliquam tincidunt tempor luctus. Quisque varius tellus quis augue sollicitudin hendrerit. Aliquam porta nibh nunc, ut volutpat tellus lacinia elementum. Pellentesque quis quam vel elit sollicitudin sodales. ",
                        Category = "Sales",
                        UserTickets = new List<UserTicket>
                        {
                            new UserTicket
                            {
                                AppUserId = "c",
                                IsHost = true,
                                DateJoined = DateTime.Now.AddMonths(2)
                            },
                            new UserTicket
                            {
                                AppUserId = "a",
                                IsHost = false,
                                DateJoined = DateTime.Now.AddMonths(2)
                            },
                        }
                    },
                    new Ticket
                    {
                        Title = "Future Ticket 3",
                         DateFirst = DateTime.Now.AddMonths(-20),
                        DateModified = DateTime.Now.AddMonths(-11),
                        DateDeadline = DateTime.Now.AddMonths(2),
                        Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec eu ultricies orci. Aliquam cursus nulla lectus, id suscipit justo fringilla sed. Donec et aliquet sapien. Nunc et eleifend sem, sed dapibus lectus. Curabitur consectetur augue urna, et mattis felis congue in. Nullam non nisi lacus. Mauris id accumsan est. Mauris accumsan pulvinar mi non vulputate. Aliquam tincidunt tempor luctus. Quisque varius tellus quis augue sollicitudin hendrerit. Aliquam porta nibh nunc, ut volutpat tellus lacinia elementum. Pellentesque quis quam vel elit sollicitudin sodales. ",
                        Category = "Quality Department",
                        UserTickets = new List<UserTicket>
                        {
                            new UserTicket
                            {
                                AppUserId = "b",
                                IsHost = true,
                                DateJoined = DateTime.Now.AddMonths(3)
                            },
                            new UserTicket
                            {
                                AppUserId = "c",
                                IsHost = false,
                                DateJoined = DateTime.Now.AddMonths(3)
                            },
                        }
                    },
                    new Ticket
                    {
                        Title = "Future Ticket 4",
                         DateFirst = DateTime.Now.AddMonths(-4),
                        DateModified = DateTime.Now.AddMonths(-1),
                        DateDeadline = DateTime.Now.AddMonths(2),
                        Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec eu ultricies orci. Aliquam cursus nulla lectus, id suscipit justo fringilla sed. Donec et aliquet sapien. Nunc et eleifend sem, sed dapibus lectus. Curabitur consectetur augue urna, et mattis felis congue in. Nullam non nisi lacus. Mauris id accumsan est. Mauris accumsan pulvinar mi non vulputate. Aliquam tincidunt tempor luctus. Quisque varius tellus quis augue sollicitudin hendrerit. Aliquam porta nibh nunc, ut volutpat tellus lacinia elementum. Pellentesque quis quam vel elit sollicitudin sodales. ",
                        Category = "Quality Department",
                        UserTickets = new List<UserTicket>
                        {
                            new UserTicket
                            {
                                AppUserId = "a",
                                IsHost = true,
                                DateJoined = DateTime.Now.AddMonths(4)
                            }
                        }
                    },
                    new Ticket
                    {
                        Title = "Future Ticket 5",
                         DateFirst = DateTime.Now.AddMonths(-5),
                        DateModified = DateTime.Now.AddMonths(-1),
                        DateDeadline = DateTime.Now.AddMonths(2),
                        Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec eu ultricies orci. Aliquam cursus nulla lectus, id suscipit justo fringilla sed. Donec et aliquet sapien. Nunc et eleifend sem, sed dapibus lectus. Curabitur consectetur augue urna, et mattis felis congue in. Nullam non nisi lacus. Mauris id accumsan est. Mauris accumsan pulvinar mi non vulputate. Aliquam tincidunt tempor luctus. Quisque varius tellus quis augue sollicitudin hendrerit. Aliquam porta nibh nunc, ut volutpat tellus lacinia elementum. Pellentesque quis quam vel elit sollicitudin sodales. ",
                        Category = "Quality Department",
                        UserTickets = new List<UserTicket>
                        {
                            new UserTicket
                            {
                                AppUserId = "c",
                                IsHost = true,
                                DateJoined = DateTime.Now.AddMonths(5)
                            },
                            new UserTicket
                            {
                                AppUserId = "b",
                                IsHost = false,
                                DateJoined = DateTime.Now.AddMonths(5)
                            },
                        }
                    },
                    new Ticket
                    {
                        Title = "Future Ticket 6",
                         DateFirst = DateTime.Now.AddMonths(-6),
                        DateModified = DateTime.Now.AddMonths(-1),
                        DateDeadline = DateTime.Now.AddMonths(2),
                        Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec eu ultricies orci. Aliquam cursus nulla lectus, id suscipit justo fringilla sed. Donec et aliquet sapien. Nunc et eleifend sem, sed dapibus lectus. Curabitur consectetur augue urna, et mattis felis congue in. Nullam non nisi lacus. Mauris id accumsan est. Mauris accumsan pulvinar mi non vulputate. Aliquam tincidunt tempor luctus. Quisque varius tellus quis augue sollicitudin hendrerit. Aliquam porta nibh nunc, ut volutpat tellus lacinia elementum. Pellentesque quis quam vel elit sollicitudin sodales. ",
                        Category = "Quality Department",
                        UserTickets = new List<UserTicket>
                        {
                            new UserTicket
                            {
                                AppUserId = "a",
                                IsHost = true,
                                DateJoined = DateTime.Now.AddMonths(6)
                            },
                            new UserTicket
                            {
                                AppUserId = "b",
                                IsHost = false,
                                DateJoined = DateTime.Now.AddMonths(6)
                            },
                        }
                    },
                    new Ticket
                    {
                        Title = "Future Ticket 7",
                         DateFirst = DateTime.Now.AddMonths(-1),
                        DateModified = DateTime.Now.AddMonths(-1),
                        DateDeadline = DateTime.Now.AddMonths(2),
                        Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec eu ultricies orci. Aliquam cursus nulla lectus, id suscipit justo fringilla sed. Donec et aliquet sapien. Nunc et eleifend sem, sed dapibus lectus. Curabitur consectetur augue urna, et mattis felis congue in. Nullam non nisi lacus. Mauris id accumsan est. Mauris accumsan pulvinar mi non vulputate. Aliquam tincidunt tempor luctus. Quisque varius tellus quis augue sollicitudin hendrerit. Aliquam porta nibh nunc, ut volutpat tellus lacinia elementum. Pellentesque quis quam vel elit sollicitudin sodales. ",
                        Category = "Sales",
                        UserTickets = new List<UserTicket>
                        {
                            new UserTicket
                            {
                                AppUserId = "a",
                                IsHost = true,
                                DateJoined = DateTime.Now.AddMonths(7)
                            },
                            new UserTicket
                            {
                                AppUserId = "c",
                                IsHost = false,
                                DateJoined = DateTime.Now.AddMonths(7)
                            },
                        }
                    },
                    new Ticket
                    {
                        Title = "Future Ticket 8",
                         DateFirst = DateTime.Now.AddMonths(-5),
                        DateModified = DateTime.Now.AddMonths(-1),
                        DateDeadline = DateTime.Now.AddMonths(2),
                        Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec eu ultricies orci. Aliquam cursus nulla lectus, id suscipit justo fringilla sed. Donec et aliquet sapien. Nunc et eleifend sem, sed dapibus lectus. Curabitur consectetur augue urna, et mattis felis congue in. Nullam non nisi lacus. Mauris id accumsan est. Mauris accumsan pulvinar mi non vulputate. Aliquam tincidunt tempor luctus. Quisque varius tellus quis augue sollicitudin hendrerit. Aliquam porta nibh nunc, ut volutpat tellus lacinia elementum. Pellentesque quis quam vel elit sollicitudin sodales. ",
                        Category = "Quality Department",
                        UserTickets = new List<UserTicket>
                        {
                            new UserTicket
                            {
                                AppUserId = "b",
                                IsHost = true,
                                DateJoined = DateTime.Now.AddMonths(8)
                            },
                            new UserTicket
                            {
                                AppUserId = "a",
                                IsHost = false,
                                DateJoined = DateTime.Now.AddMonths(8)
                            },
                        }
                    }
                };

                await context.Tickets.AddRangeAsync(tickets);
                await context.SaveChangesAsync();
            }
        }
    }
}