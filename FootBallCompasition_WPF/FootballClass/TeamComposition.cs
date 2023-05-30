using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace FootBallCompasition_WPF.FootballClass
{
    [Table("TeamComposition")]
    public class TeamComposition
    {

        public int Id { get; set; }
        public int IdTeam { get; set; }
        public Team Team { get; set; }
        public int IdParticipant { get; set; }
        public Participant Participant { get; set; }
        public DateTime ContractStart { get; set; }
        public DateTime ContractEnd { get; set; }
        public byte PlayerNumber { get; set; }
        public byte IdAmpluaRole { get; set; }
        public AmpluaRole AmpluaRole { get; set; }
        public TeamComposition() { }





        public string FullNameAndNumPlayer { get; set; } = string.Empty;


        public void SetFullNameAndNumPlayer()
        {
            FullNameAndNumPlayer = $"{Participant.Surname} {Participant.Name} {Participant.Patronymic} {PlayerNumber}";
        }




        public override string ToString()
        {
            return $"Id: {Id};  Team: {IdTeam}; Participant: {IdParticipant}; ContractStart: {ContractStart}; ContractEnd: {ContractEnd}; PlayerNumber: {PlayerNumber}; AmpluaRole: {IdAmpluaRole}.";
        }



    }
}
