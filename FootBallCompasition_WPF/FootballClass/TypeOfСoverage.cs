using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootBallCompasition_WPF.FootballClass
{
    [Table("TypeOfCoverage")]
    public class TypeOfСoverage
    {

        public byte Id { get; set; }
        public string Name { get; set; }

        public TypeOfСoverage() { }
       
        /*public TypeOfСoverage(byte id, string name)
        {
            this.Id = id;
            this.Name = name;
        }*/

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
