using FootBallCompasition_WPF.context;
using FootBallCompasition_WPF.FootballClass;
using FootBallCompasition_WPF.Short;
using FootBallCompasition_WPF.UserControls;
using HandyControl.Controls;
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

namespace FootBallCompasition_WPF.Pages
{
    /// <summary>
    /// Логика взаимодействия для MatchPage.xaml
    /// </summary>
    public partial class PageMatch : Page
    {

        public MainDBContext? _db;

        List<MatchShort> matchList = new List<MatchShort>();

        public PageMatch()
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
                    Id = s.Id,
                    Season = s.Season.Name,
                    Team1Name = s.Team1.Name,
                    Team2Name = s.Team2.Name,
                    Date = s.Date.ToString("D"),
                    StadiumAndCityName = $"{s.Stadium.Name} ({s.Stadium.City.Name})",

                    TypeOfMatch = s.TypeOfMatch.Name
                }).ToList();

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
                matchList = _db.Matches.Where(x => x.Team1.Name.Contains(filtrby) || x.Team2.Name.Contains(filtrby)).
                Select(s => new MatchShort()
                {
                    Id = s.Id,
                    Season = s.Season.Name,
                    Team1Name = s.Team1.Name,
                    Team2Name = s.Team2.Name,
                    Date = s.Date.ToString("D"),
                    StadiumAndCityName = $"{s.Stadium.Name} ({s.Stadium.City.Name})",
                    //StadiumName = s.Stadium.Name,
                    //CityName = s.Stadium.City.Name,
                    TypeOfMatch = s.TypeOfMatch.Name
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
            Dialog.Show(new uscDialogMatchAdd(0, true));

            loadMatch();


        }

        private void btnModify_Click(object sender, RoutedEventArgs e)
        {
            int id = (GridMatch.SelectedItem as MatchShort).Id;


            Dialog.Show(new uscDialogMatchAdd(id, false));

            loadMatch();

        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            int id = (GridMatch.SelectedItem as MatchShort).Id;

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

        private void btnTabBtnList_Click(object sender, RoutedEventArgs e)
        {
            loadMatch();
        }
    }
}
