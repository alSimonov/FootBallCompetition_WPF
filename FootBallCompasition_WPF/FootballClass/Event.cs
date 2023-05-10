using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootBallCompasition_WPF.FootballClass
{

    [Table("Event")]
    public class Event 
    {
        public int Id { get; set; }

        public int IdMatch { get; set; }
        public Match Match { get; set; }
        public int IdTeamComposition { get; set; }
        public TeamComposition TeamComposition { get; set; }
        public byte IdTypeOfEvent { get; set; }
        public TypeOfEvent TypeOfEvent { get; set; }
        public string Time { get; set; }

        public Event() { }
        

        public override string ToString()
        {
            return $"Id: {Id}; idMatch: {IdMatch}; IdTeamComposition: {IdTeamComposition}; idTypeOfEvent: {IdTypeOfEvent}; time: {Time}.";
        }

    }
}
