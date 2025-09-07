using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using static System.Net.Mime.MediaTypeNames;
using Gerber_demex.page;

namespace Gerber_demex
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            frameCont.Content = new menuPage();

        }

        private void clickZayavki(object sender, RoutedEventArgs e)
        {
            
        }

        private void frContent_Content_ContentRendered(object sender, NavigationEventArgs e)
        {
            btBack.Visibility = frameCont.CanGoBack ? Visibility.Visible : Visibility.Collapsed;
        }

        private void frContent_Navigated(object sender, NavigationEventArgs e)
        {

        }

        private void clickBack(object sender, RoutedEventArgs e)
        {
            if (frameCont.CanGoBack)
            {
                frameCont.GoBack();
            }
        }

        private void frContent_Content_ContentRendered(object sender, EventArgs e)
        {

        }
    }
}
