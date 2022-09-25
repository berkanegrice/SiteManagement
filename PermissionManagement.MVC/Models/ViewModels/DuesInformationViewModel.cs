namespace PermissionManagement.MVC.Models.ViewModels
{
    public class DuesInformationViewModel
    {
        public int Id { get; set; }
        public string LeaseHolder { get; set; }
        public string Debt { get; set; }
        public string Credit { get; set; }
        public string BalanceDebt { get; set; }
        public string BalanceCredit { get; set; }
    }
}