using DemoPET3.Repository.Models;
using DemoPET3.Repository.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DemoPET3.WebApp.Pages.Books
{
    public class DeleteModel : PageModel
    {
        private readonly IRepository<Book> _repository;

        public DeleteModel(IRepository<Book> repository)
        {
            _repository = repository;
        }

        [BindProperty] public Book Book { get; set; }
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

        public IActionResult OnPostAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            _repository.Delete(id);

            return RedirectToPage("./Index");
        }
    }
}