#nullable disable

using System.ComponentModel.DataAnnotations;

namespace DemoPET3.Repository.Models
{
    public partial class AccountUser
    {
        [Key]
        public string UserId { get; set; }
        public string UserPassword { get; set; }
        public string UserFullName { get; set; }
        public int? UserRole { get; set; }
    }
}
