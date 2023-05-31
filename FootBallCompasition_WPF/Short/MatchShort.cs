using System;

namespace FootBallCompasition_WPF.Short
{
    public class MatchShort
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
