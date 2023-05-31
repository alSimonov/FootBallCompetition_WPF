using FootBallCompasition_WPF.context;
using FootBallCompasition_WPF.FootballClass;
using FootBallCompasition_WPF.Short;
using FootBallCompasition_WPF.UserControls;
using FootBallCompasition_WPF.Windows;
using HandyControl.Controls;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace FootBallCompasition_WPF.Pages
{
    /// <summary>
    /// Логика взаимодействия для PageAccount.xaml
    /// </summary>
    public partial class PageAccount : Page
    {

        public MainDBContext? _db;

        List<AccountShort> accountList = new List<AccountShort>();


        public PageAccount()
        {
            InitializeComponent();

            dbConfiguration.ConfigureServices();
            _db = dbConfiguration.Services.GetService<MainDBContext>();

            loadAccount();
        }


        public void loadAccount()
        {

            accountList = _db.Accounts.Select(s =>
                new AccountShort()
                {
                    Id = s.Id,
                    Login = s.Login,
                    Email = s.Email,
                    PartFullName = $"{s.Participant.Surname} {s.Participant.Name} {s.Participant.Patronymic} {s.Participant.Telephone}",
                    AccountRole = s.AccountRole.Name
                }).ToList();

            //GridMatch.ItemsSource = accountList;

            GridAccount.ItemsSource = accountList.Take(10).ToList();


            pagAccount.MaxPageCount = (int)Math.Ceiling(accountList.Count / 10.0);


            txtblListCount.Text = "Найдено записей: ";
            txtblListCount.Text += accountList.Count().ToString();

        }

        private void loadAccount(string filtrby)
        {

            if (filtrby == "")
            {
                loadAccount();
            }
            else
            {
                accountList = _db.Accounts.Where(x => x.Login.StartsWith(filtrby))
                .Select(s => new AccountShort()
                {
                    Id = s.Id,
                    Login = s.Login,
                    Email = s.Email,
                    PartFullName = $"{s.Participant.Surname} {s.Participant.Name} {s.Participant.Patronymic} {s.Participant.Telephone}",
                    AccountRole = s.AccountRole.Name
                }).ToList();

                GridAccount.ItemsSource = accountList.Take(10).ToList();

                pagAccount.MaxPageCount = (int)Math.Ceiling(accountList.Count / 10.0);

                txtblListCount.Text = "Найдено записей: ";
                txtblListCount.Text += accountList.Count().ToString();
            }

        }


        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            Dialog.Show(new uscDialogAccountAdd(0, true, this));
        }


        private void btnModify_Click(object sender, RoutedEventArgs e)
        {
            int id = (GridAccount.SelectedItem as AccountShort).Id;

            Dialog.Show(new uscDialogAccountAdd(id, false, this));
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {

            var dialog = new windowConfirmation();
            if (dialog.ShowDialog() == true)
            {
                int id = (GridAccount.SelectedItem as AccountShort).Id;

                Account account = _db.Accounts.Find(id);
                _db.Accounts.Remove(account);
                _db.SaveChanges();

                loadAccount();
                Growl.Success("Аккаунт успешно удален!");
            }

        }





        private void tbFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            loadAccount(tbFilter.Text.Trim());
        }

        private void page_PageUpdated(object sender, HandyControl.Data.FunctionEventArgs<int> e)
        {
            GridAccount.ItemsSource = accountList.Skip((e.Info - 1) * 10).Take(10).ToList();
        }


        private void btnTabBtnList_Click(object sender, RoutedEventArgs e)
        {
            loadAccount();
        }

    }
}
