namespace OOPsFundamental.Encapsulation;

public class BankAccount
{
    private decimal _balance = 0;

    private BankAccount(decimal balance)
    {
        Deposit(balance);
    }

    public static BankAccount CreateAccount(decimal balance)
    {
        return new BankAccount(balance);
    }

    public decimal GetBalance()
    {
        return _balance;
    }

    public void Deposit(decimal amount)
    {
        if (amount <= 0)
        {
            throw new ArgumentException("Amount must be greater than zero");
        }

        _balance += amount;
        Console.WriteLine($"INR {amount} Deposit Successful");
    }

    public void Withdraw(decimal amount)
    {
        if (amount <= 0)
        {
            throw new ArgumentException("Amount must be greater than zero");
        }

        if (amount > _balance)
        {
            throw new InvalidOperationException("Insufficient funds");
        }

        _balance -= amount;
        Console.WriteLine($"INR {amount} Withdraw Successful");
    }
}
