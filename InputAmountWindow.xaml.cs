using System.Windows;

namespace BankingSystemGui;

public partial class InputAmountWindow : Window
{
    public decimal? Amount { get; private set; }
    public InputAmountWindow()
    {
        InitializeComponent();
    }

    private void Confirm_Click(object sender, RoutedEventArgs e)
    {
        if (decimal.TryParse(AmountTextBox.Text, out decimal value) && value > 0)
        {
            Amount = value;
            DialogResult = true;
        }
        else
        {
            MessageBox.Show("Please enter a valid positive amount.", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }
    public void SetPrompt(string prompt)
    {
        PromptText.Text = prompt;
    }
}