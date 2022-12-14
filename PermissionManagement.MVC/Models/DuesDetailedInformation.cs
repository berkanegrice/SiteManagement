using System;
using CsvHelper.Configuration.Attributes;

namespace PermissionManagement.MVC.Models
{
    public class DuesDetailedInformation
    {
        public int Id { get; set; }
        public string AccountCode { get; set; }
        public DateTime Date { get; set; }
        // public string Date { get; set; }
        public string Detail { get; set; }
        public string Debt { get; set; }
        public string Credit { get; set; }
        public string BalanceDebt { get; set; }
        public string BalanceCredit { get; set; }
        
        public override string ToString()
        {
            return
                $"insert into DuesDetailedInformation(AccountCode,Date,Detail,Debt,Credit,BalanceDebt,BalanceCredit) values ('{AccountCode}','{Date}','{Detail}','{Debt}','{Credit}','{BalanceDebt}','{BalanceCredit}');";
        }
    }
}