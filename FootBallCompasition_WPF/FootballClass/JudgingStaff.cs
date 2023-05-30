using System.ComponentModel.DataAnnotations.Schema;

namespace FootBallCompasition_WPF.FootballClass
{
    [Table("JudgingStaff")]
    public class JudgingStaff 
    {

        public int Id { get; set; }
        public int IdMatch { get; set; }
        public Match Match { get; set; }
        public int IdParticipant { get; set; }
        public Participant Participant { get; set; }
        public byte IdAmpluaRole { get; set; }

        public AmpluaRole AmpluaRole { get; set; }

        public JudgingStaff() { }

        public JudgingStaff(int id, int idMatch, int idParticipant, byte idAmpluaRole) 
        {
            this.Id = id;
            this.IdMatch = idMatch;
            this.IdParticipant = idParticipant; 
            this.IdAmpluaRole = idAmpluaRole; 
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

        public AmpluaRole IdAmpluaRole
        {
            get { return idAmpluaRole; }
            set { idAmpluaRole = value; }
        }
*/

        public override string ToString()
        {
            return $"Id: {Id}; idMatch: {IdMatch}; idParticipant: {IdParticipant}; idAmpluaRole: {IdAmpluaRole}.";
        }

    }
}
