﻿using FootBallCompasition_WPF.context;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace FootBallCompasition_WPF.Windows
{
    /// <summary>
    /// Логика взаимодействия для WindowAuthorization.xaml
    /// </summary>
    public partial class WindowAuthorization : System.Windows.Window
    {

        public record class AccountR(int IdAccount, string Email, string AccountRoleName, string PartSurname, string PartName);

        public MainDBContext? _db;

        public string TheAccountRole;
        public string FIOAccount;
        public int IdAccount;

        public WindowAuthorization()
        {
            InitializeComponent();

            btnLogin.Click += btnLogin_Click;
            
            btnWindowClose.Click += btnWindowClose_Click;

            tblLoginErr.Visibility = Visibility.Collapsed;
            tblPasswordErr.Visibility = Visibility.Collapsed;
            //TODO исправить цвет ошибки при неверном вводе пароля на xaml


            dbConfiguration.ConfigureServices();
            _db = dbConfiguration.Services.GetService<MainDBContext>();

        }



        private void btnWindowClose_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            tblLoginErr.Visibility = Visibility.Collapsed;
            tblPasswordErr.Visibility = Visibility.Collapsed;



            string Login = tbLogin.Text;
            string Password = pbPassword.Password.Trim();

            //####################################################

            //DialogResult = true;
            //TheAccountRole = "Админ";

            //####################################################


            if (_db.Accounts.Any(u => u.Login == Login))
            {

                var accountList = _db.Accounts.Where(s => s.Login == Login).
                    Select(u => new AccountR(u.Id, u.Email, u.AccountRole.Name, u.Participant.Surname, u.Participant.Name)).ToList();

                string forHash = $"{accountList[0].Email}{Login}{Password}";
                byte[] passwordByte = SHA512.Create().ComputeHash(Encoding.BigEndianUnicode.GetBytes(forHash));


                if (_db.Accounts.Any(u => u.Login == Login && u.Password == passwordByte))
                {

                    DialogResult = true;

                    TheAccountRole = accountList[0].AccountRoleName;
                    FIOAccount = accountList[0].PartSurname + " " + accountList[0].PartName;
                    IdAccount = accountList[0].IdAccount;

                }
                else
                    tblPasswordErr.Visibility = Visibility.Visible;
            }
            else
                tblLoginErr.Visibility = Visibility.Visible;

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
