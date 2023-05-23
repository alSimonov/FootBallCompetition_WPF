using FootBallCompasition_WPF.context;
using FootBallCompasition_WPF.FootballClass;
using FootBallCompasition_WPF.Short;
using FootBallCompasition_WPF.UserControls;
using FootBallCompasition_WPF.UserControls.fUscTeamComposition;
using FootBallCompasition_WPF.UserControls.ucsMatch;
using FootBallCompasition_WPF.Windows;
using HandyControl.Controls;
using HandyControl.Tools.Extension;
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

namespace FootBallCompasition_WPF.Pages.pgsTeam
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

            teamList = _db.Teams.Where(x => x.Active == tglbtnActive.IsChecked)
                .Select(s =>
                new TeamShort()
                {
                    Id = s.Id,
                    TeamName = s.Name,
                    CityName = s.City.Name,
                    Active = s.Active,
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

               
                teamList = _db.Teams.Where(x => x.Active == tglbtnActive.IsChecked && x.Name.StartsWith(filtrby)).
                    Select(s => new TeamShort()
                    {
                        Id = s.Id,
                        TeamName = s.Name,
                        CityName = s.City.Name,
                        Active = s.Active,
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
            Dialog.Show(new UsControlDialogTeamAdd(0, true, this));
        }


        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            //int id = (GridTeam.SelectedItem as TeamShort).Id;

            //Team team = _db.Teams.Find(id);

            //_db.Teams.Remove(team);
            //_db.SaveChanges();

            //loadTeam();

            //Growl.Success("Команда успешно удалена!");

            var dialog = new windowConfirmation("Изменение активности");
            if (dialog.ShowDialog() == true)
            {
                int id = (GridTeam.SelectedItem as TeamShort).Id;
                Team team = _db.Teams.Find(id);

                team.Active = team.Active ? false : true;

                _db.Entry(team).State = EntityState.Modified;
                _db.SaveChanges();

                loadTeam();
                Growl.Warning("Состояние активности команды изменено!");

            }

        }

        private void btnModify_Click(object sender, RoutedEventArgs e)
        {
            int id = (GridTeam.SelectedItem as TeamShort).Id;


            Dialog.Show(new UsControlDialogTeamAdd(id, false, this));


        }

        private void tbFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            loadTeam(tbFilter.Text.Trim());
        }

        private void btnTabBtnList_Click(object sender, RoutedEventArgs e)
        {
            loadTeam();
        }

        private void btnTabBtnTeamComposition_Click(object sender, RoutedEventArgs e)
        {

            if (GridTeam.SelectedItem == null)
            {
                Growl.Error("Команда не была выбрана!");
            }
            else
            {

                Dialog.Show(new uscTeamCompositionForTeam((TeamShort)GridTeam.SelectedItem));

            }


        }

        private void tglbtnActive_Click(object sender, RoutedEventArgs e)
        {
            loadTeam();
        }
    }
}
