using System.Collections.Generic;
using System.Linq;
using DemoPET3.Repository.Models;
using Microsoft.EntityFrameworkCore;

namespace DemoPET3.Repository.Repositories
{
    public class PublisherRepository : IRepository<Publisher>
    {
        private readonly DemoPEContext _context;

        public PublisherRepository(DemoPEContext context)
        {
            _context = context;
        }
        public IEnumerable<Publisher> GetAll()
        {
            return _context.Publishers.ToList();
        }

        public Publisher GetById(string id)
        {
            return _context.Publishers
                .FirstOrDefault(p => p.PublisherId == id);
        }

        public void Add(Publisher entity)
        {
            _context.Publishers.Add(entity);
            _context.SaveChanges();
        }

        public void Update(Publisher entity)
        {
            _context.Attach(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(string id)
        {
            var publisher = GetById(id);
            if (publisher == null) return;
            _context.Publishers.Remove(publisher);
            _context.SaveChanges();
        }
        
        public bool DoesContainBooks(string id)
        {
            return _context.Books.Any(b => b.PublisherId == id);
        }
    }
}