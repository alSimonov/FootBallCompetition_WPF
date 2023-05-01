using FootBallCompasition_WPF.context;
using FootBallCompasition_WPF.FootballClass;
using HandyControl.Controls;
using HandyControl.Tools;
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

namespace FootBallCompasition_WPF.UserControls
{
    /// <summary>
    /// Логика взаимодействия для UsControlDialogTeamAdd.xaml
    /// </summary>
    public partial class UsControlDialogTeamAdd : UserControl
    {

        public MainDBContext? _db;

        Team _team;

        int _idP;
        bool _addOrModify;


        public UsControlDialogTeamAdd(int idP, bool addOrModify)
        {
            InitializeComponent();

            dbConfiguration.ConfigureServices();
            _db = dbConfiguration.Services.GetService<MainDBContext>();



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
            if (_addOrModify)
            {
                Team team = new Team();

                team.Name = tbTeamName.Text;
                team.City = (City) cbCity.SelectedItem;


       
                _db.Teams.Add(team);
                _db.SaveChanges();

                Growl.Success("Команда успешно добавлена!");

            }
            else if (!_addOrModify)
            {
                _team.Name = tbTeamName.Text;
                _team.City = (City)cbCity.SelectedItem;

                _db.Entry(_team).State = EntityState.Modified;

                _db.SaveChanges();

                Growl.Success("Команда успешно изменена!");

            }

        }
    }
}
