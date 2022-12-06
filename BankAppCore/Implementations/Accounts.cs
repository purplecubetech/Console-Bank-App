using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankAppCore.Interfaces;
using BankAppCore.Models;

namespace BankAppCore.Implementations
{
    public class Accounts : IAccount
    {

        public readonly AccountModel Account = new ();     


        public Accounts (string id, double firstDep, int accountType)
        {
            Account.Balance += firstDep;
            Account.AccountNumber = RandomDigits(10);
            Account.AccountType = accountType == 0 ? "Saving" : "Current";
            Account.UserId = id;
            Account.TransactionsList = new List<TransactionModel>();         
                      
        }

        public bool AddTranscations(double amt, string description)
        {
            TransactionModel trans = new()
            { 
                AccountNumber = Account.AccountNumber,
                Description = description,
                Amount = amt.ToString(),
                Balance = Account.Balance.ToString()
            };
            if (trans != null)
            {
                Account.TransactionsList.Add(trans);
                return true;
            }
            return false;
        }

        public void Deposit(double addAmt)
        {
            Account.Balance += addAmt;
        }

        public void GetAllTransactionsForThisAccount(string accNum)
        {
            string[] header = new string[] { "DATE", "DESCRIPTION", "AMOUNT", "BALANCE" };
            PrintTable printTable = new (header);
            foreach (var item in Account.TransactionsList)
            {
                if (item.AccountNumber == accNum)
                {
                    string[] row = new string[4];
                    row[0] = item.Date.ToString();
                    row[1] = item.Description;
                    row[2] = item.Amount;
                    row[3] = item.Balance;
                    printTable.AddRow(row);
                }
            }


            Console.ForegroundColor = ConsoleColor.DarkGreen;
            printTable.Print();
            Console.ResetColor();
        }

        public double getBal()
        {
            return Account.Balance;
        }

        public bool Withdraw(double outAmt, string accType)
        {
            double minBalance = accType == "Saving" ? 1000.0 : 0.0;
            bool chk = true;

            if (outAmt <= Account.Balance - minBalance)
            {
                Account.Balance -= outAmt;
            }
            else if (outAmt > Account.Balance - minBalance)
            {
                chk = false;
            }
            return chk;
        }

        private static string RandomDigits(int length)
        {
            var random = new Random();
            string s = string.Empty;
            for (int i = 0; i < length; i++)
                s = String.Concat(s, random.Next(10).ToString());
            return s;
        }
    }
}
