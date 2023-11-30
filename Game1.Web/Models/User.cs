using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Game1.Web.Models
{
    public class User : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        public string? Password { get; set; }

        public string Salt { get; set; }

        public string? ReferralLink { get; set; }

        public string? Role { get; set; }

        public int Level { get; set; }

        public bool IsApprove { get; set; }

        public string? AgentId { get; set; }
    }
}
