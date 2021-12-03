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
            exc.Book.Owner = db.Users.Find(exc.Book.OwnerId); // Mise a jour du propriétaire du livre (With tracking to avoid creation)
            if (exc.Book.Id == 0) // Si le livre n'existe pas en base (Cas impossible pour le moment)
            {
                db.Books.Add(exc.Book); // Ajout du livre en Base
            }
            else
            {
                Book b = db.Books.Find(exc.Book.Id); // Récupération du livre pour mise a jour (With tracking to avoid creation)
                b.Owner = exc.Book.Owner; // Update du propriétaire
                b.State = exc.Book.State; // Update de l'Etat
                b.pointsValue = exc.Book.pointsValue; // Update Valeur en points
                exc.Book = b;
            }
            db.Users.Attach(exc.OldOwner); // Evite la création d'un nouvel utilisateur lors de la persistence de l'échange
            db.BookExchanges.Add(exc); // Persistence de l'échange
            db.SaveChanges();
        }
    }
}
