# Bank-Management-System
A simple yet functional **Bank Management System** built with **C# (.NET WPF)** and **SQLite**, designed to manage customers, their accounts, and basic transactions such as **deposits**, **withdrawals**, and **transfers**.
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

## Login Window
All Window classes in this repository inherit from `Window` which is class that provides functionalities to display a window, handle events like user input and interacts with the operating system like maximizing or closing the window. The Login window has two `Boxes` where the user input is going to be extracted from: 
```xaml
<TextBox Name="UsernameTextBox" Height="30" Margin="0,0,0,15"/>
<PasswordBox Name="PasswordBox" Height="30" Margin="0,0,0,25"/>
```
In the code, after the user has written their credentials and pressed on the login button, these **controls** will be used to extract the username and password in this way:





    
