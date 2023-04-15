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
    /// Логика взаимодействия для TeamPage.xaml
    /// </summary>
    public partial class TeamPage : Page
    {

        public MainDBContext? _db;

        List<TeamShort> teamList = new List<TeamShort>();


        public TeamPage()
        {
            InitializeComponent();
            dbConfiguration.ConfigureServices();
            _db = dbConfiguration.Services.GetService<MainDBContext>();
            loadTeam();

        }


        public void loadTeam()
        {

            teamList = _db.Teams.Select(s =>
                new TeamShort()
                {
                    //Id = s.Id,
                    TeamName = s.Name,
                    CityName = s.City.Name
                }).ToList();

            GridTeam.ItemsSource = teamList;


        }




    }
}