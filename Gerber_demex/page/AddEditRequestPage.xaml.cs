using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Gerber_demex.helpersClass;
using Gerber_demex.models;

namespace Gerber_demex.page
{
    public partial class AddEditRequestPage : Page
    {
        private tehnEntities _context;
        private ProductRequest _currentRequest;
        private bool _isEditMode;

        public AddEditRequestPage(ProductRequest request)
        {
            InitializeComponent();
            _context = Helper.GetContext();
            _currentRequest = request;
            _isEditMode = request != null;

            LoadComboBoxData();
            LoadRequestData();
        }

        private void LoadComboBoxData()
        {
            try
            {
                cbProduct.ItemsSource = _context.Product.ToList();

                cbPartner.ItemsSource = _context.Partners.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}");
            }
        }

        private void LoadRequestData()
        {
            if (_isEditMode && _currentRequest != null)
            {
                cbProduct.SelectedValue = _currentRequest.Product_id;
                cbPartner.SelectedValue = _currentRequest.Partners_id;
                txtCount.Text = _currentRequest.Count.ToString();
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cbProduct.SelectedItem == null)
                {
                    MessageBox.Show("Выберите продукт");
                    return;
                }

                if (cbPartner.SelectedItem == null)
                {
                    MessageBox.Show("Выберите партнера");
                    return;
                }

                if (!int.TryParse(txtCount.Text, out int count) || count <= 0)
                {
                    MessageBox.Show("Введите корректное количество");
                    return;
                }

                if (_isEditMode)
                {
                    _currentRequest.Product_id = (int)cbProduct.SelectedValue;
                    _currentRequest.Partners_id = (int)cbPartner.SelectedValue;
                    _currentRequest.Count = count;
                }
                else
                {
                    var newRequest = new ProductRequest
                    {
                        Product_id = (int)cbProduct.SelectedValue,
                        Partners_id = (int)cbPartner.SelectedValue,
                        Count = count
                    };
                    _context.ProductRequest.Add(newRequest);
                }

                _context.SaveChanges();
                MessageBox.Show("Данные успешно сохранены");
                NavigationService.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка сохранения: {ex.Message}");
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}