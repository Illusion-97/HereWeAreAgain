using HereWeAreAgain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HereWeAreAgain.Services
{
    interface IBookService
    {
        List<Book> FindAll();
        void ValidateExchange(Book b, int newOwner);
    }
}
