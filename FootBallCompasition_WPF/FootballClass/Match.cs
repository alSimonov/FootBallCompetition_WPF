using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FootBallCompasition_WPF.FootballClass
{
    [Table("Match")]
    public class Match 
    {
        public int Id { get; set; }
        public byte IdSeason { get; set; }
        public Season Season { get; set; }
        public int IdTeam1 { get; set; }
        public Team Team1 { get; set; }
        public int IdTeam2 { get; set; }
        public Team Team2 { get; set; }
        public DateTime Date { get; set; }
        public int IdStadium { get; set; }
        public Stadium Stadium { get; set; }
        public byte IdTypeOfMatch { get; set; }
        public TypeOfMatch TypeOfMatch { get; set; }

        public Match() { }


        public Match(int id, byte idSeason, int idTeam1, int idTeam2, DateTime date, int idStadium, byte idTypeOfMatch)
        {
            this.Id = id;
            this.IdSeason = idSeason;
            this.IdTeam1 = idTeam1;
            this.IdTeam2 = idTeam2;
            this.Date = date;
            this.IdStadium = idStadium;
            this.IdTypeOfMatch = idTypeOfMatch;

        }



        public override string ToString()
        {
            return $"Id: {Id}; Season: {IdSeason}, Team: {IdTeam1}, Team: {IdTeam2}, DateTime: {Date}; idStadium: {IdStadium}, idTypeOfMatch: {IdTypeOfMatch}.";
        }



    }
}
