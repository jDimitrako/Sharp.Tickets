using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Domain
{
    // TO-DO: Remove Identity dependency from Domain  
    // and seperate it in different project. ex. Identity-Server
    public class AppUser : IdentityUser
    {
        public string DisplayName { get; set; }
        public virtual ICollection<UserTicket> UserTickets { get; set; }
        public virtual ICollection<Photo> Photos { get; set; }
        public virtual ICollection<UserFollowing> Followings { get; set; }
        public virtual ICollection<UserFollowing> Followers { get; set; }

    }
}