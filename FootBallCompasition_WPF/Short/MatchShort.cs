using FootBallCompasition_WPF.FootballClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootBallCompasition_WPF.Short
{
    class MatchShort
    {

        public int Id { get; set; }
        public string Season { get; set; }
        public string Team1Name { get; set; }
        public string Team2Name { get; set; }
        public String Date { get; set; }
        public string StadiumAndCityName { get; set; }
        //public string CityName { get; set; }
        public string TypeOfMatch { get; set; }

        public MatchShort() { }


    }
}
