using FootBallCompasition_WPF.context;
using FootBallCompasition_WPF.Pages.pgsMatch;
using FootBallCompasition_WPF.Short;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace FootBallCompasition_WPF.Pages
{
    /// <summary>
    /// Логика взаимодействия для PageStatistic.xaml
    /// </summary>
    public partial class PageStatistic : Page
    {

        public record class PartRole(int id, string last, string first, DateTime date, string NameRole);
        private MainDBContext? _db;

        List<StadiumShort> stadiumList = new List<StadiumShort>();



        public PageStatistic()
        {
            InitializeComponent();

            dbConfiguration.ConfigureServices();
            _db = dbConfiguration.Services.GetService<MainDBContext>();



            frameMatch.Navigate(new PageMatchList());

        }



        private void btnTabBtnList_Click(object sender, RoutedEventArgs e)
        {
            //loadStadium();

            frameMatch.Navigate(new PageMatchList());

        }




    }
}
