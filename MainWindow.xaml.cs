using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BankingSystemGui;
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }
    private void OpenLoginWindoClick(object sender, RoutedEventArgs e)
    {
        var loginWindow = new LoginWindow(this);
        loginWindow.ShowDialog();
    }
    private void OpenRegisterWindowClick(object sender, RoutedEventArgs e)
    {
        var registerWindow = new RegisterWindow(this);
        registerWindow.ShowDialog();
    }
}