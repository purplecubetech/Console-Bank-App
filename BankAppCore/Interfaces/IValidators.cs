using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAppCore.Interfaces
{
    public interface IValidators
    {
        bool CheckName(string name);
        bool CheckEmail(string email);
        bool CheckPassword(string password);
    }
}
