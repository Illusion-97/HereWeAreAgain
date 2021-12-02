using HereWeAreAgain.Context;
using HereWeAreAgain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace HereWeAreAgain.DAO
{
    public class BookDAO : IBookRepository
    {
        private readonly MyContext db = new MyContext();
        public List<Book> FindAll()
        {
            return db.Books.Include(b => b.Owner).AsNoTracking().ToList();
        }

        public void SaveOrUpdate(BookExchange exc)
        {
            exc.Book.Owner = db.Users.Find(exc.Book.OwnerId);
            if (exc.Book.Id == 0)
            {
                db.Books.Add(exc.Book);
            }
            else
            {
                db.Books.Find(exc.Book.Id).Owner = exc.Book.Owner;
            }
            db.BookExchanges.Add(exc);
            db.SaveChanges();
        }
    }
}
