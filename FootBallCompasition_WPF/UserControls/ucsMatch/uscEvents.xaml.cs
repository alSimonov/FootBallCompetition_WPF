using FootBallCompasition_WPF.context;
using FootBallCompasition_WPF.FootballClass;
using FootBallCompasition_WPF.Pages.pgsMatch;
using FootBallCompasition_WPF.Short;
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

namespace FootBallCompasition_WPF.UserControls.ucsMatch
{
    /// <summary>
    /// Логика взаимодействия для uscEvents.xaml
    /// </summary>
    public partial class uscEvents : UserControl
    {


        private MainDBContext? _db;

        PageMatch _pageMatch;

        List<EventShort> dataGridList = new List<EventShort>();

        int _idM;





        public uscEvents(GetMatchListModelShort getMatchListModelShort, PageMatch pageMatch)
        {
            InitializeComponent();

            


            dbConfiguration.ConfigureServices();
            _db = dbConfiguration.Services.GetService<MainDBContext>();

            _pageMatch = pageMatch;

            _idM = getMatchListModelShort.IdMatch0;
            tblMatchInfo.Text = $"{getMatchListModelShort.Team1AndTeam2Name} | {getMatchListModelShort.DateMatch0}";

            loadDataGrid();

        }

        public void loadDataGrid()
        {

            dataGridList = _db.Events.Where(x => x.IdMatch == _idM)
            .Select(s => new EventShort()
            {
                Id = s.Id,
                TeamName = s.TeamComposition.Team.Name,
                FIO = $"{s.TeamComposition.Participant.Surname} {s.TeamComposition.Participant.Name} {s.TeamComposition.Participant.Patronymic}",
                PlayerNumber = s.TeamComposition.PlayerNumber,
                TypeOfEventName = s.TypeOfEvent.Name,
                Time = s.Time,
            }).ToList();


            GridReferee.ItemsSource = dataGridList.Take(6).ToList();

            pagGrid.MaxPageCount = (int)Math.Ceiling(dataGridList.Count / 6.0);

            txtblListCount.Text = "Найдено записей: ";
            txtblListCount.Text += dataGridList.Count().ToString();

        }

        private void loadDataGrid(string filtrby)
        {

            if (filtrby == "")
            {
                loadDataGrid();
            }
            else
            {
                dataGridList = _db.Events.Where(x => x.IdMatch == _idM)
                    .Select(s => new EventShort()
                    {
                        Id = s.Id,
                        TeamName = s.TeamComposition.Team.Name,
                        FIO = $"{s.TeamComposition.Participant.Surname} {s.TeamComposition.Participant.Name} {s.TeamComposition.Participant.Patronymic}",
                        PlayerNumber = s.TeamComposition.PlayerNumber,
                        TypeOfEventName = s.TypeOfEvent.Name,
                        Time = s.Time,
                    }).ToList();


                GridReferee.ItemsSource = dataGridList.Take(6).ToList();

                pagGrid.MaxPageCount = (int)Math.Ceiling(dataGridList.Count / 6.0);

                txtblListCount.Text = "Найдено записей: ";
                txtblListCount.Text += dataGridList.Count().ToString();

            }


        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            Dialog.Show(new uscEventsDialogAdd(0, true, _idM, this));
        }

        private void btnModify_Click(object sender, RoutedEventArgs e)
        {
            int id = (GridReferee.SelectedItem as EventShort).Id;

            Dialog.Show(new uscEventsDialogAdd(id, false, _idM, this));
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            int id = (GridReferee.SelectedItem as EventShort).Id;

            Event eventtt = _db.Events.Find(id);

            _db.Events.Remove(eventtt);
            _db.SaveChanges();

            loadDataGrid();

            Growl.Success("Событие успешно удалено!");

        }

        private void tbFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            loadDataGrid(tbFilter.Text.Trim());
        }

        private void btnTabBtnList_Click(object sender, RoutedEventArgs e)
        {
            loadDataGrid();
        }

        private void page_PageUpdated(object sender, HandyControl.Data.FunctionEventArgs<int> e)
        {
            GridReferee.ItemsSource = dataGridList.Skip((e.Info - 1) * 6).Take(6).ToList();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            _pageMatch.loadMatch();
        }
    }
}
