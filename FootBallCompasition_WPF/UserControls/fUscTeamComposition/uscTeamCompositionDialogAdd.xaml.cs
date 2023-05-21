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

namespace FootBallCompasition_WPF.UserControls.fUscTeamComposition
{
    /// <summary>
    /// Логика взаимодействия для uscTeamCompositionDialogAdd.xaml
    /// </summary>
    public partial class uscTeamCompositionDialogAdd : UserControl
    {


        public MainDBContext? _db;

        uscTeamComposition _uscTeamComposition;

        FootballClass.TeamComposition _teamComposition;
        FootballClass.Participant _participant;


        int _idT;
        bool _addOrModify;
        int _idP;



        public uscTeamCompositionDialogAdd(int idT, bool addOrModify, int idP, uscTeamComposition uscTeamComposition)
        {
            InitializeComponent();


            tblTeamErr.Visibility = Visibility.Collapsed;
            tblStartContractErr.Visibility = Visibility.Collapsed;
            tblEndContractErr.Visibility = Visibility.Collapsed;
            tblPlayerNumberErr.Visibility = Visibility.Collapsed;
            tblAmpluaRoleErr.Visibility = Visibility.Collapsed;



            ConfigHelper.Instance.SetLang("ru");


            dbConfiguration.ConfigureServices();
            _db = dbConfiguration.Services.GetService<MainDBContext>();

            _idT = idT;
            _addOrModify = addOrModify;
            _idP = idP;
            this._uscTeamComposition = uscTeamComposition;

            _participant = _db.Participants.Find(_idP);


            if (_addOrModify)
                tglbtnActive.IsChecked = true;
            else tglbtnActive.IsChecked = false;


            loadComboboxData();


        }

        public void loadComboboxData()
        {
         
            if (tglbtnActive.IsChecked == true)
                cbTeam.ItemsSource = _db.Teams.Where(x => x.Active == true).ToList();
            else
                cbTeam.ItemsSource = _db.Teams.ToList();

            cbTeam.SelectedValuePath = "Id";
            cbTeam.DisplayMemberPath = "Name";


            cbAmpluaRole.ItemsSource = _db.AmpluaRoles.Where(x => x.Id == 1 || x.Id == 2 || x.Id == 3 || x.Id == 4).ToList();
            cbAmpluaRole.SelectedValuePath = "Id";
            cbAmpluaRole.DisplayMemberPath = "Name";



            if (!_addOrModify)
            {
                _teamComposition = _db.TeamCompositions.Find(_idT);

                //var teamCompositionnn = _db.TeamCompositions.Find(_event.IdTeamComposition);

                cbTeam.SelectedValue = _teamComposition.IdTeam;
                dpContractStart.SelectedDate = _teamComposition.ContractStart;
                dpContractEnd.SelectedDate = _teamComposition.ContractEnd;
                tbPlayerNumber.Text = _teamComposition.PlayerNumber.ToString();
                cbAmpluaRole.SelectedValue = _teamComposition.IdAmpluaRole;

            }



        }



        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {

            tblTeamErr.Visibility = cbTeam.SelectedIndex == -1 ? Visibility.Visible : Visibility.Collapsed;
            tblStartContractErr.Visibility = !dpContractStart.SelectedDate.HasValue ? Visibility.Visible : Visibility.Collapsed;
            tblEndContractErr.Visibility = !dpContractEnd.SelectedDate.HasValue ? Visibility.Visible : Visibility.Collapsed;
            tblPlayerNumberErr.Visibility = !Byte.TryParse(tbPlayerNumber.Text, out byte playerNumInt) ? Visibility.Visible : Visibility.Collapsed;
            tblAmpluaRoleErr.Visibility = cbAmpluaRole.SelectedIndex == -1 ? Visibility.Visible : Visibility.Collapsed;

            if (tblTeamErr.Visibility == 0 || tblStartContractErr.Visibility == 0 || tblEndContractErr.Visibility == 0 || tblPlayerNumberErr.Visibility == 0
                || tblAmpluaRoleErr.Visibility == 0)
                return;




            if (_addOrModify)
            {
                FootballClass.TeamComposition teamComposition = new FootballClass.TeamComposition();


                teamComposition.Participant = _participant;
                teamComposition.Team = (Team)cbTeam.SelectedItem;
                teamComposition.ContractStart = (DateTime) dpContractStart.SelectedDate;
                teamComposition.ContractEnd = (DateTime) dpContractEnd.SelectedDate;
                teamComposition.PlayerNumber = playerNumInt;
                teamComposition.AmpluaRole = (AmpluaRole)cbAmpluaRole.SelectedItem;


                _db.TeamCompositions.Add(teamComposition);
                _db.SaveChanges();

                _uscTeamComposition.loadDataGrid();

                Growl.Success("Контракт успешно добавлен!");

            }
            else if (!_addOrModify)
            {
                _teamComposition.Team = (Team)cbTeam.SelectedItem;
                _teamComposition.ContractStart = (DateTime)dpContractStart.SelectedDate;
                _teamComposition.ContractEnd = (DateTime)dpContractEnd.SelectedDate;
                _teamComposition.PlayerNumber = playerNumInt;
                _teamComposition.AmpluaRole = (AmpluaRole)cbAmpluaRole.SelectedItem;


                _db.Entry(_teamComposition).State = EntityState.Modified;
                _db.SaveChanges();
                _uscTeamComposition.loadDataGrid();

                Growl.Success("Контракт успешно изменен!");

            }



        }

        private void tglbtnActive_Click(object sender, RoutedEventArgs e)
        {
            loadComboboxData();
        }

        private void tbPlayerNumber_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}

