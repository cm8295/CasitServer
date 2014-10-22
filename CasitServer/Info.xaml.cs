using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CasitServer
{
    /// <summary>
    /// Interaction logic for Info.xaml
    /// </summary>
    public partial class Info : Window
    {
        public Info()
        {
            InitializeComponent();
            btnUserInfo.Click += new RoutedEventHandler(btnUserInfo_Click);
            
            showUderInfo();
        }

        DatabaseControl dbc = new DatabaseControl();
        private void showUderInfo()
        {
            gdInfoView.ItemsSource = dbc.GetUserInfo().Tables[0].DefaultView;
        }
        private void btnUserInfo_Click(object sender, RoutedEventArgs e)
        {
            gdInfoView.ItemsSource = dbc.GetUserInfo().Tables[0].DefaultView;
        }
    }
}
