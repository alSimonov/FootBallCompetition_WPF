using FootBallCompasition_WPF.context;
using FootBallCompasition_WPF.FootballClass;
using FootBallCompasition_WPF.Short;
using FootBallCompasition_WPF.UserControls;
using FootBallCompasition_WPF.UserControls.ucsMatch;
using FootBallCompasition_WPF.Windows;
using HandyControl.Controls;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;


namespace FootBallCompasition_WPF.Pages.pgsMatch
{
    /// <summary>
    /// Логика взаимодействия для MatchPage.xaml
    /// </summary>
    public partial class PageMatch : Page
    {

        public MainDBContext? _db;

        List<GetMatchListModelShort> matchList = new List<GetMatchListModelShort>();

        byte? _idSeason;

        //TODO потом убрать закоменченный не нужный код

        public PageMatch()
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
            cbSeason.ItemsSource = _db.Seasons.Where(s => _db.Matches.Any(m => m.IdSeason == s.Id)).ToList();
            
        }


        public void loadMatch()
        {

            //loadComboBox();

            List<GetMatchListModel> tempMatchList;

            if (_idSeason == null)
            {
                tempMatchList = _db.GetMatchListModels.FromSqlRaw("GetMatchList").ToList();
            }
            else
            {

                SqlParameter[] param = new SqlParameter[1];
                param[0] = new("@inpIdSeason", _idSeason);

                tempMatchList = _db.GetMatchListModels.FromSqlRaw("GetMatchListForSeason @inpIdSeason", param).ToList();

            }


            matchList = tempMatchList.Select(s =>
                new GetMatchListModelShort()
                {
                    IdMatch0 = s.IdMatch0,
                    SeasonName0 = s.SeasonName0,
                    Team1AndTeam2Name = s.TeamName1 +" - "+ s.TeamName2,
                    DateMatch0 = s.DateMatch0.ToString("D"),
                    StadiumAndCity0 = s.StadiumAndCity0,
                    TypeOfMatch0 = s.TypeOfMatch0,
                     
                    Score0 = ""+ (s.Score1 == null ? "0" : s.Score1.ToString()) + " | " + (s.Score2 == null ? "0" : s.Score2.ToString()),

                }).ToList();


            GridMatch.ItemsSource = matchList.Take(10).ToList();


            pagGrid.MaxPageCount = (int)Math.Ceiling(matchList.Count / 10.0);


            txtblListCount.Text = "Найдено записей: ";
            txtblListCount.Text += matchList.Count().ToString();


        }

        private void loadMatch(string filtrby)
        {

            if (filtrby == "")
            {
                loadMatch();
            }
            else
            {


                var tempMatchList = _db.GetMatchListModels.FromSqlRaw("GetMatchList").ToList();

                matchList = tempMatchList.Where(x => x.TeamName1.StartsWith(filtrby) || x.TeamName2.StartsWith(filtrby))
                    .Select(s =>
                    new GetMatchListModelShort()
                    {
                        IdMatch0 = s.IdMatch0,
                        SeasonName0 = s.SeasonName0,
                        Team1AndTeam2Name = s.TeamName1 + " - " + s.TeamName2,
                        DateMatch0 = s.DateMatch0.ToString("D"),
                        StadiumAndCity0 = s.StadiumAndCity0,
                        TypeOfMatch0 = s.TypeOfMatch0,
                        Score0 = "" + (s.Score1 == null ? "0" : s.Score1.ToString()) + " | " + (s.Score2 == null ? "0" : s.Score2.ToString()),
                    }).ToList();


                GridMatch.ItemsSource = matchList.Take(10).ToList();

                pagGrid.MaxPageCount = (int)Math.Ceiling(matchList.Count / 10.0);

                txtblListCount.Text = "Найдено записей: ";
                txtblListCount.Text += matchList.Count().ToString();
            }
        }


        private void page_PageUpdated(object sender, HandyControl.Data.FunctionEventArgs<int> e)
        {
            GridMatch.ItemsSource = matchList.Skip((e.Info - 1) * 10).Take(10).ToList();
        }


        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            Dialog.Show(new uscDialogMatchAdd(0, true, this));
        }


        private void btnModify_Click(object sender, RoutedEventArgs e)
        {
            int id = (GridMatch.SelectedItem as GetMatchListModelShort).IdMatch0;

            Dialog.Show(new uscDialogMatchAdd(id, false, this));
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {

            //System.Windows.Media.Effects.BlurEffect objBlur = new System.Windows.Media.Effects.BlurEffect();
            //objBlur.Radius = 4;
            //this.Effect = objBlur;


            var dialog = new windowConfirmation();
            if (dialog.ShowDialog() == true)
            {
                int id = (GridMatch.SelectedItem as GetMatchListModelShort).IdMatch0;
                Match match = _db.Matches.Find(id);

                _db.Matches.Remove(match);
                _db.SaveChanges();

                loadMatch();

                Growl.Success("Матч успешно удален!");

            }

            //this.Effect = null;


        }

        private void tbFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            loadMatch(tbFilter.Text.Trim());
        }


        private void btnTabBtnReferee_Click(object sender, RoutedEventArgs e)
        {

            if (GridMatch.SelectedItem == null)
            {
                Growl.Error("Матч не был выбран!");
            }
            else
            {
                Dialog.Show(new ucsReferee((GetMatchListModelShort)GridMatch.SelectedItem));
            }

        }

        private void btnTabBtnEvent_Click(object sender, RoutedEventArgs e)
        {
            if (GridMatch.SelectedItem == null)
            {
                Growl.Error("Матч не был выбран!");
            }
            else
            {
                Dialog.Show(new uscEvents((GetMatchListModelShort)GridMatch.SelectedItem, this));
            }
        }

        private void cbSeason_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _idSeason = cbSeason.SelectedValue == null ? null : (cbSeason.SelectedItem as Season).Id;
            loadMatch();
        }

    }
}
