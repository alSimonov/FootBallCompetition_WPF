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

        FootballClass.TeamComposition _teamComposition;
        FootballClass.Participant _participant;


        int _idT;
        bool _addOrModify;
        int _idP;



        public uscTeamCompositionDialogAdd(int idT, bool addOrModify, int idP)
        {
            InitializeComponent();

            ConfigHelper.Instance.SetLang("ru");


            dbConfiguration.ConfigureServices();
            _db = dbConfiguration.Services.GetService<MainDBContext>();

            _idP = idP;

            _participant = _db.Participants.Find(_idP);



            cbTeam.ItemsSource = _db.Teams.ToList();
            cbTeam.SelectedValuePath = "Id";
            cbTeam.DisplayMemberPath = "Name";


            cbAmpluaRole.ItemsSource = _db.AmpluaRoles.Where(x => x.Id == 1 || x.Id == 2 || x.Id == 3 || x.Id == 4).ToList();
            cbAmpluaRole.SelectedValuePath = "Id";
            cbAmpluaRole.DisplayMemberPath = "Name";



            _idT = idT;
            _addOrModify = addOrModify;
            _idP = idP;


            if (!addOrModify)
            {
                _teamComposition = _db.TeamCompositions.Find(idT);

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

            if (_addOrModify)
            {
                FootballClass.TeamComposition teamComposition = new FootballClass.TeamComposition();


                teamComposition.Participant = _participant;

                teamComposition.Team = (Team)cbTeam.SelectedItem;
                
                DateTime? tempContractStart = dpContractStart.SelectedDate;
                DateTime? tempContractEnd = dpContractEnd.SelectedDate;
                if (tempContractStart.HasValue && tempContractEnd.HasValue)
                {
                    teamComposition.ContractStart = (DateTime)tempContractStart;
                    teamComposition.ContractEnd = (DateTime)tempContractEnd;
                }
                else
                {

                }

                var tryParsePlayNum  = Byte.TryParse(tbPlayerNumber.Text, out byte playerNumInt);

                if (tryParsePlayNum)
                {
                    teamComposition.PlayerNumber = playerNumInt;
                }



                teamComposition.AmpluaRole = (AmpluaRole)cbAmpluaRole.SelectedItem;


                _db.TeamCompositions.Add(teamComposition);
                _db.SaveChanges();

                Growl.Success("Контракт успешно добавлен!");

            }
            else if (!_addOrModify)
            {
                _teamComposition.Team = (Team)cbTeam.SelectedItem;

                DateTime? tempContractStart = dpContractStart.SelectedDate;
                DateTime? tempContractEnd = dpContractEnd.SelectedDate;
                if (tempContractStart.HasValue && tempContractEnd.HasValue)
                {
                    _teamComposition.ContractStart = (DateTime)tempContractStart;
                    _teamComposition.ContractEnd = (DateTime)tempContractEnd;
                }
                else
                {

                }

                var tryParsePlayNum = Byte.TryParse(tbPlayerNumber.Text, out byte playerNumInt);

                if (tryParsePlayNum)
                {
                    _teamComposition.PlayerNumber = playerNumInt;

                }



                _teamComposition.AmpluaRole = (AmpluaRole)cbAmpluaRole.SelectedItem;


                _db.Entry(_teamComposition).State = EntityState.Modified;
                _db.SaveChanges();

                Growl.Success("Контракт успешно изменен!");

            }



        }
    }
}

