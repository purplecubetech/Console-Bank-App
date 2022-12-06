using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankAppCore.Implementations;
using BankAppCore.Models;

namespace BankAppCore.Interfaces
{
    public interface IAccount
    {
        double getBal();
        void Deposit(double addAmt);
        bool Withdraw(double outAmt, string accType);
        bool AddTranscations(double amt, string description);
        void GetAllTransactionsForThisAccount(string accNum);
        


    }
}
