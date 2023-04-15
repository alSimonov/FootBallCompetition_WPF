using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

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
        public string ContractStart { get; set; }
        public string ContractEnd { get; set; }
        public int PlayerNumber { get; set; }
        public byte IdAmpluaRole { get; set; }
        public AmpluaRole AmpluaRole { get; set; }
        public TeamComposition() { }

        public TeamComposition(int id, int idTeam, int idParticipant, string contractStart, string contractEnd, int playerNumber, byte idAmpluaRole)
        {
            this.Id = id;
            this.IdTeam = idTeam;
            this.IdParticipant = idParticipant;
            this.ContractStart = contractStart;
            this.ContractEnd = contractEnd;
            this.PlayerNumber = playerNumber;
            this.IdAmpluaRole = idAmpluaRole;

        }

/*
        public int Id
        {
            get { return id; }
            set { id = value; }

        }

        public Team IdTeam
        {
            get { return idTeam; }
            set { idTeam = value; }
        }

        public Participant IdParticipant
        {
            get { return idParticipant; }
            set
            {
                idParticipant = value;
            }
        }

        public string ContractStart
        {
            get { return contractStart; }
            set
            {
                contractStart = value;
            }
        }



        public string ContractEnd
        {
            get { return contractEnd; }
            set
            {
                contractEnd = value;
            }
        }

        public int PlayerNumber
        {
            get { return playerNumber; }
            set
            {
                playerNumber = value;
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
            return $"Id: {Id};  Team: {IdTeam}; Participant: {IdParticipant}; ContractStart: {ContractStart}; ContractEnd: {ContractEnd}; PlayerNumber: {PlayerNumber}; AmpluaRole: {IdAmpluaRole}.";
        }




    }
}
