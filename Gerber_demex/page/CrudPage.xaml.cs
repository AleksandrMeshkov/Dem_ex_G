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
        private tehnEntities context = new tehnEntities();
        private List<ProductRequest> _requests;

        public CrudPage()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                    _requests = context.ProductRequest
                        .Include("Product")//таблица продуктов
                        .Include("Partners")//таблица партнеров
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
            NavigationService.Navigate(new AddEditRequestPage(null));//навигация между страницами
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            var selectedRequest = dgRequests.SelectedItem as ProductRequest;
            if (selectedRequest != null)//клик по заявке для редактирования 
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
                        using (var context = Helper.GetContext())
                        {
                            var entityToDelete = context.ProductRequest
                                .FirstOrDefault(pr => pr.Request_id == selectedRequest.Request_id);

                            if (entityToDelete != null)
                            {
                                context.ProductRequest.Remove(entityToDelete);
                                context.SaveChanges();// сохранение данных
                                LoadData();
                                MessageBox.Show("Заявка успешно удалена");
                            }
                        }
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

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void dgRequests_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }
    }
}