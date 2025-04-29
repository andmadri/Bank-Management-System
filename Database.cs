using System.Security.Cryptography;
using System.Text;
using System.Windows.Controls.Primitives;
using Microsoft.Data.Sqlite;

public static class Database
{
    private const string connectionString = "Data Source=bank.db";

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

    public static string HashPassword(string password)
    {
        using var sha = SHA256.Create();
        var bytes = Encoding.UTF8.GetBytes(password);
        var hash = sha.ComputeHash(bytes);
        return Convert.ToBase64String(hash);
    }
    
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

    public static bool CheckAccountExist(string clientName, string password)
    {
        using var connection = new SqliteConnection(connectionString);
        connection.Open();

        var selectCmd = connection.CreateCommand();
        selectCmd.CommandText = @"
        SELECT Password FROM Accounts WHERE ClientName = $clientName LIMIT 1;";
        selectCmd.Parameters.AddWithValue("$clientName", clientName);
        var result = selectCmd.ExecuteScalar() as string;
        if (result == null) return false;
        return result == HashPassword(password);
    }

    public static decimal ? GetBalance(string clientName)
    {
        using var connection = new SqliteConnection(connectionString);
        connection.Open();
        
        var selectCmd = connection.CreateCommand();
        selectCmd.CommandText = @"
            SELECT Balance FROM Accounts WHERE ClientName = $clientName;";
        selectCmd.Parameters.AddWithValue("$clientName", clientName);
        var result = selectCmd.ExecuteScalar();
        return result != null ? (decimal)(double)result : (decimal?)null; 
    }

    public static void UpdateBalance(string clientName, decimal newBalance)
    {
        using var connection = new SqliteConnection(connectionString);
        connection.Open();

        var updateCmd = connection.CreateCommand();
        updateCmd.CommandText = @"
            UPDATE Accounts
            SET Balance = $newbalance
            WHERE ClientName = $clientname;";
        updateCmd.Parameters.AddWithValue("$newbalance", newBalance);
        updateCmd.Parameters.AddWithValue("$clientname", clientName);
        updateCmd.ExecuteNonQuery();
    }
}