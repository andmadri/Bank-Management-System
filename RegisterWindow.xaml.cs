using System;

using System.Windows;

namespace BankingSystemGui;

public partial class RegisterWindow : Window
{
    private MainWindow _mainWindow;
    public RegisterWindow(MainWindow mainWindow)
    {
        InitializeComponent();
        _mainWindow = mainWindow;
    }
    public void RegisterButton_Click(object send, RoutedEventArgs e)
    {
        string username = UsernameTextBox.Text.Trim();
        string password = PasswordBox.Password;
        if(string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
        {
            MessageBox.Show("Please enter a valid username and password.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }
        if (Database.CheckAccountExist(username, password))
        {
            MessageBox.Show("An account with this username already exists.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        Database.AddAccount(username, password, 0);
        MessageBox.Show("Account successfully created!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        var loginWindow = new LoginWindow(_mainWindow);
        loginWindow.Show();
        this.Close();
    }
}