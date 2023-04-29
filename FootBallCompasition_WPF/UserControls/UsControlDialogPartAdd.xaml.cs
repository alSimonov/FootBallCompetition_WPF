using FootBallCompasition_WPF.context;
using FootBallCompasition_WPF.FootballClass;
using HandyControl.Controls;
using HandyControl.Tools;
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

        List<Role> roleList = new List<Role>(); 


        public UsControlDialogPartAdd()
        {
            InitializeComponent();


            ConfigHelper.Instance.SetLang("ru");



            dbConfiguration.ConfigureServices();
            _db = dbConfiguration.Services.GetService<MainDBContext>();


            roleList = _db.Roles.ToList();

            cbRole.ItemsSource = roleList;
            cbRole.SelectedValuePath = "Id";
            cbRole.DisplayMemberPath = "Name";



            //cbRole.ItemsSource = _db.Roles.ToList();

            //cbRole.ValueMember = "Id";
            //cbRole.DisplayMember = "Name";




        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
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
            participant.Role = (Role) cbRole.SelectedItem;


            //_db.Participants.Add(participant);

            //_db.SaveChanges();

            Growl.Success("File saved successfully!");

            //MessageBox.Show("Новый работник добавлен");


            //loadEmpl();






        }
    }
}
