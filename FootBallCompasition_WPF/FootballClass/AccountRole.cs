using System.ComponentModel.DataAnnotations.Schema;

namespace FootBallCompasition_WPF.FootballClass
{
    [Table("AccountRole")]
    public class AccountRole
    {
        public byte Id { get; set; }
        public string Name { get; set; }
    }
}
