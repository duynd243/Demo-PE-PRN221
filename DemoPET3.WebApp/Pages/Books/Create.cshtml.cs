using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DemoPET3.Repository.Models;
using DemoPET3.Repository.Repositories;

namespace DemoPET3.WebApp.Pages.Books
{
    public class CreateModel : PageModel
    {
        private readonly DemoPEContext _context;
        private readonly IRepository<Book> _repository;

        public CreateModel(DemoPEContext context, IRepository<Book> repository)
        {
            _context = context;
            _repository = repository;
        }

        public IActionResult OnGet()
        {
        ViewData["PublisherId"] = new SelectList(_context.Publishers, "PublisherId", "PublisherId");
            return Page();
        }

        [BindProperty]
        public Book Book { get; set; }
        public string ErrorMessage { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            ViewData["PublisherId"] = new SelectList(_context.Publishers, "PublisherId", "PublisherId");
            if (!ModelState.IsValid)
            {
                return Page();
            }
            
            var book = _repository.GetById(Book.BookId);
            
            // Đã tồn tại trong db -> Ko insert, báo lỗi
            if (book != null)
            {
                ErrorMessage = "Book with this id was existed";
                return Page();
            }
            
            // Insert vào db
            _repository.Add(Book);

            return RedirectToPage("./Index");
        }
    }
}
