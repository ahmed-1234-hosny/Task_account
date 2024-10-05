namespace Task_27_9__account
{
    using System;
    using System.Collections.Generic;

    public class Account
    {
        private string name;
        private double balance;

        public Account(string name = "Unnamed Account", double balance = 0.0)
        {
            this.name = name;
            this.balance = balance;
        }

        public bool Deposit(double amount)
        {
            if (amount < 0)
                return false;
            else
            {
                balance += amount;
                return true;
            }
        }

        public bool Withdraw(double amount)
        {
            if (balance - amount >= 0)
            {
                balance -= amount;
                return true;
            }
            else
            {
                return false;
            }
        }

        public double GetBalance()
        {
            return balance;
        }

        public override string ToString()
        {
            return $"{name}: {balance:C}";
        }

        public static Account operator +(Account acc1, Account acc2)
        {
            return new Account(acc1.name, acc1.balance + acc2.balance);
        }
    }

    public class SavingsAccount : Account
    {
        private double interestRate;

        public SavingsAccount(string name = "Unnamed Savings Account", double balance = 0.0, double interestRate = 0.0)
            : base(name, balance)
        {
            this.interestRate = interestRate;
        }

        public  bool Deposit(double amount)
        {
            if (base.Deposit(amount))
            {
                double interest = amount * (interestRate / 100);
                base.Deposit(interest);  
                return true;
            }
            return false;
        }
    }

    public class CheckingAccount : Account
    {
        private const double withdrawalFee = 1.50;

        public CheckingAccount(string name = "Unnamed Checking Account", double balance = 0.0)
            : base(name, balance) { }

        public  bool Withdraw(double amount)
        {
            amount += withdrawalFee;  
            return base.Withdraw(amount);
        }
    }

    public class TrustAccount : SavingsAccount
    {
        private int withdrawalCount = 0;
        private const int maxWithdrawals = 3;

        public TrustAccount(string name = "Unnamed Trust Account", double balance = 0.0, double interestRate = 0.0)
            : base(name, balance, interestRate) { }

        public  bool Deposit(double amount)
        {
            if (amount >= 5000)
                amount += 50;  
            return base.Deposit(amount);
        }

        public  bool Withdraw(double amount)
        {
            if (withdrawalCount < maxWithdrawals && amount <= GetBalance() * 0.2)
            {
                withdrawalCount++;
                return base.Withdraw(amount);
            }
            return false;
        }
    }

    public static class AccountUtil
    {
        public static void Display(List<Account> accounts)
        {
            Console.WriteLine("\n=== Accounts ==========================================");
            foreach (var acc in accounts)
            {
                Console.WriteLine(acc);
            }
        }

        public static void Deposit(List<Account> accounts, double amount)
        {
            Console.WriteLine("\n=== Depositing to Accounts =================================");
            foreach (var acc in accounts)
            {
                if (acc.Deposit(amount))
                    Console.WriteLine($"Deposited {amount:C} to {acc}");
                else
                    Console.WriteLine($"Failed Deposit of {amount:C} to {acc}");
            }
        }

        public static void Withdraw(List<Account> accounts, double amount)
        {
            Console.WriteLine("\n=== Withdrawing from Accounts ==============================");
            foreach (var acc in accounts)
            {
                if (acc.Withdraw(amount))
                    Console.WriteLine($"Withdrew {amount:C} from {acc}");
                else
                    Console.WriteLine($"Failed Withdrawal of {amount:C} from {acc}");
            }
        }
    }

    class Program
    {
        static void Main()
        {
            
              // Accounts
              var accounts = new List<Account>();
              new Account();
              new Account("Larry");
              new Account("Moe", 2000);
              new Account("Curly", 5000);

              AccountUtil.Display(accounts);
              AccountUtil.Deposit(accounts, 1000);
              AccountUtil.Withdraw(accounts, 2000);

              // Savings
              var savAccounts = new List<Account>();
              new SavingsAccount();
              new SavingsAccount("Superman");
              new SavingsAccount("Batman", 2000);
              new SavingsAccount("Wonderwoman", 5000, 5.0);

              AccountUtil.Display(savAccounts);
              AccountUtil.Deposit(savAccounts, 1000);
              AccountUtil.Withdraw(savAccounts, 2000);

              // Checking
              var checAccounts = new List<Account>();
              new CheckingAccount();
              new CheckingAccount("Larry2");
              new CheckingAccount("Moe2", 2000);
              new CheckingAccount("Curly2", 5000);

              AccountUtil.Display(checAccounts);
              AccountUtil.Deposit(checAccounts, 1000);
              AccountUtil.Withdraw(checAccounts, 2000);
              AccountUtil.Withdraw(checAccounts, 2000);

              // Trust
              var trustAccounts = new List<Account>();
              new TrustAccount();
              new TrustAccount("Superman2");
              new TrustAccount("Batman2", 2000);
              new TrustAccount("Wonderwoman2", 5000, 5.0);

              AccountUtil.Display(trustAccounts);
              AccountUtil.Deposit(trustAccounts, 1000);
              AccountUtil.Deposit(trustAccounts, 6000);
              AccountUtil.Withdraw(trustAccounts, 2000);
              AccountUtil.Withdraw(trustAccounts, 3000);
              AccountUtil.Withdraw(trustAccounts, 500);

              Console.WriteLine();
            
          
        }
    }
}