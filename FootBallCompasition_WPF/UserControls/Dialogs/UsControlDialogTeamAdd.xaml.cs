using FootBallCompasition_WPF.context;
using FootBallCompasition_WPF.FootballClass;
using FootBallCompasition_WPF.Pages.pgsTeam;
using HandyControl.Controls;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace FootBallCompasition_WPF.UserControls
{
    /// <summary>
    /// Логика взаимодействия для UsControlDialogTeamAdd.xaml
    /// </summary>
    public partial class UsControlDialogTeamAdd : UserControl
    {

        public MainDBContext? _db;

        PageTeam _pageTeam;

        Team _team;

        int _idP;
        bool _addOrModify;


        public UsControlDialogTeamAdd(int idP, bool addOrModify, PageTeam pageTeam)
        {
            InitializeComponent();

            tblTeamNameErr.Visibility = Visibility.Collapsed;
            tblCityErr.Visibility = Visibility.Collapsed;


            dbConfiguration.ConfigureServices();
            _db = dbConfiguration.Services.GetService<MainDBContext>();


            _pageTeam = pageTeam;


            cbCity.ItemsSource = _db.Cities.ToList();
            cbCity.SelectedValuePath = "Id";
            cbCity.DisplayMemberPath = "Name";



            _idP = idP;
            _addOrModify = addOrModify;

            if (!addOrModify)
            {
                _team = _db.Teams.Find(idP);

                tbTeamName.Text = _team.Name;
                cbCity.SelectedItem = _team.City;
            }

        }


        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {

            tblTeamNameErr.Visibility = tbTeamName.Text == string.Empty ? Visibility.Visible : Visibility.Collapsed;
            tblCityErr.Visibility = cbCity.SelectedIndex == -1 ? Visibility.Visible : Visibility.Collapsed;

            if (tblTeamNameErr.Visibility == 0 || tblCityErr.Visibility == 0)
                return;

            

            if (_addOrModify)
            {
                Team team = new Team();

                team.Name = tbTeamName.Text;
                team.City = (City) cbCity.SelectedItem;
                team.Active = true;

       
                _db.Teams.Add(team);
                _db.SaveChanges();
                _pageTeam.loadTeam();

                Growl.Success("Команда успешно добавлена!");

            }
            else if (!_addOrModify)
            {
                _team.Name = tbTeamName.Text;
                _team.City = (City)cbCity.SelectedItem;

                _db.Entry(_team).State = EntityState.Modified;
                _db.SaveChanges();
                _pageTeam.loadTeam();

                Growl.Success("Команда успешно изменена!");

            }

        }
    }
}
