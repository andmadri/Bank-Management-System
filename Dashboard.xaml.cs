using System;
using System.Windows;

namespace BankingSystemGui;

//the dashboard should

public partial class Dashboard : Window
{
    private BankAccount account;
    public Dashboard(BankAccount account)
    {
        InitializeComponent();
        this.account = account;
        WelcomeText.Text = $"Welcome, {account.ClientName}";
        BalanceText.Text = $"Current Balance: €{account.Balance:F2}";
    }
    private void DepositButton_Click(object sender, RoutedEventArgs e)
    {
      var InputAmountWindow = new InputAmountWindow();
      InputAmountWindow.SetPrompt("Enter deposit amount in Euros: ");

      if (InputAmountWindow.ShowDialog() == true && InputAmountWindow.Amount.HasValue)
      {
        account.Deposit(InputAmountWindow.Amount.Value);
        BalanceText.Text = $"Current Balance: €{account.Balance:F2}";
        ActivityList.Items.Insert(0, $"Deposited €{InputAmountWindow.Amount.Value:F2} on {DateTime.Now:yyyy-MM-dd}");
      }
    }

    private void WidthrawButton_Click(object sender, RoutedEventArgs e)
    {
      var InputAmountWindow = new InputAmountWindow();
      InputAmountWindow.SetPrompt("Enter withdraw amount in Euros: ");
      if (InputAmountWindow.ShowDialog() == true && InputAmountWindow.Amount.HasValue)
      {
        if (account.Withdraw(InputAmountWindow.Amount.Value) == true) {
          BalanceText.Text = $"Current Balance: €{account.Balance:F2}";
          ActivityList.Items.Insert(0, $"Withdrew €{InputAmountWindow.Amount.Value:F2} on {DateTime.Now:yyyy-MM-dd}");
        }
        else
        {
          MessageBox.Show("Not enough money to withdraw", "Withdraw Fails", MessageBoxButton.OK, MessageBoxImage.Error);
        }
      }
    }

    private void LogoutButton_Click(object sender, RoutedEventArgs e)
    {
      MainWindow mainWindow = new MainWindow();
      mainWindow.Show();
      this.Close();
    }
}