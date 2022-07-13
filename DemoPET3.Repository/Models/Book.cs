#nullable disable

using System.ComponentModel.DataAnnotations;

namespace DemoPET3.Repository.Models
{
    public partial class Book
    {
        [Required]
        [StringLength(12), MinLength(6)]
        public string BookId { get; set; }
        public string BookName { get; set; }
        public int? Quantity { get; set; }
        [StringLength(150), MinLength(11)]
        public string AuthorName { get; set; }
        public string PublisherId { get; set; }

        public virtual Publisher Publisher { get; set; }
    }
}
