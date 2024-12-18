using Pro_Practice.Models;
using System.Windows;

namespace Pro_Practice
{
    /// <summary>
    /// Логика взаимодействия для Authorization.xaml
    /// </summary>
    public partial class Authorization : Window
    {
        private BochagovaProPracticeContext _db = new BochagovaProPracticeContext();

        public Authorization()
        {
            InitializeComponent();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            List<Employee> employees = _db.Employees.ToList();

            string login = txtLogin.Text;
            string password = txtPassword.Password;
            bool isAuthenticated = false;

            foreach (Employee employee in employees)
            {
                if (employee.Login == login && employee.Password == password)
                {
                    App.currentUser = employee;
                    MainWindow mainWindow = new MainWindow();
                    mainWindow.Show();
                    Close();
                    isAuthenticated = true;
                    break;
                }
            }

            if (!isAuthenticated)
            {
                MessageBox.Show("Неверный логин или пароль");
            }
        }
    }
}