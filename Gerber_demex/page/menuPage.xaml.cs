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

namespace Gerber_demex.page
{
    /// <summary>
    /// Логика взаимодействия для menuPage.xaml
    /// </summary>
    public partial class menuPage : Page
    {
        public menuPage()
        {
            InitializeComponent();
        }

        private void clickZayavki(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new aplicationPage());
        }

        private void ClickCrud(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new CrudPage());
        }
    }
}
