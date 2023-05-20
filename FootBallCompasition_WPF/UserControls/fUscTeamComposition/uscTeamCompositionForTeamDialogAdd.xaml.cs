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

namespace FootBallCompasition_WPF.UserControls.fUscTeamComposition
{
    /// <summary>
    /// Логика взаимодействия для uscTeamCompositionForTeamDialogAdd.xaml
    /// </summary>
    public partial class uscTeamCompositionForTeamDialogAdd : UserControl
    {

        public MainDBContext? _db;

        uscTeamCompositionForTeam _uscTeamCompositionForTeam;

        FootballClass.TeamComposition _teamComposition;
        FootballClass.Team _team;


        int _idT;
        bool _addOrModify;
        int _idP;




        public uscTeamCompositionForTeamDialogAdd(int idT, bool addOrModify, int idP, uscTeamCompositionForTeam uscTeamCompositionForTeam)
        {
            InitializeComponent();

            ConfigHelper.Instance.SetLang("ru");


            dbConfiguration.ConfigureServices();
            _db = dbConfiguration.Services.GetService<MainDBContext>();

            _idT = idT;
            _addOrModify = addOrModify;
            _idP = idP;
            _uscTeamCompositionForTeam = uscTeamCompositionForTeam;

            _team = _db.Teams.Find(_idP);

            if (_addOrModify)
                tglbtnActive.IsChecked = true;
            else tglbtnActive.IsChecked = false;


            loadComboboxData();


        }

        public void loadComboboxData()
        {

            List<Participant> ll;

            if (tglbtnActive.IsChecked == true)
                ll = _db.Participants.Where(x => x.Active == true && x.Role.Name == "Игрок").ToList();
            else
                ll = _db.Participants.Where(x => x.Role.Name == "Игрок").ToList();

            ll.ForEach(x => x.SetFullName());

            cbPart.ItemsSource = ll;
            cbPart.SelectedValuePath = "Id";
            cbPart.DisplayMemberPath = "FullName";


            cbAmpluaRole.ItemsSource = _db.AmpluaRoles.Where(x => x.Id == 1 || x.Id == 2 || x.Id == 3 || x.Id == 4).ToList();
            cbAmpluaRole.SelectedValuePath = "Id";
            cbAmpluaRole.DisplayMemberPath = "Name";


            if (!_addOrModify)
            {
                _teamComposition = _db.TeamCompositions.Find(_idT);

                //var teamCompositionnn = _db.TeamCompositions.Find(_event.IdTeamComposition);

                cbPart.SelectedValue = _teamComposition.IdParticipant;
                dpContractStart.SelectedDate = _teamComposition.ContractStart;
                dpContractEnd.SelectedDate = _teamComposition.ContractEnd;
                tbPlayerNumber.Text = _teamComposition.PlayerNumber.ToString();
                cbAmpluaRole.SelectedValue = _teamComposition.IdAmpluaRole;

            }


        }



        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {

            if (_addOrModify)
            {
                FootballClass.TeamComposition teamComposition = new FootballClass.TeamComposition();


                teamComposition.Team = _team;

                teamComposition.Participant = (Participant)cbPart.SelectedItem;

                DateTime? tempContractStart = dpContractStart.SelectedDate;
                DateTime? tempContractEnd = dpContractEnd.SelectedDate;
                if (tempContractStart.HasValue && tempContractEnd.HasValue && tempContractStart < tempContractEnd)
                {
                    teamComposition.ContractStart = (DateTime)tempContractStart;
                    teamComposition.ContractEnd = (DateTime)tempContractEnd;
                }
                else
                {
                    Growl.Warning("Дата начала контракта должна быть раньше даты оконачания контракта!");
                    return;
                }

                var tryParsePlayNum = Byte.TryParse(tbPlayerNumber.Text, out byte playerNumInt);

                if (tryParsePlayNum)
                {
                    teamComposition.PlayerNumber = playerNumInt;
                }



                teamComposition.AmpluaRole = (AmpluaRole)cbAmpluaRole.SelectedItem;


                _db.TeamCompositions.Add(teamComposition);
                _db.SaveChanges();

                _uscTeamCompositionForTeam.loadDataGrid();

                Growl.Success("Контракт успешно добавлен!");

            }
            else if (!_addOrModify)
            {
                _teamComposition.Participant = (Participant)cbPart.SelectedItem;

                DateTime? tempContractStart = dpContractStart.SelectedDate;
                DateTime? tempContractEnd = dpContractEnd.SelectedDate;
                if (tempContractStart.HasValue && tempContractEnd.HasValue && tempContractStart < tempContractEnd)
                {
                    _teamComposition.ContractStart = (DateTime)tempContractStart;
                    _teamComposition.ContractEnd = (DateTime)tempContractEnd;
                }
                else
                {
                    Growl.Warning("Дата начала контракта должна быть раньше даты оконачания контракта!");
                    return;
                }

                var tryParsePlayNum = Byte.TryParse(tbPlayerNumber.Text, out byte playerNumInt);

                if (tryParsePlayNum)
                {
                    _teamComposition.PlayerNumber = playerNumInt;

                }



                _teamComposition.AmpluaRole = (AmpluaRole)cbAmpluaRole.SelectedItem;


                _db.Entry(_teamComposition).State = EntityState.Modified;
                _db.SaveChanges();


                _uscTeamCompositionForTeam.loadDataGrid();

                Growl.Success("Контракт успешно изменен!");

            }




        }

        private void tglbtnActive_Click(object sender, RoutedEventArgs e)
        {
            loadComboboxData();
        }
    }
}
