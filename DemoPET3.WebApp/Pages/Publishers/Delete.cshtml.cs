using DemoPET3.Repository.Models;
using DemoPET3.Repository.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DemoPET3.WebApp.Pages.Publishers
{
    public class DeleteModel : PageModel
    {
        private readonly IRepository<Publisher> _repository;

        public DeleteModel(IRepository<Publisher> repository)
        {
            _repository = repository;
        }

        [BindProperty] public Publisher Publisher { get; set; }
        public string ErrorMessage { get; set; }
        public void OnGet(string id)
        {
            if (id == null)
            {
                ErrorMessage = "Please provide a valid Publisher ID";
            }

            Publisher = _repository.GetById(id);

            if (Publisher == null)
            {
                ErrorMessage = $"Publisher with ID {id} not found";
            }
        }

        public IActionResult OnPost(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var publisher = _repository.GetById(id);
            
            if (publisher != null)
            {
                var doesContainBooks = ((PublisherRepository) _repository).DoesContainBooks(id);
                if (doesContainBooks)
                {
                    ErrorMessage = "This publisher cannot be deleted because it contains books. Please delete the books first.";
                    Publisher = publisher;
                    return Page();
                }
                _repository.Delete(id);
            }

            return RedirectToPage("./Index");
        }
    }
}