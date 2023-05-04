using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootBallCompasition_WPF.FootballClass
{
    [Table("Account")]
    public class Account
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public byte[] Password { get; set; }
        public string Email { get; set; }
        public int IdParticipant { get; set; }
        public Participant Participant { get; set; }
        public byte IdAccountRole { get; set; }
        public AccountRole AccountRole { get; set; }

        public Account() { }

        
    }
}
