using Pro_Practice.Models;
using System.Windows;

namespace Pro_Practice.WorkWithInvoice
{
    /// <summary>
    /// Логика взаимодействия для WorkDocumentType.xaml
    /// </summary>
    public partial class WorkDocumentType : Window
    {
        private readonly BochagovaProPracticeContext _context;
        private DocumentType _documentType;
        public WorkDocumentType(BochagovaProPracticeContext context, DocumentType? documentType = null)
        {
            InitializeComponent();
            _context = context;
            _documentType = documentType;

            if (_documentType != null)
            {
                TextBoxNameDocumentType.Text = _documentType.NameOfType;
                SaveButton.Content = "ИЗМЕНИТЬ";
            }
        }

        private bool AreRequiredFieldsFilled()
        {
            if (string.IsNullOrWhiteSpace(TextBoxNameDocumentType.Text))
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

            if (_documentType == null)
            {
                _documentType = new DocumentType();
                _documentType.NameOfType = TextBoxNameDocumentType.Text;
                _context.DocumentTypes.Add(_documentType);
            }
            else
            {
                _documentType.NameOfType = TextBoxNameDocumentType.Text;
                _context.DocumentTypes.Update(_documentType);
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
