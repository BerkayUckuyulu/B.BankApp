using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Udemy.BankApp.Web.Models
{
    public class SendMoneyModel
    {
        public int SenderId { get; set; }
        public int AccountId { get; set; }
        public int Amount { get; set; }
    }
}
