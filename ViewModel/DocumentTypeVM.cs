using Pro_Practice.Models;
using System.Collections.ObjectModel;

namespace Pro_Practice.ViewModel
{
    internal class DocumentTypeVM
    {
        private BochagovaProPracticeContext _context = new BochagovaProPracticeContext();
        public ObservableCollection<DocumentType> ListDocumentType { get; set; }

        public DocumentTypeVM()
        {
            ListDocumentType = new ObservableCollection<DocumentType>(_context.DocumentTypes.ToList());
        }
    }
}
