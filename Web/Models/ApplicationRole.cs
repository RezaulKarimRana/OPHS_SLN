using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Models
{
    public class ApplicationRole : IdentityRole
    {
        [Column(TypeName = "nvarchar(250)")]
        public string Description { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
