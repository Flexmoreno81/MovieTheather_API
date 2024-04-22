using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Movie_Theater_Model.Models
{
    public partial class UserLogins: IdentityUser
    {
        [Column(TypeName = "VARCHAR")]
        [StringLength(50)]
        [Required]
        public string FullName { set; get; }
    }
}
