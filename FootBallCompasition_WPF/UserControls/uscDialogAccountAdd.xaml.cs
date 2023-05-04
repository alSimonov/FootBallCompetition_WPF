using FootBallCompasition_WPF.context;
using FootBallCompasition_WPF.FootballClass;
using HandyControl.Controls;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
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

        FootballClass.Account _account;

        int _idP;
        bool _addOrModify;


        public uscDialogAccountAdd(int idP, bool addOrModify)
        {
            InitializeComponent();


            dbConfiguration.ConfigureServices();
            _db = dbConfiguration.Services.GetService<MainDBContext>();



            cbAccountRole.ItemsSource = _db.AccountRoles.ToList();
            cbAccountRole.SelectedValuePath = "Id";
            cbAccountRole.DisplayMemberPath = "Name";



            var ll = _db.Participants.Where(x => x.Role.Name == "Пользователь").ToList();

            ll.ForEach(x => x.SetFullName());

            cbParticipant.ItemsSource = ll;
            cbParticipant.SelectedValuePath = "Id";
            cbParticipant.DisplayMemberPath = "FullName";




            _idP = idP;
            _addOrModify = addOrModify;



            if (!addOrModify)
            {
                _account = _db.Accounts.Find(idP);

                tbLogin.Text = _account.Login;
                tbEmail.Text = _account.Email;

                cbParticipant.SelectedItem = _account.Participant;
                cbAccountRole.SelectedItem = _account.AccountRole;

            }


        }

        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {

            if (_addOrModify)
            {



                FootballClass.Account account = new FootballClass.Account();

                account.Login = tbLogin.Text;

                //byte[] passwordAdmin = SHA512.Create().ComputeHash(Encoding.BigEndianUnicode.GetBytes("admin@gmail.comadminadminСкрипеевСофронЗосимович22 июля 1998 г."));

                //Account account = new Account(Id = 1, Login = "admin", Password = passwordAdmin, IdParticipant = 422, Email = "admin@gmail.com", IdAccountRole = 1);

                account.Email = tbEmail.Text;
                account.Participant = (Participant)cbParticipant.SelectedItem;
                account.AccountRole = (AccountRole)cbAccountRole.SelectedItem;

                string dateStr = account.Participant.DateOfBirth.ToString("D");

                string strPart = $"{account.Email}{account.Login}{tbPassword.Text}{account.Participant.Surname}{account.Participant.Name}{account.Participant.Patronymic}{dateStr}";

                byte[] passwordHash = SHA512.Create().ComputeHash(Encoding.BigEndianUnicode.GetBytes(strPart));

                account.Password = passwordHash;


                _db.Accounts.Add(account);

                _db.SaveChanges();

                Growl.Success("Аккаунт успешно добавлен!");


            }

            else if (!_addOrModify)
            {
                _account.Login = tbLogin.Text;

                _account.Email = tbEmail.Text;
                _account.Participant = (Participant)cbParticipant.SelectedItem;
                _account.AccountRole = (AccountRole)cbAccountRole.SelectedItem;

                if (tbPassword.Text != string.Empty)
                {

                    string dateStr = _account.Participant.DateOfBirth.ToString("D");

                    string strPart = $"{_account.Email}{_account.Login}{tbPassword.Text}{_account.Participant.Surname}{_account.Participant.Name}{_account.Participant.Patronymic}{dateStr}";

                    byte[] passwordHash = SHA512.Create().ComputeHash(Encoding.BigEndianUnicode.GetBytes(strPart));

                    _account.Password = passwordHash;


                }

                _db.Entry(_account).State = EntityState.Modified;

                _db.SaveChanges();

                Growl.Success("Аккаунт успешно изменен!");



            }
        }
    }
}   
