using FootBallCompasition_WPF.context;
using FootBallCompasition_WPF.FootballClass;
using FootBallCompasition_WPF.Pages.pgsParticipant;
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
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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

            if (_addOrModify)
            {

                Participant participant = new Participant();

                participant.Surname = tbPartSurname.Text;
                participant.Name = tbPartName.Text;
                participant.Patronymic = tbPartPatronymic.Text;

                DateTime? dateTime = dpDateOfBirth.SelectedDate;
                if (dateTime.HasValue)
                {
                    participant.DateOfBirth = (DateTime)dateTime;

                }
                else
                {

                }

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

                DateTime? dateTime = dpDateOfBirth.SelectedDate;
                if (dateTime.HasValue)
                {
                    _participant.DateOfBirth = (DateTime)dateTime;

                }
                else
                {

                }

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
