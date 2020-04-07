using Microsoft.AspNetCore.Identity;

namespace Domain
{
    // TO-DO: Remove Identity dependency from Domain  
    // and seperate it in different project. ex. Identity-Server
    public class AppUser : IdentityUser
    {
        public string DisplayName { get; set; }
    }
}