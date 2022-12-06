using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankAppCore.Models;

namespace BankAppCore.Interfaces
{
    public interface IBank
    {
        void ShowAccountUI(UsersModel user);

        UsersModel Login(List<UsersModel> users, string email, string password);

       
    }
}
