#nullable disable

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DemoPET3.Repository.Models
{
    public partial class Publisher
    {
        public Publisher()
        {
            Books = new HashSet<Book>();
        }
        [Key]
        [Required]
        public string PublisherId { get; set; }
        [Required]
        public string PublisherName { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}
