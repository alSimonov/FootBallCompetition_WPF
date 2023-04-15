﻿using FootBallCompasition_WPF.context;
using FootBallCompasition_WPF.Short;
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

namespace FootBallCompasition_WPF.Pages
{
    /// <summary>
    /// Логика взаимодействия для MatchPage.xaml
    /// </summary>
    public partial class MatchPage : Page
    {

        public MainDBContext? _db;

        List<MatchShort> matchList = new List<MatchShort>();

        public MatchPage()
        {
            InitializeComponent();

            dbConfiguration.ConfigureServices();
            _db = dbConfiguration.Services.GetService<MainDBContext>();

            loadMatch();

        }


        private void loadMatch()
        {

            matchList = _db.Matches.Select(s =>
                new MatchShort()
                {
                    //Id = s.Id,
                    Season = s.Season.Name,
                    Team1Name = s.Team1.Name,
                    Team2Name = s.Team2.Name,
                    Date = s.Date,
                    StadiumName = s.Stadium.Name,
                    CityName = s.Stadium.City.Name,
                    TypeOfMatch = s.TypeOfMatch.Name
                }).ToList();

            GridMatch.ItemsSource = matchList;


        }



    }
}