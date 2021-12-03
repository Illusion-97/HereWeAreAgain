using HereWeAreAgain.DAO;
using HereWeAreAgain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HereWeAreAgain.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository BookDao = new BookDAO();
        public List<Book> FindAll()
        {
            return BookDao.FindAll();
        }

        public void ValidateExchange(Book b, int newOwner)
        {
            b.OwnerId = newOwner; // Utilise l'OwnerId comme tampon pour la mise a jour lors de la persistence
            BookDao.SaveOrUpdate(new BookExchange { CreationDate = DateTime.Now, Book = b, OldOwner = b.Owner });
        }
    }
}
