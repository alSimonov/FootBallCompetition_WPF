using System.ComponentModel.DataAnnotations.Schema;

namespace FootBallCompasition_WPF.FootballClass
{
    [Table("TypeOfStadium")]
    public class TypeOfStadium
    {

        public byte Id { get; set; }
        public string Name { get; set; }

        public TypeOfStadium() { }
        public TypeOfStadium(byte id, string name)
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
