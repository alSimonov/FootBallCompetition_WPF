using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootBallCompasition_WPF.FootballClass
{
    [Table("AccountRole")]
    public class AccountRole
    {
        public byte Id { get; set; }
        public string Name { get; set; }
    }
}
