using DemoPET3.Repository.Models;
using DemoPET3.Repository.Repositories;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DemoPET3.WebApp.Pages.Books
{
    public class DetailsModel : PageModel
    {
        private readonly IRepository<Book> _repository;

        public DetailsModel(IRepository<Book> repository)
        {
            _repository = repository;
        }

        public Book Book { get; set; }
        public string ErrorMessage { get; set; }

        public void OnGet(string id)
        {
            if (id == null)
            {
                ErrorMessage = "Please provide a valid Book ID";
            }

            Book = _repository.GetById(id);

            if (Book == null)
            {
                ErrorMessage = $"Book with ID {id} not found";
            }
        }
    }
}
