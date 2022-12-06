using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAppCore.Interfaces
{
    public interface IPrintTable
    {
        void AddRow(object[] values);
        void Print();
    }
}
