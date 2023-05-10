using FootBallCompasition_WPF.context;
using FootBallCompasition_WPF.FootballClass;
using HandyControl.Controls;
using HandyControl.Tools;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Логика взаимодействия для uscDialogMatchAdd.xaml
    /// </summary>
    public partial class uscDialogMatchAdd : UserControl
    {       
        public MainDBContext? _db;

        FootballClass.Match _match;

        int _idP;
        bool _addOrModify;


        public uscDialogMatchAdd(int idP, bool addOrModify)
        {
            InitializeComponent();

            //Установка языка для datePicker

            ConfigHelper.Instance.SetLang("ru");



            dbConfiguration.ConfigureServices();
            _db = dbConfiguration.Services.GetService<MainDBContext>();


            cbSeason.ItemsSource = _db.Seasons.ToList();
            cbSeason.SelectedValuePath = "Id";
            cbSeason.DisplayMemberPath = "Name";
 

            cbTeam1.ItemsSource = _db.Teams.ToList();
            cbTeam1.SelectedValuePath = "Id";
            cbTeam1.DisplayMemberPath = "Name";
            
            cbTeam2.ItemsSource = _db.Teams.ToList();
            cbTeam2.SelectedValuePath = "Id";
            cbTeam2.DisplayMemberPath = "Name";



            
            var ll = _db.Stadiums.Include(x => x.City).ToList();

            ll.ForEach(x => x.SetStadiumAndCityName());

            cbStadium.ItemsSource = ll;
            cbStadium.SelectedValuePath = "Id";
            cbStadium.DisplayMemberPath = "StadiumAndCityName";

            
            cbTypeOfMatch.ItemsSource = _db.TypeOfMatches.ToList();
            cbTypeOfMatch.SelectedValuePath = "Id";
            cbTypeOfMatch.DisplayMemberPath = "Name";

            


            _idP = idP;
            _addOrModify = addOrModify;



            if (!addOrModify)
            {
                _match = _db.Matches.Find(idP);

                cbSeason.SelectedItem = _match.Season;
                cbTeam1.SelectedItem = _match.Team1;
                cbTeam2.SelectedItem = _match.Team2;

                dpDateOfMatch.SelectedDate = _match.Date;

                cbStadium.SelectedItem =  _match.Stadium;
                cbTypeOfMatch.SelectedItem = _match.TypeOfMatch;

            }



        }

        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {

            if (_addOrModify)
            {

                FootballClass.Match match = new FootballClass.Match();

                match.Season = (Season)cbSeason.SelectedItem;
                match.Team1 = (Team)cbTeam1.SelectedItem;
                match.Team2 = (Team)cbTeam2.SelectedItem;

                DateTime? dateTime = dpDateOfMatch.SelectedDate;
                if (dateTime.HasValue)
                {
                    match.Date = (DateTime)dateTime;

                }
                else
                {

                }

                match.Stadium = (Stadium)cbStadium.SelectedItem;
                match.TypeOfMatch = (TypeOfMatch) cbTypeOfMatch.SelectedItem;


                _db.Matches.Add(match);

                _db.SaveChanges();

                Growl.Success("Матч успешно добавлен!");



            }
            else if (!_addOrModify)
            {
                
                _match.Season = (Season)cbSeason.SelectedItem;
                _match.Team1 = (Team)cbTeam1.SelectedItem;
                _match.Team2 = (Team)cbTeam2.SelectedItem;



                DateTime? dateTime = dpDateOfMatch.SelectedDate;
                if (dateTime.HasValue)
                {
                    _match.Date = (DateTime)dateTime;

                }
                else
                {
                }


                _match.Stadium = (Stadium)cbStadium.SelectedItem;
                _match.TypeOfMatch = (TypeOfMatch)cbTypeOfMatch.SelectedItem;



                _db.Entry(_match).State = EntityState.Modified;

                _db.SaveChanges();

                Growl.Success("Матч успешно изменен!");



            }




        }




    }
}
