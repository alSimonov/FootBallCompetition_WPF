using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootBallCompasition_WPF.FootballClass
{
    public class GetMatchListModel
    {
        public int IdMatch0 { get; set; }
        public string SeasonName0 { get; set; }
        public string TeamName1 { get; set; }
        public string TeamName2 { get; set; }
        public DateTime DateMatch0 { get; set; }
        public string StadiumAndCity0 { get; set; }
        public string TypeOfMatch0 { get; set; }
        public int? Score1 { get; set; }
        public int? Score2 { get; set; }


    }
}
