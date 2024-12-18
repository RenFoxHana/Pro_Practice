using Pro_Practice.Models;
using System.Windows;

namespace Pro_Practice.WorkWithInvoice
{
    /// <summary>
    /// Логика взаимодействия для WorkTypeCargo.xaml
    /// </summary>
    public partial class WorkTypeCargo : Window
    {
        private readonly BochagovaProPracticeContext _context;
        private TypesOfCargo _typeCargo;
        public WorkTypeCargo(BochagovaProPracticeContext context, TypesOfCargo? typeCargo = null)
        {
            InitializeComponent();
            _context = context;
            _typeCargo = typeCargo;

            if (_typeCargo != null)
            {
                TextBoxNameTypeCargo.Text = _typeCargo.NameTypeCargo;
                SaveButton.Content = "ИЗМЕНИТЬ";
            }
        }

        private bool AreRequiredFieldsFilled()
        {
            if (string.IsNullOrWhiteSpace(TextBoxNameTypeCargo.Text))
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


            if (_typeCargo == null)
            {
                _typeCargo = new TypesOfCargo();
                _typeCargo.NameTypeCargo = TextBoxNameTypeCargo.Text;
                _context.TypesOfCargos.Add(_typeCargo);
            }
            else
            {
                _typeCargo.NameTypeCargo = TextBoxNameTypeCargo.Text;
                _context.TypesOfCargos.Update(_typeCargo);
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
