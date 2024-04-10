using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie_Theater_Model.Models
{

    public partial class Login {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int loginID { get; set; }
        public string username { get; set; } = null!;
        public string password { get; set; } = null!; 
        public string first_name { get; set;  }

        public string last_name { get; set;}

    } 
}
