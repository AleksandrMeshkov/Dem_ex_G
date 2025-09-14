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
using Gerber_demex.page;

namespace Gerber_demex
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            FrameCont.Content = new menuPage();// выделяем основную страницу и выводим ее
        }

        private void clickZayavki(object sender, RoutedEventArgs e)
        {

        }

        private void frContent_Navigated(object sender, NavigationEventArgs e)
        {

        }

        private void ClickBack(object sender, RoutedEventArgs e)
        {
            if (FrameCont.CanGoBack)
            {
                FrameCont.GoBack();// возвращение назад
            }
        }

        private void FrameCont_ContentRendered(object sender, EventArgs e) //функиця для скрытия кнопки и изменения заголовка страниц
        {
            if (FrameCont.Content is aplicationPage)
            {
                btBack.Visibility = Visibility.Visible;
                Title.Text = "Просмотр заявок";
            }
            else if (FrameCont.Content is CrudPage)
            {
                btBack.Visibility = Visibility.Visible;
                Title.Text = "Редактирование заявки";
            }
            else if (FrameCont.Content is menuPage)
            {
                btBack.Visibility = Visibility.Hidden;
                Title.Text = "Главное меню";
            }
            else
            {
                
                btBack.Visibility = Visibility.Collapsed;

            }
        }
    }
}