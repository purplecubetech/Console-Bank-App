using System;
using System.Collections.Generic;
using BankAppCore.Interfaces;
using BankAppCore.Models;

namespace BankAppCore.Implementations
{
    public class Bank : IBank
    {
               
        readonly List<Accounts> _bank;
        public Bank()
        {
            _bank = new List<Accounts>();
        }
        public UsersModel Login(List<UsersModel> users, string email, string password)
        {
            foreach (var item in users)
            {
                if (item.Email == email && item.Password == password)
                {
                    return item;
                }
            }

            return null;
        }

        public void ShowAccountUI(UsersModel user)
        {
            while (true)
            {
                Console.Write("1. Set up account\n2. Check balance\n3. Deposit\n4. Withdraw\n5. Account Details\n6. Account Statement\n7. Transfer\nX. LogOut\n\nSelect Function: ");
                string sel = Console.ReadLine().ToUpper();
                Console.WriteLine("\n");

                if (sel == "1")
                {
                    Console.Clear();
                    double init;
                    int accountType;
                    while (true)
                    {
                        Console.Write("Enter initial balance: ");
                        bool checkInit = double.TryParse(Console.ReadLine(), out init);
                        if (checkInit)
                        {
                            break;
                        }
                        Console.WriteLine("Invalid Amount");
                    }
                    while (true)
                    {
                        Console.Write("Choose Account Type 0 for Saving or 1 for Current: ");
                        bool checkType = int.TryParse(Console.ReadLine(), out accountType);
                        if (checkType)
                        {
                            break;
                        }
                        Console.WriteLine("Invalid account Type, type 0 or 1");
                    }

                    var createAcc = new Accounts(user.Id, init, accountType);
                    _bank.Add(createAcc);
                    Console.WriteLine($"Bank Account Added!\n Your Account Number is : {createAcc.Account.AccountNumber}");
                    Console.ReadLine();
                }
                else if (sel == "2")
                {
                    Console.Clear();
                    Console.Write("Enter your Account Number: ");
                    string nmChk = Console.ReadLine();
                    string response = "Account Not Found!!!";
                    for (int ix = 0; ix < _bank.Count; ix++)
                    {
                        if (_bank[ix].Account.AccountNumber == nmChk)
                        {
                            response = $"Account Found!\n Name: {_bank[ix].Account.AccountNumber }\n Balance: {_bank[ix].Account.Balance}";
                        }
                    }
                    Console.WriteLine(response);
                    Console.ReadLine();
                }
                else if (sel == "3")
                {
                    Console.Clear();
                    Console.Write("Enter Account Number: ");
                    string nmChk = Console.ReadLine();
                    string response = "Account not found!";
                    for (int ix = 0; ix < _bank.Count; ix++)
                    {
                        if (_bank[ix].Account.AccountNumber == nmChk)
                        {
                            if (user != null)
                            {
                                Console.Write($"Please Enter the Amount to Deposit to {user.FullName} account: ");
                                double amt = double.Parse(Console.ReadLine());
                                _bank[ix].Deposit(amt);
                                response = "Amount successfully deposited!";
                                _bank[ix].AddTranscations(amt, "Money deposited");

                            }



                        }
                    }
                    Console.WriteLine(response);
                    Console.ReadLine();
                }
                else if (sel == "4")
                {
                    Console.Clear();
                    Console.Write("Enter Account Number: ");
                    string nmChk = Console.ReadLine();
                    string response = "Account not found!";
                    for (int ix = 0; ix < _bank.Count; ix++)
                    {
                        if (_bank[ix].Account.AccountNumber == nmChk)
                        {
                            if (user != null)
                            {
                                Console.Write($"Please Enter the Amount to Withdraw:");
                                double amt = double.Parse(Console.ReadLine());
                                bool check = _bank[ix].Withdraw(amt, _bank[ix].Account.AccountType);
                                if (check)
                                {
                                    response = "Amount successfully Withdraw!";
                                    _bank[ix].AddTranscations(amt, "Money withdraw");
                                }
                                else
                                {
                                    response = "Insufficient funds";
                                }
                            }



                        }
                    }
                    Console.WriteLine(response);
                    Console.ReadLine();
                }
                else if (sel == "5")
                {
                    Console.Clear();
                    PrintAccountDetails(user);
                }

                else if (sel == "6")
                {
                    Console.Clear();
                    Console.Write("Enter your Account Number: ");
                    string nmChk = Console.ReadLine();
                    for (int ix = 0; ix < _bank.Count; ix++)
                    {
                        if (_bank[ix].Account.AccountNumber == nmChk)
                        {
                            _bank[ix].GetAllTransactionsForThisAccount(_bank[ix].Account.AccountNumber);
                        }
                    }
                    Console.ReadLine();
                }

                else if (sel == "7")
                {
                    Console.Clear();
                    Console.Write("Enter the Sender's Account Number : ");
                    string sender = Console.ReadLine();
                    Console.Write("Enter the Receiver's Account Number: ");
                    string receiver = Console.ReadLine();
                    double amt;
                    while (true)
                    {
                        Console.Write("Enter the Amount: ");
                        bool checkInit = double.TryParse(Console.ReadLine(), out amt);
                        if (checkInit)
                        {
                            break;
                        }
                        Console.WriteLine("Invalid Amount");
                    }
                    bool check = false;
                    for (int ix = 0; ix < _bank.Count; ix++)
                    {
                        if (_bank[ix].Account.AccountNumber == sender)
                        {
                            check = _bank[ix].Withdraw(amt, _bank[ix].Account.AccountType);
                            if (check)
                            {

                                _bank[ix].AddTranscations(amt, $"Send Money to {receiver}");
                            }

                        }

                    }

                    if (check)
                    {

                        for (int ix = 0; ix < _bank.Count; ix++)
                        {
                            if (_bank[ix].Account.AccountNumber == receiver)
                            {
                                _bank[ix].Deposit(amt);
                                _bank[ix].AddTranscations(amt, $"Recieved Money from {sender}");
                            }

                        }
                    }
                    else
                    {
                        Console.WriteLine("Insufficient funds");
                    }

                }
                else if (sel == "X")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid Syntax!");
                    Console.ReadLine();
                }

            }
        }


        private void PrintAccountDetails(UsersModel user)
        {
            string[] header = new string[] { "FULL NAME", "ACCOUNT NUMBER", "ACCOUNT TYPE", "BALANCE" };
            PrintTable printTable = new PrintTable(header);
            foreach (var item in _bank)
            {
                if (item.Account.UserId == user.Id)
                {
                    string[] row = new string[4];
                    row[0] = user.FullName;
                    row[1] = item.Account.AccountNumber;
                    row[2] = item.Account.AccountType;
                    row[3] = item.Account.Balance.ToString();
                    printTable.AddRow(row);
                }
            }
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            printTable.Print();
            Console.ResetColor();
        }
    }
}
