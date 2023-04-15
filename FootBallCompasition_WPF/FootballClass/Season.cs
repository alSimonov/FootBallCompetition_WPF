using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootBallCompasition_WPF.FootballClass
{
    [Table("Season")]
    public class Season
    {

        public byte Id { get; set; }
        public string Name { get; set; }

        public Season() { }
        public Season(byte id, string name)
        {
            this.Id = id;
            this.Name = name;
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

*/


        public override string ToString()
        {
            return $"Id: {Id}; name: {Name}.";
        }


    }
}
