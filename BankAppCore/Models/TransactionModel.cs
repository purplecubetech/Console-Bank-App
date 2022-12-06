using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAppCore.Models
{
    public class TransactionModel
    {
        public string AccountNumber { get; set; }
        public string Description { get; set; }
        public string Amount { get; set; }
        public string Balance { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
    }
}
