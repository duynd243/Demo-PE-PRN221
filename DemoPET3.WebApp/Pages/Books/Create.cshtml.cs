using DemoPET3.Repository.Models;
using DemoPET3.Repository.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DemoPET3.WebApp.Pages.Books
{
    public class CreateModel : PageModel
    {
        private readonly IRepository<Book> _bookRepository;
        private readonly IRepository<Publisher> _publisherRepository;
        private readonly SelectList _publisherDropdownOptions;

        public CreateModel(IRepository<Book> bookRepository, IRepository<Publisher> publisherRepository)
        {
            _bookRepository = bookRepository;
            _publisherRepository = publisherRepository;
            _publisherDropdownOptions = new SelectList(
                _publisherRepository.GetAll()
                , "PublisherId",
                "PublisherName"
            );
        }

        public IActionResult OnGet()
        {
            ViewData["Publishers"] = _publisherDropdownOptions;
            return Page();
        }

        [BindProperty] public Book Book { get; set; }
        public string ErrorMessage { get; set; }

        public IActionResult OnPost()
        {
            ViewData["Publishers"] = _publisherDropdownOptions;
            if (!ModelState.IsValid)
            {
                return Page();
            }
            
            var book = _bookRepository.GetById(Book.BookId);

            // Book existed in DB -> Not insert
            if (book != null)
            {
                ErrorMessage = "Book with this ID was existed";
                return Page();
            }
            
            // Not found in DB -> Insert...
            _bookRepository.Add(Book);

            return RedirectToPage("/Books/Index");
        }
    }
}