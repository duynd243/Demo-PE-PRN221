using DemoPET3.Repository.Models;
using DemoPET3.Repository.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DemoPET3.WebApp.Pages.Books
{
    public class EditModel : PageModel
    {
        private readonly IRepository<Book> _bookRepository;
        private readonly IRepository<Publisher> _publisherRepository;
        private readonly SelectList _publisherDropdownOptions;

        public EditModel(IRepository<Book> bookRepository, IRepository<Publisher> publisherRepository)
        {
            _bookRepository = bookRepository;
            _publisherRepository = publisherRepository;
            _publisherDropdownOptions = new SelectList(
                _publisherRepository.GetAll()
                , "PublisherId",
                "PublisherName"
            );
        }

        [BindProperty]
        public Book Book { get; set; }
        public string ErrorMessage { get; set; }

        public void OnGet(string id)
        {
            ViewData["Publishers"] = _publisherDropdownOptions;
            if (id == null)
            {
                ErrorMessage = "Please provide a valid Book ID";
            }

            Book = _bookRepository.GetById(id);

            if (Book == null)
            {
                ErrorMessage = $"Book with ID {id} not found";
            }
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _bookRepository.Update(Book);

            return RedirectToPage("./Index");
        }
    }
}
