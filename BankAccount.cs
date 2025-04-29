public class BankAccount
{
    public string ClientName { get; set; }

    public decimal Balance { get; private set; }

    public BankAccount(string clientName, decimal balance)
    {
        ClientName = clientName;
        Balance = balance;
    }

    public void Deposit(decimal amount)
    {
        Balance += amount;
        Database.UpdateBalance(ClientName, Balance);
    }

    public bool Withdraw(decimal amount)
    {
        if (amount <= Balance)
        {
            Balance -= amount;
            Database.UpdateBalance(ClientName, Balance);
            return true;
        }
        return false;
    }
}