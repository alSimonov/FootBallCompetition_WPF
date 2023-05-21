﻿ using FootBallCompasition_WPF.context;
using FootBallCompasition_WPF.FootballClass;
using FootBallCompasition_WPF.Short;
using FootBallCompasition_WPF.UserControls;
using FootBallCompasition_WPF.UserControls.ucsMatch;
using HandyControl.Controls;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FootBallCompasition_WPF.Pages.pgsMatch
{
    /// <summary>
    /// Логика взаимодействия для MatchPage.xaml
    /// </summary>
    public partial class PageMatch : Page
    {

        public MainDBContext? _db;

        List<GetMatchListModelShort> matchList = new List<GetMatchListModelShort>();

        //byte? _idSeason;

        //TODO потом убрать закоменченный не нужный код

        public PageMatch()
        {
            InitializeComponent();

            dbConfiguration.ConfigureServices();
            _db = dbConfiguration.Services.GetService<MainDBContext>();

            loadMatch();


        }

        public void loadMatch()
        {

            var tempMatchList = _db.GetMatchListModels.FromSqlRaw("GetMatchList").ToList();

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


            //matchList = _db.Matches.Select(s =>
            //    new MatchShort()
            //    {
            //        Id = s.Id,
            //        Season = s.Season.Name,
            //        Team1Name = s.Team1.Name,
            //        Team2Name = s.Team2.Name,
            //        Date = s.Date.ToString("D"),
            //        StadiumAndCityName = $"{s.Stadium.Name} ({s.Stadium.City.Name})",

            //        TypeOfMatch = s.TypeOfMatch.Name
            //    }).ToList();

            //GridMatch.ItemsSource = matchList;

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
            int id = (GridMatch.SelectedItem as GetMatchListModelShort).IdMatch0;
            Match match = _db.Matches.Find(id);

            _db.Matches.Remove(match);
            _db.SaveChanges();

            loadMatch();

            Growl.Success("Матч успешно удален!");
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

        //private void cbSeason_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    _idSeason = cbSeason.SelectedValue == null ? null : (cbSeason.SelectedItem as Season).Id;
        //    loadMatch();
        //}
    }
}
