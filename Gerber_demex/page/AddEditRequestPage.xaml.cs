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
        private tehnEntities _context; // Контекст базы данных
        private ProductRequest _currentRequest; // Текущая редактируемая заявка
        private bool _isEditMode; // Флаг режима редактирования

        // Конструктор страницы добавления/редактирования заявки
        public AddEditRequestPage(ProductRequest request)
        {
            InitializeComponent();
            _context = Helper.GetContext(); // Получение контекста БД
            _currentRequest = request; // Сохранение переданной заявки
            _isEditMode = request != null; // Определение режима (редактирование/добавление)

            LoadComboBoxData(); // Загрузка данных в комбобоксы
            LoadRequestData(); // Загрузка данных заявки при редактировании
        }

        // Загрузка данных для комбобоксов (продукты и партнеры)
        private void LoadComboBoxData()
        {
            try
            {
                cbProduct.ItemsSource = _context.Product.ToList(); // Загрузка списка продуктов
                cbPartner.ItemsSource = _context.Partners.ToList(); // Загрузка списка партнеров
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}");
            }
        }

        // Загрузка данных заявки в форму при редактировании
        private void LoadRequestData()
        {
            if (_isEditMode && _currentRequest != null)
            {
                cbProduct.SelectedValue = _currentRequest.Product_id; // Установка продукта
                cbPartner.SelectedValue = _currentRequest.Partners_id; // Установка партнера
                txtCount.Text = _currentRequest.Count.ToString(); // Установка количества
            }
        }

        // Обработчик сохранения заявки
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Валидация выбора продукта
                if (cbProduct.SelectedItem == null)
                {
                    MessageBox.Show("Выберите продукт");
                    return;
                }

                // Валидация выбора партнера
                if (cbPartner.SelectedItem == null)
                {
                    MessageBox.Show("Выберите партнера");
                    return;
                }

                // Валидация ввода количества
                if (!int.TryParse(txtCount.Text, out int count) || count <= 0)
                {
                    MessageBox.Show("Введите корректное количество");
                    return;
                }

                // Обновление существующей заявки
                if (_isEditMode)
                {
                    _currentRequest.Product_id = (int)cbProduct.SelectedValue;
                    _currentRequest.Partners_id = (int)cbPartner.SelectedValue;
                    _currentRequest.Count = count;
                }
                else
                {
                    // Создание новой заявки
                    var newRequest = new ProductRequest
                    {
                        Product_id = (int)cbProduct.SelectedValue,
                        Partners_id = (int)cbPartner.SelectedValue,
                        Count = count
                    };
                    _context.ProductRequest.Add(newRequest); // Добавление в контекст
                }

                _context.SaveChanges(); // Сохранение изменений в БД
                MessageBox.Show("Данные успешно сохранены");
                NavigationService.Navigate(new CrudPage()); // Переход на страницу CRUD
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка сохранения: {ex.Message}");
            }
        }

        // Обработчик отмены - возврат на предыдущую страницу
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}