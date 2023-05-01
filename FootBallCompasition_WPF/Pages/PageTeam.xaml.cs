using FootBallCompasition_WPF.context;
using FootBallCompasition_WPF.FootballClass;
using FootBallCompasition_WPF.Short;
using FootBallCompasition_WPF.UserControls;
using HandyControl.Controls;
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
    public partial class PageTeam : Page
    {

        public MainDBContext? _db;

        List<TeamShort> teamList = new List<TeamShort>();


        public PageTeam()
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
                    Id = s.Id,
                    TeamName = s.Name,
                    CityName = s.City.Name
                }).ToList();

            //GridTeam.ItemsSource = teamList;


            GridTeam.ItemsSource = teamList.Take(10).ToList();

            pagGrid.MaxPageCount = (int)Math.Ceiling(teamList.Count / 10.0);

            txtblListCount.Text = "Найдено записей: ";
            txtblListCount.Text += teamList.Count().ToString();



        }

        public void loadTeam(string filtrby)
        {

            if (filtrby == "")
            {
                loadTeam();

            }
            else
            {

               
                teamList = _db.Teams.Where(x => x.Name.Contains(filtrby)).
                    Select(s => new TeamShort()
                    {
                        Id = s.Id,
                        TeamName = s.Name,
                        CityName = s.City.Name
                    }).ToList();

                GridTeam.ItemsSource = teamList.Take(10).ToList();

                pagGrid.MaxPageCount = (int)Math.Ceiling(teamList.Count / 10.0);

                txtblListCount.Text = "Найдено записей: ";
                txtblListCount.Text += teamList.Count().ToString();



            }



        }


        private void page_PageUpdated(object sender, HandyControl.Data.FunctionEventArgs<int> e)
        {
            GridTeam.ItemsSource = teamList.Skip((e.Info - 1) * 10).Take(10).ToList();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {


            Dialog.Show(new UsControlDialogTeamAdd(0, true));

            loadTeam();


        }




        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            int id = (GridTeam.SelectedItem as TeamShort).Id;

            Team team = _db.Teams.Find(id);

            _db.Teams.Remove(team);
            _db.SaveChanges();

            loadTeam();

            Growl.Success("Команда успешно удалена!");


        }

        private void btnModify_Click(object sender, RoutedEventArgs e)
        {
            int id = (GridTeam.SelectedItem as TeamShort).Id;


            Dialog.Show(new UsControlDialogTeamAdd(id, false));

            loadTeam();



        }

        private void tbFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            loadTeam(tbFilter.Text.Trim());
        }

        private void btnTabBtnList_Click(object sender, RoutedEventArgs e)
        {
            loadTeam();
        }
    }
}
