using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAppCore.Models
{
    public class AccountModel
    {
        
        public string AccountNumber { get; set; }
        public double Balance { get; set; }
        public string AccountType { get; set; }
        public string UserId { get; set; }
        public List<TransactionModel> TransactionsList { get; set; }
    }
}
