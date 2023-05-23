using FootBallCompasition_WPF.context;
using FootBallCompasition_WPF.FootballClass;
using FootBallCompasition_WPF.Short;
using FootBallCompasition_WPF.Windows;
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
    /// Логика взаимодействия для uscTeamCompositionForTeam.xaml
    /// </summary>
    public partial class uscTeamCompositionForTeam : UserControl
    {
        private MainDBContext? _db;



        List<TeamCompositionShort> dataGridList = new List<TeamCompositionShort>();

        int _idP;


        public uscTeamCompositionForTeam(TeamShort teamShort)
        {
            InitializeComponent();

            dbConfiguration.ConfigureServices();
            _db = dbConfiguration.Services.GetService<MainDBContext>();

            _idP = teamShort.Id;

            tblPartInfo.Text = $"{teamShort.TeamName} | {teamShort.CityName}";

            loadDataGrid();



        }





        public void loadDataGrid()
        {

            dataGridList = _db.TeamCompositions.Where(x => x.IdTeam == _idP)
            .Select(s => new TeamCompositionShort()
            {
                Id = s.Id,
                //TeamName = s.Team.Name,
                FIO = $"{s.Participant.Surname} {s.Participant.Name} {s.Participant.Patronymic}",
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

                dataGridList = _db.TeamCompositions.Where(x => x.IdTeam == _idP && x.Participant.Surname.StartsWith(filtrby))
                    .Select(s => new TeamCompositionShort()
                    {
                        Id = s.Id,
                        //TeamName = s.Team.Name,
                        FIO = $"{s.Participant.Surname} {s.Participant.Name} {s.Participant.Patronymic}",
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
            Dialog.Show(new uscTeamCompositionForTeamDialogAdd(0, true, _idP, this));
        }

        private void btnModify_Click(object sender, RoutedEventArgs e)
        {
            int id = (GridReferee.SelectedItem as TeamCompositionShort).Id;

            Dialog.Show(new uscTeamCompositionForTeamDialogAdd(id, false, _idP, this));
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {

            var dialog = new windowConfirmation();
            if (dialog.ShowDialog() == true)
            {
                int id = (GridReferee.SelectedItem as TeamCompositionShort).Id;

                TeamComposition teamComposition = _db.TeamCompositions.Find(id);

                _db.TeamCompositions.Remove(teamComposition);
                _db.SaveChanges();

                loadDataGrid();

                Growl.Success("Контракт успешно удален!");

            }

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
