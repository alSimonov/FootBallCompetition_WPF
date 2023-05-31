using FootBallCompasition_WPF.context;
using FootBallCompasition_WPF.FootballClass;
using FootBallCompasition_WPF.Pages.pgsParticipant;
using HandyControl.Controls;
using HandyControl.Tools;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Windows;

namespace FootBallCompasition_WPF.UserControls
{
    /// <summary>
    /// Логика взаимодействия для UsControlDialogPartAdd.xaml
    /// </summary>
    public partial class UsControlDialogPartAdd : System.Windows.Controls.UserControl
    {

        public MainDBContext? _db;

        PageParticipant _pageParticipant;

        Participant _participant;


        int _idP;
        bool _addOrModify;

        public UsControlDialogPartAdd(int idP, bool addOrModify, PageParticipant pageParticipant)
        {
            InitializeComponent();

            tblSurnameErr.Visibility = Visibility.Collapsed;
            tblNameErr.Visibility = Visibility.Collapsed;
            tblPatronymicErr.Visibility = Visibility.Collapsed;
            tblDateOfBirthErr.Visibility = Visibility.Collapsed;
            tblPhoneErr.Visibility = Visibility.Collapsed;
            tblRoleErr.Visibility = Visibility.Collapsed;




            //Установка языка для datePicker

            ConfigHelper.Instance.SetLang("ru");

            dbConfiguration.ConfigureServices();
            _db = dbConfiguration.Services.GetService<MainDBContext>();

            _pageParticipant = pageParticipant;

            cbRole.ItemsSource = _db.Roles.ToList();
            cbRole.SelectedValuePath = "Id";
            cbRole.DisplayMemberPath = "Name";

            _idP = idP;
            _addOrModify = addOrModify;



            if(!addOrModify)
            {

                _participant = _db.Participants.Find(idP);


                tbPartSurname.Text = _participant.Surname;
                tbPartName.Text = _participant.Name;
                tbPartPatronymic.Text = _participant.Patronymic;

                dpDateOfBirth.SelectedDate = _participant.DateOfBirth;

                tbPartPhone.Text = _participant.Telephone;
                cbRole.SelectedItem = _participant.Role;


            }

        }


        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {

            tblSurnameErr.Visibility = tbPartSurname.Text == string.Empty ? Visibility.Visible : Visibility.Collapsed;
            tblNameErr.Visibility = tbPartName.Text == string.Empty ? Visibility.Visible : Visibility.Collapsed;
            tblPatronymicErr.Visibility = tbPartPatronymic.Text == string.Empty ? Visibility.Visible : Visibility.Collapsed;
            tblDateOfBirthErr.Visibility = !dpDateOfBirth.SelectedDate.HasValue  ? Visibility.Visible : Visibility.Collapsed;
            tblPhoneErr.Visibility = tbPartPhone.Text == string.Empty? Visibility.Visible : Visibility.Collapsed;
            tblRoleErr.Visibility = cbRole.SelectedIndex == -1 ? Visibility.Visible : Visibility.Collapsed;


            if (tblSurnameErr.Visibility == 0 || tblNameErr.Visibility == 0 || tblPatronymicErr.Visibility == 0 || tblDateOfBirthErr.Visibility == 0
                || tblPhoneErr.Visibility == 0 || tblRoleErr.Visibility == 0)
                return;


            if (_db.Participants.Any(u => u.Telephone == tbPartPhone.Text) && (_addOrModify || _participant.Telephone != tbPartPhone.Text))
            {
                Growl.Warning("Этот телефон уже зарегестрирован!");
                return;
            }
           


            if (_addOrModify)
            {

                Participant participant = new Participant();

                participant.Surname = tbPartSurname.Text;
                participant.Name = tbPartName.Text;
                participant.Patronymic = tbPartPatronymic.Text;
                participant.DateOfBirth = (DateTime) dpDateOfBirth.SelectedDate;
                participant.Telephone = tbPartPhone.Text;
                participant.Role = (Role)cbRole.SelectedItem;
                participant.Active = true;


                _db.Participants.Add(participant);
                _db.SaveChanges();
                _pageParticipant.loadPart();

                Growl.Success("Участник успешно добавлен!");



            }
            else if (!_addOrModify)
            {

                _participant.Surname = tbPartSurname.Text;
                _participant.Name = tbPartName.Text;
                _participant.Patronymic = tbPartPatronymic.Text;
                _participant.DateOfBirth = (DateTime)dpDateOfBirth.SelectedDate;
                _participant.Telephone = tbPartPhone.Text;
                _participant.Role = (Role)cbRole.SelectedItem;


                _db.Entry(_participant).State = EntityState.Modified;
                _db.SaveChanges();
                _pageParticipant.loadPart();

                Growl.Success("Участник успешно изменен!");                

            }

        }

    }
}
