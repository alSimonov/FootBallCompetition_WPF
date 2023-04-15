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
        public int IdParticipant { get; set; }
        public Participant Participant { get; set; }
        public byte IdTypeOfEvent { get; set; }
        public TypeOfEvent TypeOfEvent { get; set; }
        public string Time { get; set; }

        public Event() { }
        public Event(int id, int idMatch, int idParticipant, byte idTypeOfEvent, string time)
        {
            this.Id = id;
            this.IdMatch = idMatch;
            this.IdParticipant = idParticipant;
            this.IdTypeOfEvent = idTypeOfEvent;
            this.Time = time;
        }

/*
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public Match IdMatch
        {
            get { return idMatch; }
            set { idMatch = value; }
        }

        public Participant IdParticipant
        {
            get { return idParticipant; }
            set
            {
                idParticipant = value;
            }
        }

        public TypeOfEvent IdTypeOfEvent
        {
            get { return idTypeOfEvent; }
            set
            {
                idTypeOfEvent = value;
            }
        }
        public string Time
        {
            get { return time; }
            set { time = value; }

        }
*/


        public override string ToString()
        {
            return $"Id: {Id}; idMatch: {IdMatch}; idParticipant: {IdParticipant}; idTypeOfEvent: {IdTypeOfEvent}; time: {Time}.";
        }

    }
}
