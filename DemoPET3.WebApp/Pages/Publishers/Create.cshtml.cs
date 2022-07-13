using DemoPET3.Repository.Models;
using DemoPET3.Repository.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DemoPET3.WebApp.Pages.Publishers
{
    public class CreateModel : PageModel
    {
        private readonly IRepository<Publisher> _repository;

        public CreateModel(IRepository<Publisher> repository)
        {
            _repository = repository;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Publisher Publisher { get; set; }
        public string ErrorMessage { get; set; }
        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            
            var publisher = _repository.GetById(Publisher.PublisherId);
            if (publisher != null)
            {
                ErrorMessage = "Publisher with this id already exists";
                return Page();
            }
            _repository.Add(Publisher);
            return RedirectToPage("./Index");
        }
    }
}
