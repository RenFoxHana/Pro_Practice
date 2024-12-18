using Pro_Practice.Models;
using Pro_Practice.ViewModel;
using Pro_Practice.WorkWithInvoice;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace Pro_Practice.InvoicePages
{
    /// <summary>
    /// Логика взаимодействия для DocumentType.xaml
    /// </summary>
    public partial class DocumentTypes : Page
    {
        private BochagovaProPracticeContext _context = new BochagovaProPracticeContext();
        public ObservableCollection<DocumentType> ListDocumentTypes { get; set; }
        public DocumentTypes()
        {
            InitializeComponent();
            DocumentTypeVM viewModel = new DocumentTypeVM();
            DataContext = viewModel;
            ListDocumentTypes = new ObservableCollection<DocumentType>(_context.DocumentTypes.ToList());
            if (App.currentUser.IdRole != 1)
            {
                AddButton.Visibility = Visibility.Collapsed;
                EditButton.Visibility = Visibility.Collapsed;
            }
        }

        private DocumentType _selectedDocumentType;
        public DocumentType SelectedDocumentType
        {
            get { return _selectedDocumentType; }
            set
            {
                _selectedDocumentType = value;
            }
        }

        private void ListViewDocumentType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListViewDocumentTypes.SelectedItem is DocumentType selectedType)
            {
                SelectedDocumentType = selectedType;
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var workTypeDocumentWindow = new WorkDocumentType(new BochagovaProPracticeContext());
            workTypeDocumentWindow.ShowDialog();
            Refresh();

        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedDocumentType = SelectedDocumentType;
            var workTypeDocumentWindow = new WorkDocumentType(new BochagovaProPracticeContext(), selectedDocumentType);
            workTypeDocumentWindow.ShowDialog();
            Refresh();

        }


        public void Refresh()
        {
            DocumentTypeVM viewModel = (DocumentTypeVM)DataContext;
            viewModel.ListDocumentType.Clear();

            var documenttypes = _context.DocumentTypes.ToList();

            foreach (var documenttype in documenttypes)
            {
                viewModel.ListDocumentType.Add(documenttype);
            }
        }
    }
}
