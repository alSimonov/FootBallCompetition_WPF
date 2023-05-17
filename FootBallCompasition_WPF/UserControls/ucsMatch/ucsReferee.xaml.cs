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

namespace FootBallCompasition_WPF.UserControls.ucsMatch
{
    /// <summary>
    /// Логика взаимодействия для ucsReferee.xaml
    /// </summary>
    public partial class ucsReferee : UserControl
    {

        public record class PartRole(int id, string last, string first, DateTime date, string NameRole);
        private MainDBContext? _db;

        List<JudgingStaffShort> dataGridList = new List<JudgingStaffShort>();

        int _idM;
        


        public ucsReferee(MatchShort matchShort)
        {
            InitializeComponent();



            dbConfiguration.ConfigureServices();
            _db = dbConfiguration.Services.GetService<MainDBContext>();

            _idM = matchShort.Id;

            tblMatchInfo.Text = $"{matchShort.Team1Name} - {matchShort.Team2Name} | {matchShort.Date}";

            loadDataGrid();


        }

        public void loadDataGrid()
        {

            dataGridList = _db.JudgingStaffs.Where(x => x.IdMatch == _idM)
            .Select(s => new JudgingStaffShort()
            {
                Id = s.Id,
                FIO = $"{s.Participant.Surname} {s.Participant.Name} {s.Participant.Patronymic}",
                Telephone = s.Participant.Telephone,
                AmpluaRoleName = s.AmpluaRole.Name,
            }).ToList();


            GridReferee.ItemsSource = dataGridList.Take(10).ToList();

            pagGrid.MaxPageCount = (int)Math.Ceiling(dataGridList.Count / 10.0);

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
                dataGridList = _db.JudgingStaffs.Where(x => x.IdMatch == _idM)
                    .Select(s => new JudgingStaffShort()
                    {
                        Id = s.Id,
                        FIO = $"{s.Participant.Surname} {s.Participant.Name} {s.Participant.Patronymic}",
                        Telephone = s.Participant.Telephone,
                        AmpluaRoleName = s.AmpluaRole.Name,
                    }).Where(x => x.FIO.StartsWith(filtrby))
                    .ToList();


                GridReferee.ItemsSource = dataGridList.Take(10).ToList();

                pagGrid.MaxPageCount = (int)Math.Ceiling(dataGridList.Count / 10.0);

                txtblListCount.Text = "Найдено записей: ";
                txtblListCount.Text += dataGridList.Count().ToString();

            }


        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            Dialog.Show(new ucsJudgingStaffDialogAdd(0, true, _idM, this));
        }

        private void btnModify_Click(object sender, RoutedEventArgs e)
        {
            int id = (GridReferee.SelectedItem as JudgingStaffShort).Id;

            Dialog.Show(new ucsJudgingStaffDialogAdd(id, false, _idM, this));
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            int id = (GridReferee.SelectedItem as JudgingStaffShort).Id;

            JudgingStaff judgingStaff = _db.JudgingStaffs.Find(id);

            _db.JudgingStaffs.Remove(judgingStaff);
            _db.SaveChanges();

            loadDataGrid();

            Growl.Success("Судья успешно удален!");

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
            GridReferee.ItemsSource = dataGridList.Skip((e.Info - 1) * 10).Take(10).ToList();
        }




    }
}
