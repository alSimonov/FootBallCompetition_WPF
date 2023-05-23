using FootBallCompasition_WPF.context;
using FootBallCompasition_WPF.FootballClass;
using FootBallCompasition_WPF.Pages;
using HandyControl.Controls;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualBasic.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
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
    /// Логика взаимодействия для uscDialogAccountAdd.xaml
    /// </summary>
    public partial class uscDialogAccountAdd : UserControl
    {


        public MainDBContext? _db;

        PageAccount _pageAccount;

        FootballClass.Account _account;

        int _idP;
        bool _addOrModify;

        bool _PersonalOrCommonDialog;


        public uscDialogAccountAdd(int idP, bool addOrModify, PageAccount pageAccount)
        {
            InitializeComponent();

            tblLoginErr.Visibility = Visibility.Collapsed;
            tblPasswordErr.Visibility = Visibility.Collapsed;
            tblEmailErr.Visibility = Visibility.Collapsed;
            tblParticipantErr.Visibility = Visibility.Collapsed;
            tblRoleErr.Visibility = Visibility.Collapsed;

            dbConfiguration.ConfigureServices();
            _db = dbConfiguration.Services.GetService<MainDBContext>();

            _PersonalOrCommonDialog = false;

            _idP = idP;
            _addOrModify = addOrModify;
            _pageAccount = pageAccount;

            if (_addOrModify)
                tglbtnActive.IsChecked = true;
            else tglbtnActive.IsChecked = false;

            loadComboboxData();


        }

        public uscDialogAccountAdd(int idP, bool addOrModify)
        {
            InitializeComponent();

            tblLoginErr.Visibility = Visibility.Collapsed;
            tblPasswordErr.Visibility = Visibility.Collapsed;
            tblEmailErr.Visibility = Visibility.Collapsed;
            tblParticipantErr.Visibility = Visibility.Collapsed;
            tblRoleErr.Visibility = Visibility.Collapsed;

            dbConfiguration.ConfigureServices();
            _db = dbConfiguration.Services.GetService<MainDBContext>();

            _PersonalOrCommonDialog = true;

            _idP = idP;
            _addOrModify = addOrModify;

            tglbtnActive.IsChecked = false;

            tglbtnActive.Visibility = Visibility.Collapsed;

            cbParticipant.IsEditable = false;
            cbParticipant.IsEnabled = false;
            cbAccountRole.IsEditable = false;
            cbAccountRole.IsEnabled = false;

            loadComboboxData();

        }



        public void loadComboboxData()
        {

            cbAccountRole.ItemsSource = _db.AccountRoles.ToList();
            cbAccountRole.SelectedValuePath = "Id";
            cbAccountRole.DisplayMemberPath = "Name";


            List<Participant> ll;

            if (tglbtnActive.IsChecked == true)
                ll = _db.Participants.Where(x => x.Active == true && x.Role.Name == "Пользователь").ToList();
            else
                ll = _db.Participants.Where(x => x.Role.Name == "Пользователь").ToList();


            ll.ForEach(x => x.SetFullName());

            cbParticipant.ItemsSource = ll;
            cbParticipant.SelectedValuePath = "Id";
            cbParticipant.DisplayMemberPath = "FullName";


            if (!_addOrModify)
            {
                _account = _db.Accounts.Find(_idP);

                tbLogin.Text = _account.Login;
                tbEmail.Text = _account.Email;

                cbParticipant.SelectedItem = _account.Participant;
                cbAccountRole.SelectedItem = _account.AccountRole;

            }



        }


        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {

            tblLoginErr.Visibility = tbLogin.Text == string.Empty ? Visibility.Visible : Visibility.Collapsed;
            tblPasswordErr.Visibility = tbPassword.Text == string.Empty ? Visibility.Visible : Visibility.Collapsed;
            tblEmailErr.Visibility = tbEmail.Text == string.Empty ? Visibility.Visible : Visibility.Collapsed;
            tblParticipantErr.Visibility = cbParticipant.SelectedIndex == -1 ? Visibility.Visible : Visibility.Collapsed;
            tblRoleErr.Visibility = cbAccountRole.SelectedIndex == -1 ? Visibility.Visible : Visibility.Collapsed;

            if (tblLoginErr.Visibility == 0 || tblPasswordErr.Visibility == 0 || tblEmailErr.Visibility == 0 || tblParticipantErr.Visibility == 0 || tblRoleErr.Visibility == 0)
                return;




            if (_db.Accounts.Any(u => u.Login == tbLogin.Text) && (_addOrModify ||  _account.Login != tbLogin.Text))
            {
                Growl.Warning("Такой логин уже существует!");
                return;
            }
            if (_db.Accounts.Any(u => u.Email == tbEmail.Text) && (_addOrModify || _account.Email != tbEmail.Text))
            {
                Growl.Warning("Такая почта уже зарегестрирована!");
                return;
            }




            if (_addOrModify)
            {


                FootballClass.Account account = new FootballClass.Account();

                account.Login = tbLogin.Text;
                account.Email = tbEmail.Text;
                account.Participant = (Participant)cbParticipant.SelectedItem;
                account.AccountRole = (AccountRole)cbAccountRole.SelectedItem;

                string forHash = $"{account.Email}{account.Login}{tbPassword.Text}";
                byte[] passwordHash = SHA512.Create().ComputeHash(Encoding.BigEndianUnicode.GetBytes(forHash));

                account.Password = passwordHash;


                _db.Accounts.Add(account);
                _db.SaveChanges();

                _pageAccount.loadAccount();
                Growl.Success("Аккаунт успешно добавлен!");


            }

            else if (!_addOrModify)
            {


                _account.Login = tbLogin.Text;

                _account.Email = tbEmail.Text;
                _account.Participant = (Participant)cbParticipant.SelectedItem;
                _account.AccountRole = (AccountRole)cbAccountRole.SelectedItem;

                string forHash = $"{_account.Email}{_account.Login}{tbPassword.Text.Trim()}";
                byte[] passwordHash = SHA512.Create().ComputeHash(Encoding.BigEndianUnicode.GetBytes(forHash));

                _account.Password = passwordHash;

                _db.Entry(_account).State = EntityState.Modified;
                _db.SaveChanges();

                if(!_PersonalOrCommonDialog)
                    _pageAccount.loadAccount();

                Growl.Success("Аккаунт успешно изменен!");

            }
        }

        private void tglbtnActive_Click(object sender, RoutedEventArgs e)
        {
            loadComboboxData();
        }
    }
}   
