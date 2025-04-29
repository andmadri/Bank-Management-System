using System.Windows;
using System.Windows.Controls;

namespace BankingSystemGui
{
    public partial class LoginWindow : Window
    {
        private MainWindow _mainWindow;
        public LoginWindow(MainWindow mainWindow)
        {
            InitializeComponent();
            _mainWindow = mainWindow;
        }
        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;
            string password = PasswordBox.Password;
            if (Database.CheckAccountExist(username, password))
            {
               decimal? balance = Database.GetBalance(username);
               if (balance != null)
               {
                var account = new BankAccount(username, balance.Value);
                var dashboard = new Dashboard(account);
                dashboard.Show();
                _mainWindow.Close();
                this.Close();
               }
               else
               {
                _mainWindow.Close();
                MessageBox.Show("Platform Error", "Bad Balance", MessageBoxButton.OK, MessageBoxImage.Error);
                this.Close();
               }
            }
            else 
            {
                MessageBox.Show("Invalid credentials", "Login Failes", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}