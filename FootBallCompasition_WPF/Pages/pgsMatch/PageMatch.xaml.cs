 using FootBallCompasition_WPF.context;
using FootBallCompasition_WPF.FootballClass;
using FootBallCompasition_WPF.Short;
using FootBallCompasition_WPF.UserControls;
using HandyControl.Controls;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace FootBallCompasition_WPF.Pages.pgsMatch
{
    /// <summary>
    /// Логика взаимодействия для MatchPage.xaml
    /// </summary>
    public partial class PageMatch : Page
    {

        public MainDBContext? _db;

        List<MatchShort> matchList = new List<MatchShort>();

        public PageMatch()
        {
            InitializeComponent();

            dbConfiguration.ConfigureServices();
            _db = dbConfiguration.Services.GetService<MainDBContext>();

            frameMatch.Navigate(new PageMatchList());


        }


        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            Dialog.Show(new uscDialogMatchAdd(0, true));

            //loadMatch();


        }


        private void btnTabBtnList_Click(object sender, RoutedEventArgs e)
        {
            frameMatch.Navigate(new PageMatchList());

        }
    }
}
