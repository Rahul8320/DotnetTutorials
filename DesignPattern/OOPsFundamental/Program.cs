using OOPsFundamental.Encapsulation;

// Test cases
try
{
    BankAccount testAccount = BankAccount.CreateAccount(0);
    testAccount.Deposit(100);
    testAccount.Withdraw(50);
    testAccount.Withdraw(60);
}
catch (ArgumentException ex)
{
    Console.WriteLine(ex.Message);
}

try
{
    BankAccount testAccount = BankAccount.CreateAccount(100);
    testAccount.Withdraw(150);
}
catch (InvalidOperationException ex)
{
    Console.WriteLine(ex.Message);
}

try
{
    BankAccount testAccount = BankAccount.CreateAccount(-100);
}
catch (ArgumentException ex)
{
    Console.WriteLine(ex.Message);
}


