# Bank-Management-System

https://github.com/user-attachments/assets/69fec95b-fa6f-40c1-b863-733539c4b406

A simple yet functional **Bank Management System** built with **C# (.NET WPF)** and **SQLite**, designed to manage customers, their accounts, and basic transactions such as **deposits** or **withdrawals**.
The technologies used in this project are:
- C# (.NET)
- WPF (XAML for UI)
- SQLite (with `Microsoft.Data.Sqlite`)


## Build Instructions
To build and run the Bank Management System unfortunately you would need a **Windows** machine (because WPF is Windows-only). Moreover you need at least **.NET 6.0 SDK** or later version.
```PowerShell
git clone https://github.com/andmadri/Bank-Management-System.git Bank-Management-System
cd Bank-Management-System
dotnet build
dotnet run
```
## What is XAML?
XAML (Extensible Application Markup Language) is a declarative language used to define the structure, layout and appearance of user interfaces in WPF (Windows Presentation Foundation) applications. It is similar to HTML, but it is specifically used for defining the UI in .NET applications together with C#, which provides the back-end logic for the program.

## Main Window
![image](https://github.com/user-attachments/assets/3e74d940-07fd-4772-9486-55b8f213c62a)

The landing page offers two options either login or register in case of a new user. The code is located in files `MainWindow.xaml` and `MainWindow.xaml.cs`.
Most of the xaml files in this repository will start like this:
```xaml
<Window x:Class="BankingSystemGui.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
        xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
        xmlns:local="clr-namespace:BankingSystemGui"
        mc:Ignorable="d"
        Title="Bank Management System" 
        Height="450" 
        Width="800"
        Background="#045e78">
```
This creates a Window element, where `x:Class="BankingSystemGui.MainWindow` associates the XAML with the MainWindow class in C#. The `xmlns` declaration defines namespaces that are required for XAML to function. The `Title`, `Height`, `Width`, and `Background` properties define the window's title, size, and background color.

Some recurrent elements in all XAML files are:
- `<Grid>`: used to organize the UI elements inside the window.
```xaml
<StackPanel HorizontalAlignment="Center" VerticalAlignment="Center">
```
- `<StackPanel>`: arranges child elements (like buttons and text) vertically by default, but in this case it centers the elements both horizontally and vertically.
```xaml
<TextBlock Text="Welcome to VLK Bank" FontSize="35" FontFamily="Georgia" FontWeight="Bold" Foreground="White" HorizontalAlignment="Center"/>
```
- `<TextBlock>`: display the a message where you can specify the font size, font family, text color, etc.
```xaml
<Button Content="Login" Width="200" Height="50" FontFamily="Segoe UI" FontWeight="Bold" Foreground="White" Background="#00abab" Margin="0,10" Click="OpenLoginWindoClick"/>
<Button Content="Register" Width="200" Height="50" FontFamily="Segoe UI" FontWeight="Bold" Foreground="White" Background="#00abab" Margin="0,10" Click="OpenRegisterWindowClick"/>
```
- `<Button>`: similar to text blocks however they can handle **events**! The `Click` event handlers (`OpenLoginWindoClick` and `OpenRegisterWindowClick`) link these buttons to methods in C# code that are executed when the buttons are clicked.
These methods will create a new instance of `LoginWindow` and `RegisterWindow` respectively. As you can see both functions have a `ShowDialog()`. This function will open a login/register window in **modal state**, which means that the user won't be able to interact with the main landing page until they close the login/register window.
  
```c#
private void OpenLoginWindoClick(object sender, RoutedEventArgs e)
{
var loginWindow = new LoginWindow();
loginWindow.ShowDialog();
}
private void OpenRegisterWindowClick(object sender, RoutedEventArgs e)
{
var registerWindow = new RegisterWindow();
registerWindow.ShowDialog();
}
```

## Login 
![image](https://github.com/user-attachments/assets/7bb34e62-8b4a-4a53-8193-5a37e68738a3)

All Window classes in this repository inherit from `Window` which is class that provides functionalities to display a window, handle events like user input and interacts with the operating system like maximizing or closing the window. The Login window has two `Boxes` where the user input is going to be extracted from: 
```xaml
<TextBox Name="UsernameTextBox" Height="30" Margin="0,0,0,15"/>
<PasswordBox Name="PasswordBox" Height="30" Margin="0,0,0,25"/>
```
In the code, after the user has written their credentials and pressed on the login button, these **controls** will be used to extract the username and password in this way:
```c#
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
```
If `CheckAccountExists()` after taking the username and password extracted from the textbox returns true, that means that the client exists and we can create a new instance of BankAccount and a Dashboard, which is going to be the page where we can see the current balance of the account, withdraw or deposit money.
The line `dashboard.Show()` will show the window for the dashboard but not after we close the login window as well as the main landing page to keep our desktop visually clean.
In case of the client typing a username or password that does not exist in our database, the error is handle through a MessageBox that simple explains the type of error.
![image](https://github.com/user-attachments/assets/5e888142-f577-40c1-aa41-3aaddc4a8f2c)

## Dashboard
![image](https://github.com/user-attachments/assets/f046b849-8402-4bdd-b6f2-1bcda3f4c003)
In the dashboard the client can see recent transactions such as withdrawls and deposits. The client can also see their current balance and have the possibility to logout.
The functions to deposit and withdraw are fairly easy and are triggered when the buttons are pressed:
```c#
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
```
When depositing or withdrawing money, a new InputAmountWindow is created. This window receives the event type (either deposit or withdraw) so it knows how to handle the user's action. The user is prompted to enter a positive number, which is first stored in a local variable and then assigned to the public Amount property of the InputAmountWindow. Once the input is confirmed, DialogResult is set to true. This ensures that money is only deposited (or withdrawn), the account balance is updated, and a transaction record is created if the input was valid. WithdrawButton_Click has an exact same structure as DepositButton_Click but checks if the amount that the client wants to withdraw from the account is not more than the balance of the account itself.
```c#
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
```
Error handling after trying to withdraw more than my current balance:

<img width="500" alt="image" src="https://github.com/user-attachments/assets/b16ddc16-8cb9-4260-9f37-0b17c6af5431" />


Error handling when inputing a non positive number:

![image](https://github.com/user-attachments/assets/bb4715dd-cac5-4e55-a68b-721d0c21e958) ![image](https://github.com/user-attachments/assets/e64c2944-2772-429c-ac16-372dfa395f94)

## Database
To manage the local SQlite Account there is the file called `Database.cs` which contains a static class that handles operations for the bank management system. It provides methods to store user accounts, authenticate users, and manages account balances.
```c#
public static void Initialize()
{
using var connection = new SqliteConnection(connectionString);
connection.Open();

var tableCmd = connection.CreateCommand();
tableCmd.CommandText = @"
    CREATE TABLE IF NOT EXISTS Accounts (
        Id INTEGER PRIMARY KEY AUTOINCREMENT,
        ClientName TEXT NOT NULL,
        Password TEXT NOT NULL,
        Balance REAL NOT NULL
    );";
tableCmd.ExecuteNonQuery();
}
```
`Initialize()` creates a local bank.db file represented by connectionString and sets up the Accounts table with columns for client name, hashed password and account balance. The password is hashed using SHA256 before it is stored, ensuring that it is more secure than storing plain text.

The functions inside this class follow a similar structure. To be able to interact with the database, we need to create an object of SqliteConnection, after opening the connection, we can create a command that will be executed at the end of the function through `command.ExecuteNonQuery()`. For instance in `AddAccount()` commands from sql are used to interact with the database, and we add values that get passed to the function to the commands through `command.Parameters.AddWithValue()`.
```c#
public static void AddAccount(string clientName, string password, decimal initialBalance)
{
using var connection = new SqliteConnection(connectionString);
connection.Open();

var insertCmd = connection.CreateCommand();
insertCmd.CommandText = @"
INSERT INTO Accounts (ClientName, Password, Balance)
VALUES ($clientName, $password, $balance);";
insertCmd.Parameters.AddWithValue("$clientName", clientName);
insertCmd.Parameters.AddWithValue("$password", HashPassword(password));
insertCmd.Parameters.AddWithValue("$balance", initialBalance);
insertCmd.ExecuteNonQuery();
}
```
## Final Thoughts
This Bank Management System was built as a learning project to explore the C# language, WPF for desktop application development, and SQLite for lightweight database storage. It implements essential banking operations such as registration, login, balance tracking, deposits, and withdrawals—all using a clean and modern UI.

While simple in scope, it demonstrates core concepts of desktop app development including:
- GUI event handling
- Secure password hashing
- Modal window interactions
- Data validation and error handling
- Local storage with SQLite

    
