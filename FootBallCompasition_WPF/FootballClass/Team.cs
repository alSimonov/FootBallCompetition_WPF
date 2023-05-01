using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootBallCompasition_WPF.FootballClass
{
    [Table("Team")]
    public class Team 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int IdCity { get; set; }
        public City City { get; set; }

        public Team() { }
        public Team(int id, string name, int idCity)
        {
            this.Id = id;
            this.Name = name;
            this.IdCity = idCity;
        }



        public override string ToString()
        {
            return $"Id: {Id};  Name: {Name}; City: {IdCity}.";
        }

    }
}
