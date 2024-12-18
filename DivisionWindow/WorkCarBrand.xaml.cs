using Pro_Practice.Models;
using Pro_Practice.ViewModel;
using System.Collections.ObjectModel;
using System.Windows;

namespace Pro_Practice.DivisionWindow
{
    /// <summary>
    /// Логика взаимодействия для WorkCarBrand.xaml
    /// </summary>
    public partial class WorkCarBrand : Window
    {
        private readonly BochagovaProPracticeContext _context;
        private CarBrand _carBrand;
        public WorkCarBrand(BochagovaProPracticeContext context, CarBrand? carBrand = null)
        {
            InitializeComponent();
            _context = context;
            _carBrand = carBrand;

            if (_carBrand != null)
            {
                TextBoxNameBrand.Text = _carBrand.NameBrand;
                SaveButton.Content = "ИЗМЕНИТЬ";
            }
        }

        private bool AreRequiredFieldsFilled()
        {
            if (string.IsNullOrWhiteSpace(TextBoxNameBrand.Text))
            {
                MessageBox.Show("Поле 'Наименование' обязательно для заполнения.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            return true;
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            if (!AreRequiredFieldsFilled())
            {
                return;
            }

            if (_carBrand == null)
            {
                _carBrand = new CarBrand();
                _carBrand.NameBrand = TextBoxNameBrand.Text;
                _context.CarBrands.Add(_carBrand);
            }
            else
            {
                _carBrand.NameBrand = TextBoxNameBrand.Text;
                _context.CarBrands.Update(_carBrand);
            }

            _context.SaveChanges();
            Close();

        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
