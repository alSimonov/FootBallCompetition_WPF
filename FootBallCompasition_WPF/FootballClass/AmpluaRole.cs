using System.ComponentModel.DataAnnotations.Schema;

namespace FootBallCompasition_WPF.FootballClass
{
    [Table("AmpluaRole")]
    public class AmpluaRole
    {
        public byte Id { get; set; } 
        public string Name { get; set; }

        public AmpluaRole() { }

        public AmpluaRole(byte id, string name)
        {
            this.Id = id;
            this.Name = name;
        }

        public override string ToString()
        {
            return $"Id: {Id}; name: {Name}.";
        }


    }
}
