using System.ComponentModel.DataAnnotations.Schema;

namespace FootBallCompasition_WPF.FootballClass
{
    [Table("City")]
    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public City() { }

        public City(int id, string name)
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
