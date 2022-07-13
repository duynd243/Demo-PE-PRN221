using DemoPET3.Repository.Models;
using DemoPET3.Repository.Repositories;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DemoPET3.WebApp.Pages.Publishers
{
    public class DetailsModel : PageModel
    {
        private readonly IRepository<Publisher> _repository;

        public DetailsModel(IRepository<Publisher> repository)
        {
            _repository = repository;
        }
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
    }
}
