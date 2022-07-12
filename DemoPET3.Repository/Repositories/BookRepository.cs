using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DemoPET3.Repository.Models;
using Microsoft.EntityFrameworkCore;

namespace DemoPET3.Repository.Repositories
{
    public class BookRepository: IRepository<Book>
    {
        private readonly DemoPEContext _context;

        public BookRepository(DemoPEContext context)
        {
            _context = context;
        }


        public IEnumerable<Book> GetAll()
        {
            return _context.Books
                .Include(b => b.Publisher)
                .ToList();
        }

        public Book GetById(string id)
        {
            return _context.Books.FirstOrDefault(b => b.BookId == id);
        }

        public void Add(Book entity)
        {
            _context.Books.Add(entity);
            _context.SaveChanges();
        }

        public void Update(Book entity)
        {
            _context.Books.Update(entity);
            _context.SaveChanges();
        }

        public void Delete(string id)
        {
            var book = GetById(id);
            if (book != null)
            {
                _context.Books.Remove(book);
                _context.SaveChanges();
            }
        }
    }
}