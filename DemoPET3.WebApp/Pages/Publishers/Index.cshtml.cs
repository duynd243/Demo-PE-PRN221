using System;
using System.Collections.Generic;
using System.Linq;
using DemoPET3.Repository.Models;
using DemoPET3.Repository.Repositories;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DemoPET3.WebApp.Pages.Publishers
{
    public class IndexModel : PageModel
    {
        private readonly IRepository<Publisher> _repository;

        public IndexModel(IRepository<Publisher> repository)
        {
            _repository = repository;
        }

        public IEnumerable<Publisher> Publishers { get; set; }
        public string CurrentSearchValue { get; set; } = "";
        public int CurrentPage { get; set; } = 1;
        public int TotalPages { get; set; }
        public int PageSize { get; set; } = 3;


        public void OnGet(string searchValue, int pageNumber)
        {
            pageNumber = pageNumber == 0 ? 1 : pageNumber;
            var publisherList = _repository.GetAll();

            if (!String.IsNullOrEmpty(searchValue))
            {
                CurrentSearchValue = searchValue;
                publisherList = publisherList
                    .Where(p => p.PublisherName.ToLower().Contains(searchValue.ToLower()));
            }

            publisherList = publisherList.ToList();
            TotalPages = (int) Math.Ceiling(publisherList.Count() / (double) PageSize);
            CurrentPage = pageNumber > TotalPages ? TotalPages : pageNumber;
            var skip = (CurrentPage - 1) * PageSize;
            Publishers = publisherList.Skip(skip)
                .Take(PageSize);
        }
    }
}