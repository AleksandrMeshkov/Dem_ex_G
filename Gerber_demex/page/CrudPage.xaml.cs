using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Gerber_demex.helpersClass;
using Gerber_demex.models;

namespace Gerber_demex.page
{
    public partial class CrudPage : Page
    {
        private tehnEntities _context;
        private List<ProductRequest> _requests;

        public CrudPage()
        {
            InitializeComponent();
            _context = Helper.GetContext();
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                _requests = _context.ProductRequest
                    .Include("Product")
                    .Include("Partners")
                    .ToList();

                dgRequests.ItemsSource = _requests;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}");
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddEditRequestPage(null));
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            var selectedRequest = dgRequests.SelectedItem as ProductRequest;
            if (selectedRequest != null)
            {
                NavigationService.Navigate(new AddEditRequestPage(selectedRequest));
            }
            else
            {
                MessageBox.Show("Выберите заявку для редактирования");
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var selectedRequest = dgRequests.SelectedItem as ProductRequest;
            if (selectedRequest != null)
            {
                var result = MessageBox.Show("Вы уверены, что хотите удалить эту заявку?",
                    "Подтверждение удаления", MessageBoxButton.YesNo);

                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        _context.ProductRequest.Remove(selectedRequest);
                        _context.SaveChanges();
                        LoadData(); 
                        MessageBox.Show("Заявка успешно удалена");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка удаления: {ex.Message}");
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите заявку для удаления");
            }
        }

        private void dgRequests_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }
    }
}