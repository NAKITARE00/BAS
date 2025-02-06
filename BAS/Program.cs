using System;
using System.Linq;

public class BankAccount
{
    public string AccountNumber { get; set; }
    public string AccountHolderName { get; set; }
    private double Balance { get; set; }
    private string Pin { get; set; }  

    public BankAccount(string accountNumber, string accountHolderName, double initialBalance, string pin)
    {
        AccountNumber = accountNumber;
        AccountHolderName = accountHolderName;
        Balance = initialBalance;
        Pin = pin;
    }

    public void Deposit(double amount)
    {
        if (amount <= 0)
        {
            throw new Exception("Deposit amount must be positive.");
        }
        Balance += amount;
    }

    public void Withdraw(double amount)
    {
        if (amount <= 0)
        {
            throw new Exception("Withdrawal amount must be positive.");
        }
        if (amount > Balance)
        {
            throw new Exception("Insufficient funds.");
        }
        Balance -= amount;
    }

    public void DisplayBalance()
    {
        Console.WriteLine($"Account Balance: ${Balance}");
    }

    // Method to validate the PIN
    public bool ValidatePin(string pin)
    {
        return Pin == pin;
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Creating a list of accounts with PINs (AccountNumber, AccountHolderName, InitialBalance, Pin)
        var accountDatabase = new[]
        {
            new BankAccount("123456", "John Doe", 1000, "1234"),
            new BankAccount("789101", "Jane Smith", 1500, "5678"),
            new BankAccount("112233", "Alice Johnson", 2000, "9876")
        };

        // Ask the user to enter an account number
        Console.Write("Enter account number: ");
        string accountNumber = Console.ReadLine();

        // Find if the account exists in the "database"
        BankAccount account = accountDatabase.FirstOrDefault(a => a.AccountNumber == accountNumber);

        if (account != null)
        {
            // Ask the user to enter the PIN
            Console.Write("Enter your 4-digit PIN: ");
            string pin = Console.ReadLine();

            // Validate the entered PIN
            if (account.ValidatePin(pin))
            {
                Console.WriteLine($"Welcome {account.AccountHolderName}");
                account.DisplayBalance();

                // Example of interacting with the account
                Console.WriteLine("Would you like to make a deposit? (y/n)");
                if (Console.ReadLine()?.ToLower() == "y")
                {
                    Console.Write("Enter deposit amount: ");
                    double depositAmount = Convert.ToDouble(Console.ReadLine());
                    account.Deposit(depositAmount);
                    account.DisplayBalance();
                }
            }
            else
            {
                Console.WriteLine("Incorrect PIN. Access denied.");
            }
        }
        else
        {
            Console.WriteLine("Account not found.");
        }
    }
}
