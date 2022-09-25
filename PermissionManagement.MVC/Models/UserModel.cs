using CsvHelper.Configuration.Attributes;

namespace PermissionManagement.MVC.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string AccountCode { get; set; }
        public string AccountName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
    }
}