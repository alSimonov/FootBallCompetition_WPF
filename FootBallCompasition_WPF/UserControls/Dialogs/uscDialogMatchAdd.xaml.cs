using FootBallCompasition_WPF.context;
using FootBallCompasition_WPF.FootballClass;
using FootBallCompasition_WPF.Pages.pgsMatch;
using HandyControl.Controls;
using HandyControl.Tools;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace FootBallCompasition_WPF.UserControls
{
    /// <summary>
    /// Логика взаимодействия для uscDialogMatchAdd.xaml
    /// </summary>
    public partial class uscDialogMatchAdd : UserControl
    {
        public MainDBContext? _db;

        PageMatch _pageMatch;

        FootballClass.Match _match;

        int _idP;
        bool _addOrModify;


        public uscDialogMatchAdd(int idP, bool addOrModify, PageMatch pageMatch)
        {
            InitializeComponent();



            tblSeasonErr.Visibility = Visibility.Collapsed;
            tblTeam1Err.Visibility = Visibility.Collapsed;
            tblTeam2Err.Visibility = Visibility.Collapsed;
            tblDateOfMatchErr.Visibility = Visibility.Collapsed;
            tblStadiumErr.Visibility = Visibility.Collapsed;
            tblTypeOfMatchErr.Visibility = Visibility.Collapsed;


            //Установка языка для datePicker

            ConfigHelper.Instance.SetLang("ru");



            dbConfiguration.ConfigureServices();
            _db = dbConfiguration.Services.GetService<MainDBContext>();

            _pageMatch = pageMatch;

            _idP = idP;
            _addOrModify = addOrModify;


            if (_addOrModify)
                tglbtnActive.IsChecked = true;
            else tglbtnActive.IsChecked = false;

            loadComboboxData();

        }

        public void loadComboboxData()
        {

            cbSeason.ItemsSource = _db.Seasons.ToList();
            cbSeason.SelectedValuePath = "Id";
            cbSeason.DisplayMemberPath = "Name";

            if (tglbtnActive.IsChecked == true)
                cbTeam1.ItemsSource = _db.Teams.Where(x => x.Active == true).ToList();
            else
                cbTeam1.ItemsSource = _db.Teams.ToList();

            cbTeam1.SelectedValuePath = "Id";
            cbTeam1.DisplayMemberPath = "Name";


            // cbTeam2 заполняется при изменении выбора cbTeam2, метод внизу

            List<Stadium> ll;

            if (tglbtnActive.IsChecked == true)
                ll = _db.Stadiums.Where(x => x.Active == true).Include(x => x.City).ToList();
            else
                ll = _db.Stadiums.Include(x => x.City).ToList();

            ll.ForEach(x => x.SetStadiumAndCityName());

            cbStadium.ItemsSource = ll;
            cbStadium.SelectedValuePath = "Id";
            cbStadium.DisplayMemberPath = "StadiumAndCityName";


            cbTypeOfMatch.ItemsSource = _db.TypeOfMatches.ToList();
            cbTypeOfMatch.SelectedValuePath = "Id";
            cbTypeOfMatch.DisplayMemberPath = "Name";




            

            if (!_addOrModify)
            {
                _match = _db.Matches.Find(_idP);

                cbSeason.SelectedItem = _match.Season;
                cbTeam1.SelectedItem = _match.Team1;
                cbTeam2.SelectedItem = _match.Team2;

                dpDateOfMatch.SelectedDate = _match.Date;

                cbStadium.SelectedItem = _match.Stadium;
                cbTypeOfMatch.SelectedItem = _match.TypeOfMatch;

            }



        }

        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {

            tblSeasonErr.Visibility = cbSeason.SelectedIndex == -1 ? Visibility.Visible : Visibility.Collapsed;
            tblTeam1Err.Visibility = cbTeam1.SelectedIndex == -1 ? Visibility.Visible : Visibility.Collapsed;
            tblTeam2Err.Visibility = cbTeam2.SelectedIndex == -1 ? Visibility.Visible : Visibility.Collapsed;
            tblDateOfMatchErr.Visibility = !dpDateOfMatch.SelectedDate.HasValue ? Visibility.Visible : Visibility.Collapsed; ;
            tblStadiumErr.Visibility = cbStadium.SelectedIndex == -1 ? Visibility.Visible : Visibility.Collapsed;
            tblTypeOfMatchErr.Visibility = cbTypeOfMatch.SelectedIndex == -1 ? Visibility.Visible : Visibility.Collapsed;



            if (tblSeasonErr.Visibility == 0 || tblTeam1Err.Visibility == 0 || tblTeam2Err.Visibility == 0 || tblDateOfMatchErr.Visibility == 0 
                || tblStadiumErr.Visibility == 0 || tblTypeOfMatchErr.Visibility == 0)
                return;

            

            if (_db.Matches.Any(u => u.Date == (DateTime)dpDateOfMatch.SelectedDate && ( u.Team1 == (Team)cbTeam1.SelectedItem  || u.Team1 == (Team)cbTeam2.SelectedItem  
                 || u.Team2 == (Team)cbTeam1.SelectedItem || u.Team2 == (Team)cbTeam2.SelectedItem ) )
                && (_addOrModify || _match.Date != dpDateOfMatch.SelectedDate || _match.Team1 != (Team)cbTeam1.SelectedItem ||
                    _match.Team2 != (Team)cbTeam2.SelectedItem))
            {
                Growl.Warning("В этот день одна из команд уже играет!");
                return;
            }


            if (_addOrModify)
            {

                FootballClass.Match match = new FootballClass.Match();

                match.Season = (Season)cbSeason.SelectedItem;
                match.Team1 = (Team)cbTeam1.SelectedItem;
                match.Team2 = (Team)cbTeam2.SelectedItem;
                match.Date = (DateTime) dpDateOfMatch.SelectedDate;
                match.Stadium = (Stadium)cbStadium.SelectedItem;
                match.TypeOfMatch = (TypeOfMatch) cbTypeOfMatch.SelectedItem;


                _db.Matches.Add(match);

                _db.SaveChanges();

                _pageMatch.loadMatch();

                Growl.Success("Матч успешно добавлен!");

            }
            else if (!_addOrModify)
            {
                
                _match.Season = (Season)cbSeason.SelectedItem;
                _match.Team1 = (Team)cbTeam1.SelectedItem;
                _match.Team2 = (Team)cbTeam2.SelectedItem;
                _match.Date = (DateTime)dpDateOfMatch.SelectedDate;
                _match.Stadium = (Stadium)cbStadium.SelectedItem;
                _match.TypeOfMatch = (TypeOfMatch)cbTypeOfMatch.SelectedItem;



                _db.Entry(_match).State = EntityState.Modified;

                _db.SaveChanges();

                _pageMatch.loadMatch();

                Growl.Success("Матч успешно изменен!");

            }


            if (!_pageMatch.tempSeasons.Any(x => x == cbSeason.SelectedItem))
                _pageMatch.loadComboBox();

        }

        private void tglbtnActive_Click(object sender, RoutedEventArgs e)
        {
            loadComboboxData();
        }

        private void cbTeam1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (tglbtnActive.IsChecked == true)
                cbTeam2.ItemsSource = _db.Teams.Where(x => x.Active == true && x != cbTeam1.SelectedItem).ToList();
            else
                cbTeam2.ItemsSource = _db.Teams.Where(x => x != cbTeam1.SelectedItem ).ToList();


            cbTeam2.SelectedValuePath = "Id";
            cbTeam2.DisplayMemberPath = "Name";

        }
    }
}
