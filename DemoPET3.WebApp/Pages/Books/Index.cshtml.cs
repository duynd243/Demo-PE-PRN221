using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DemoPET3.Repository.Models;
using DemoPET3.Repository.Repositories;

namespace DemoPET3.WebApp.Pages.Books
{
    public class IndexModel : PageModel
    {
        private readonly IRepository<Book> _repository;

        public IndexModel(IRepository<Book> repository)
        {
            _repository = repository;
        }

        public string CurrentSearchValue { get; set; } = "";
        public int CurrentPage { get; set; } = 1;
        public int TotalPages { get; set; }
        public int PageSize { get; set; } = 3;

        public IEnumerable<Book> Book { get;set; }

        public async Task OnGetAsync(string searchValue, int pageNumber)
        {
            // Nếu truyền giá trị => pagenumber, nếu ko truyền hoặc truyền chuỗi => pagenumber = 1;
            pageNumber = pageNumber == 0 ? 1 : pageNumber;
            // Tất cả books
            var bookList = _repository.GetAll();
            
            
            // search
            if (!String.IsNullOrEmpty(searchValue))
            {
                CurrentSearchValue = searchValue;
                bookList =  bookList
                    .Where(b => b.BookName.ToLower().Contains(searchValue.ToLower()));
            }
            
            
            TotalPages= (int) Math.Ceiling(bookList.Count()/ (double)PageSize);
            var skip = (pageNumber - 1) * PageSize; // số phần tử bị skip
            CurrentPage = pageNumber > TotalPages ? TotalPages : pageNumber;
            Book = bookList.Skip(skip)
                .Take(PageSize);
        }
    }
}
