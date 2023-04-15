using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FootBallCompasition_WPF.FootballClass
{

    [Table("Stadium")]
    public class Stadium
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int IdCity { get; set; }
        public City City { get; set; }
        public int Capacity { get; set; }
        public byte IdTypeOfСoverage { get; set; }
        public TypeOfСoverage TypeOfСoverage { get; set; }
        public byte IdTypeOfStadium { get; set; }
        public TypeOfStadium TypeOfStadium { get; set; }

        public Stadium() { }
        public Stadium(int id, string name, int idCity, int capacity, byte idTypeOfСoverage, byte idTypeOfStadium)
        {
            this.Id = id;
            this.Name = name;
            this.IdCity = idCity;
            this.Capacity = capacity;
            this.IdTypeOfСoverage = idTypeOfСoverage;
            this.IdTypeOfStadium = idTypeOfStadium;
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

                public int Capacity
                {
                    get { return capacity; }
                    set { capacity = value; }

                }

                public Сoverage IdСoverage
                {
                    get { return idСoverage; }
                    set { idСoverage = value; }

                }

                public TypeOfStadium IdTypeOfStadium
                {
                    get { return idTypeOfStadium; }
                    set { idTypeOfStadium = value; }
                }
        */


        public string GetNameCityCapacity()
        {
            return $"{Name} {IdCity} {Capacity}";

        }

        public override string ToString()
        {
            return $"Id: {Id}; name: {Name}; idCity: {IdCity}; capacity: {Capacity}; idСoverage: {IdTypeOfСoverage}; idTypeOfStadium: {IdTypeOfStadium}.";
        }

    }
}
