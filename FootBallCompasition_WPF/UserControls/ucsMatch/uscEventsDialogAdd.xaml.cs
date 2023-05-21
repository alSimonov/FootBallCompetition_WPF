using FootBallCompasition_WPF.context;
using FootBallCompasition_WPF.FootballClass;
using HandyControl.Controls;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
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

namespace FootBallCompasition_WPF.UserControls.ucsMatch
{
    /// <summary>
    /// Логика взаимодействия для uscEventsDialogAdd.xaml
    /// </summary>
    public partial class uscEventsDialogAdd : UserControl
    {


        public MainDBContext? _db;

        uscEvents _uscEvents;

        FootballClass.Event _event;
        FootballClass.Match _match;


        int _idP;
        bool _addOrModify;
        int _idM;


        public uscEventsDialogAdd(int idP, bool addOrModify, int idM, uscEvents uscEvents)
        {
            InitializeComponent();

            tblTeamErr.Visibility = Visibility.Collapsed;
            tblPartErr.Visibility = Visibility.Collapsed;
            tblEventErr.Visibility = Visibility.Collapsed;
            tblTimeErr.Visibility = Visibility.Collapsed;


            dbConfiguration.ConfigureServices();
            _db = dbConfiguration.Services.GetService<MainDBContext>();

            _uscEvents = uscEvents;

            _idM = idM;

            _match = _db.Matches.Find(_idM);



            cbTeam.ItemsSource = _db.Teams.Where(x => x.Id == _match.IdTeam1 || x.Id == _match.IdTeam2).ToList();
            cbTeam.SelectedValuePath = "Id";
            cbTeam.DisplayMemberPath = "Name";


            cbTypeOfEvent.ItemsSource = _db.TypeOfEvents.ToList();
            cbTypeOfEvent.SelectedValuePath = "Id";
            cbTypeOfEvent.DisplayMemberPath = "Name";
            
            

            _idP = idP;
            _addOrModify = addOrModify;
            _idM = idM;


            if (!addOrModify)
            {
                _event = _db.Events.Find(idP);

                var teamCompositionnn = _db.TeamCompositions.Find(_event.IdTeamComposition);

                cbTeam.SelectedValue = teamCompositionnn.IdTeam;
                cbTeamComposition.SelectedValue = _event.IdTeamComposition;
                cbTypeOfEvent.SelectedValue = _event.IdTypeOfEvent;
                tbTime.Text = _event.Time;


            }


        }

        private void cbTeam_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            //var ll = _db.TeamCompositions.Where(x => x.Team == cbTeam.SelectedItem && x.ContractEnd > _match.Date && x.ContractStart < _match.Date)
            //    .Select(x => x.Participant)
            //    .ToList();

            var ll = _db.TeamCompositions.Where(x => x.Team == cbTeam.SelectedItem && x.ContractEnd > _match.Date && x.ContractStart < _match.Date)
                .Include(x => x.Participant)
                .ToList();


            ll.ForEach(x => x.SetFullNameAndNumPlayer());

            cbTeamComposition.ItemsSource = ll;
            cbTeamComposition.SelectedValuePath = "Id";
            cbTeamComposition.DisplayMemberPath = "FullNameAndNumPlayer";


        }

        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {

            tblTeamErr.Visibility = cbTeam.SelectedIndex == -1 ? Visibility.Visible : Visibility.Collapsed;
            tblPartErr.Visibility = cbTeamComposition.SelectedIndex == -1 ? Visibility.Visible : Visibility.Collapsed;
            tblEventErr.Visibility = cbTypeOfEvent.SelectedIndex == -1 ? Visibility.Visible : Visibility.Collapsed;
            tblTimeErr.Visibility = tbTime.Text == string.Empty ? Visibility.Visible : Visibility.Collapsed;

            if (tblTeamErr.Visibility == 0 || tblPartErr.Visibility == 0 || tblEventErr.Visibility == 0 || tblTimeErr.Visibility == 0)
                return;

            if (_addOrModify)
            {
                FootballClass.Event eventtt = new FootballClass.Event();

                eventtt.Match = _match;
                eventtt.TeamComposition = (TeamComposition)cbTeamComposition.SelectedItem;
                eventtt.TypeOfEvent = (TypeOfEvent)cbTypeOfEvent.SelectedItem;
                eventtt.Time = tbTime.Text;

                _db.Events.Add(eventtt);
                _db.SaveChanges();

                _uscEvents.loadDataGrid();
                Growl.Success("Событие успешно добавлено!");

            }
            else if (!_addOrModify)
            {
                _event.TeamComposition = (TeamComposition)cbTeamComposition.SelectedItem;
                _event.TypeOfEvent = (TypeOfEvent)cbTypeOfEvent.SelectedItem;
                _event.Time = tbTime.Text;

                _db.Entry(_event).State = EntityState.Modified;
                _db.SaveChanges();

                _uscEvents.loadDataGrid();
                Growl.Success("Событие успешно изменено!");

            }

        }
    }
}
