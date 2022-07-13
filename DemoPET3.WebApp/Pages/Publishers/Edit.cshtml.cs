using System.Threading.Tasks;
using DemoPET3.Repository.Models;
using DemoPET3.Repository.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DemoPET3.WebApp.Pages.Publishers
{
    public class EditModel : PageModel
    {
        private readonly IRepository<Publisher> _repository;

        public EditModel(IRepository<Publisher> repository)
        {
            _repository = repository;
        }

        [BindProperty]
        public Publisher Publisher { get; set; }
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

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _repository.Update(Publisher);

            return RedirectToPage("./Index");
        }
    }
}
