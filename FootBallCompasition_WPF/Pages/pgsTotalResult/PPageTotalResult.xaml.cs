using FootBallCompasition_WPF.context;
using FootBallCompasition_WPF.FootballClass;
using FootBallCompasition_WPF.Short;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FootBallCompasition_WPF.Pages.pgsTotalResult
{
    /// <summary>
    /// Логика взаимодействия для PPageTotalResult.xaml
    /// </summary>
    public partial class PPageTotalResult : Page
    {

        public MainDBContext? _db;

        List<TotalResult> rankingList = new List<TotalResult>();
        List<TotalResultShort> rankingListShort = new List<TotalResultShort>();

        public List<Season> tempSeasons;
        byte? _idSeason;


        public PPageTotalResult()
        {
            InitializeComponent();

            dbConfiguration.ConfigureServices();
            _db = dbConfiguration.Services.GetService<MainDBContext>();


            cbSeason.SelectedValuePath = "Id";
            cbSeason.DisplayMemberPath = "Name";

            loadComboBox();


            loadMatch();
        }



        public void loadComboBox()
        {
            tempSeasons = _db.Seasons.Where(s => _db.Matches.Any(m => m.IdSeason == s.Id)).ToList();
            cbSeason.ItemsSource = tempSeasons;

        }


        public void loadMatch()
        {

            SqlParameter[] param = new SqlParameter[1];
            param[0] = new("@inpIdSeason", _idSeason);

            rankingList = _db.TotalResults.FromSqlRaw("GetTotalResultListForSeason @inpIdSeason", param).ToList();

            rankingListShort = rankingList.Select(s =>
               new TotalResultShort()
               {
                   imageIrl = null,
                   TeamName = s.TeamName,
                   CountMatches = s.CountMatches,
                   TotalScore = s.TotalScore,
               }).ToList();

            rankingListShort[0].imageIrl = "/Images/medal1.png";
            rankingListShort[1].imageIrl = "/Images/medal2.png";
            rankingListShort[2].imageIrl = "/Images/medal3.png";

            GridMatch.ItemsSource = rankingListShort;


            txtblListCount.Text = "Найдено записей: ";
            txtblListCount.Text += rankingList.Count().ToString();
        }


        private void cbSeason_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _idSeason = cbSeason.SelectedValue == null ? null : (cbSeason.SelectedItem as Season).Id;
            loadMatch();
        }

        private void GridMatch_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex() + 1).ToString();
        }
    }
}
