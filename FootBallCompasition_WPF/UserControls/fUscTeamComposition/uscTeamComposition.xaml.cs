using FootBallCompasition_WPF.context;
using FootBallCompasition_WPF.FootballClass;
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

namespace FootBallCompasition_WPF.UserControls.fUscTeamComposition
{
    /// <summary>
    /// Логика взаимодействия для uscTeamComposition.xaml
    /// </summary>
    public partial class uscTeamComposition : UserControl
    {
        private MainDBContext? _db;

        List<TeamCompositionShort> dataGridList = new List<TeamCompositionShort>();

        int _idP;



        public uscTeamComposition(ParticipantShort partShort)
        {
            InitializeComponent();

            dbConfiguration.ConfigureServices();
            _db = dbConfiguration.Services.GetService<MainDBContext>();

            _idP = partShort.Id;

            tblPartInfo.Text = $"{partShort.FIO} | {partShort.Telephone}";

            loadDataGrid();


        }



        private void loadDataGrid()
        {

            dataGridList = _db.TeamCompositions.Where(x => x.IdParticipant == _idP)
            .Select(s => new TeamCompositionShort()
            {
                Id = s.Id,
                TeamName = s.Team.Name,
                //FIO = $"{s.Participant.Surname} {s.Participant.Name} {s.Participant.Patronymic}",
                ContractStart = s.ContractStart.ToString("D"),
                ContractEnd = s.ContractEnd.ToString("D"),
                PlayerNumber = s.PlayerNumber,
                AmpluaRoleName = s.AmpluaRole.Name,
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

                _db.TeamCompositions.Where(x => x.IdParticipant == _idP && x.Team.Name.StartsWith(filtrby))
                    .Select(s => new TeamCompositionShort()
                    {
                        Id = s.Id,
                        TeamName = s.Team.Name,
                        //FIO = $"{s.Participant.Surname} {s.Participant.Name} {s.Participant.Patronymic}",
                        ContractStart = s.ContractStart.ToString("D"),
                        ContractEnd = s.ContractEnd.ToString("D"),
                        PlayerNumber = s.PlayerNumber,
                        AmpluaRoleName = s.AmpluaRole.Name,
                    }).ToList();


                GridReferee.ItemsSource = dataGridList.Take(6).ToList();

                pagGrid.MaxPageCount = (int)Math.Ceiling(dataGridList.Count / 6.0);

                txtblListCount.Text = "Найдено записей: ";
                txtblListCount.Text += dataGridList.Count().ToString();

            }

        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            Dialog.Show(new uscTeamCompositionDialogAdd(0, true, _idP));

            loadDataGrid();
        }

        private void btnModify_Click(object sender, RoutedEventArgs e)
        {
            int id = (GridReferee.SelectedItem as TeamCompositionShort).Id;

            Dialog.Show(new uscTeamCompositionDialogAdd(id, false, _idP));

            loadDataGrid();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            int id = (GridReferee.SelectedItem as TeamCompositionShort).Id;

            TeamComposition teamComposition = _db.TeamCompositions.Find(id);

            _db.TeamCompositions.Remove(teamComposition);
            _db.SaveChanges();

            loadDataGrid();

            Growl.Success("Контракт успешно удален!");

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

    }
}
