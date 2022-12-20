using System;
using CsvHelper.Configuration.Attributes;

namespace PermissionManagement.MVC.Models
{
    public class DuesInformation
    {
        public int Id { get; set; }
        
        // [Index(0)]
        public string AccountCode { get; set; }
        // [Index(1)]
        public string Debt { get; set; }
        // [Index(2)]
        public string Credit { get; set; }
        // [Index(3)]
        public string BalanceDebt { get; set; }
        // [Index(4)]
        public string BalanceCredit { get; set; }
        public override string ToString()
        {
            return
                $"insert into DuesInformation(AccountCode,Debt,Credit,BalanceDebt,BalanceCredit) values ('{AccountCode}','{Debt}','{Credit}','{BalanceDebt}','{BalanceCredit}');";
        }
    }
}