using FootBallCompasition_WPF.context;
using FootBallCompasition_WPF.FootballClass;
using HandyControl.Controls;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualBasic.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace FootBallCompasition_WPF.Windows
{
    /// <summary>
    /// Логика взаимодействия для WindowAuthorization.xaml
    /// </summary>
    public partial class WindowAuthorization : System.Windows.Window
    {

        public record class AccountR(string Email, string AccountRoleName, string PartSurname, string PartName, string PartPatronymic, DateTime PartDateOfBirth);

        public MainDBContext? _db;

        public WindowAuthorization()
        {
            InitializeComponent();

            btnLogin.Click += btnLogin_Click;
            
            btnWindowClose.Click += btnWindowClose_Click;



            dbConfiguration.ConfigureServices();
            _db = dbConfiguration.Services.GetService<MainDBContext>();

        }



        private void btnWindowClose_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {

            string Login = tbLogin.Text;
            string Password = pbPassword.Password;

            //DialogResult = true;


            //byte[] passwordAdmin = SHA512.Create().ComputeHash(Encoding.BigEndianUnicode.GetBytes("admin@gmail.comadminadminСкрипеевСофронЗосимович22 июля 1998 г."));

            //_db.Participants.Where(x =>x.Id == 422).ToList();

            //Account account = new Account(Id = 1, Login = "admin", Password = passwordAdmin, IdParticipant = 422, Email = "admin@gmail.com", IdAccountRole = 1);

            //_db.Accounts.Add(account);
            //_db.SaveChanges();







            //1998-07-22
            //admin @gmail.comadminadminСкрипеевСофронЗосимович22 июля 1998 г.

            //var mydatetime = DateTime.ParseExact("1998-07-22", "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture).ToString("D");
            //var strnn = $"admin@gmail.comadminadminСкрипеевСофронЗосимович{mydatetime}";
            //System.Windows.MessageBox.Show(strnn);
            //tbLogin.Text = strnn;

            //..........................................................


            if (_db.Accounts.Any(u => u.Login == Login))
            {


                var accountList = _db.Accounts.Where(s => s.Login == Login).
                    Select(u => new AccountR(u.Email, u.AccountRole.Name, u.Participant.Surname, u.Participant.Name, u.Participant.Patronymic, u.Participant.DateOfBirth)).ToList();

                var dateOfBirth = accountList[0].PartDateOfBirth.ToString("D");
                string forHash = $"{accountList[0].Email}{Login}{Password}{accountList[0].PartSurname}{accountList[0].PartName}{accountList[0].PartPatronymic}{dateOfBirth}";

                byte[] passwordByte = SHA512.Create().ComputeHash(Encoding.BigEndianUnicode.GetBytes(forHash));



                if (_db.Accounts.Any(u => u.Login == Login && u.Password == passwordByte))
                {

                    //HandyControl.Controls.MessageBox.Show( new HandyControl.Data.MessageBoxInfo { Message = "Вход подтвержден" });
                    DialogResult = true;

                    //TheAccountRole = accountList[0].Name;

                }
                else //TODO Руссифицировать messageBox
                    HandyControl.Controls.MessageBox.Show("Не верный пароль.");
            }
            else
                HandyControl.Controls.MessageBox.Show("Не верный логин.");


        }


        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

    }
}
