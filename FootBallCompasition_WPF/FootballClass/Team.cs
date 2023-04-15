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
/*
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public City IdCity
        {
            get { return idCity; }
            set { idCity = value; }
        }
*/


        public override string ToString()
        {
            return $"Id: {Id};  Name: {Name}; City: {IdCity}.";
        }

    }
}
