using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankAppCore.Interfaces;
using BankAppCore.Models;

namespace BankAppUI
{
    public class BankDisplay
    {
        private readonly IBank _bank;
        private readonly IValidators _validators;
        readonly List<UsersModel> users = new();

        public BankDisplay(IBank bank, IValidators validators)
        {
            _bank = bank;
            _validators = validators;
        }

        public void ShowBankDisplay()
        {
            while (true)
            {

                Console.Write("1. Set up user account\n2. Login\n3. Exit \n\nSelect Function: ");
                string selGo = Console.ReadLine().ToUpper();
                Console.WriteLine("\n");
                if (selGo == "1")
                {
                    string fullName;
                    string email;
                    string password;
                    while (true)
                    {
                        Console.WriteLine("Please Enter Your Full Name: ");
                        fullName = Console.ReadLine();
                        string[] names = fullName.Split();
                        if (_validators.CheckName(names[0]) && _validators.CheckName(names[1]))
                        {
                            break;
                        }
                        Console.WriteLine("Please input the right format\n Names should start with capital letter, Like Boss");
                    }

                    while (true)
                    {
                        Console.WriteLine("Please Enter Your Email: ");
                        email = Console.ReadLine();
                        if (_validators.CheckEmail(email))
                        {
                            break;
                        }
                        Console.WriteLine("Please input the right email format");
                    }
                    while (true)
                    {
                        Console.WriteLine("Please Enter Your Password: ");
                        password = Console.ReadLine();
                        if (_validators.CheckPassword(password))
                        {
                            break;
                        }
                        Console.WriteLine("Please input the right format\n Password should minimum of 6 characters including special character");
                    }


                    try
                    {
                        UsersModel person = new() 
                        { 
                            FullName = fullName,
                            Email = email,
                            Password = password
                        };
                        users.Add(person);
                        Console.WriteLine("Account has been successfull created !!!\n Press 2 to Login");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }



                }
                else if (selGo == "2")
                {
                    string email;
                    string password;
                    while (true)
                    {
                        Console.WriteLine("Please Enter Your Email: ");
                        email = Console.ReadLine();
                        if (_validators.CheckEmail(email))
                        {
                            break;
                        }
                        Console.WriteLine("Please input the right email format");
                    }
                    while (true)
                    {
                        Console.WriteLine("Please Enter Your Password: ");
                        password = Console.ReadLine();
                        if (_validators.CheckPassword(password))
                        {
                            break;
                        }
                        Console.WriteLine("Please input the right format\n Password should minimum of 6 characters including special character");
                    }
                    UsersModel login = _bank.Login(users, email, password);
                    if (login != null)
                    {
                        Console.Clear();
                        Console.WriteLine("Welcome to PIGGY BANK APP");                        
                        _bank.ShowAccountUI(login);


                    }
                    else
                    {
                        Console.WriteLine("Please Invalid Credentials, Press 2 to try again");
                        Console.ReadLine();
                    }
                }

                else if (selGo == "3")
                {
                    break;
                }

                else
                {
                    Console.WriteLine("Invalid Syntax!");
                    Console.ReadLine();
                }
                Console.Clear();
            }
        }
    }
}
