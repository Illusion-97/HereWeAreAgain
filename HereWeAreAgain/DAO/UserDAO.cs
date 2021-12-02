using HereWeAreAgain.Context;
using HereWeAreAgain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HereWeAreAgain.DAO
{
    public class UserDAO : IUserRepository
    {
        private MyContext db = new MyContext();
        public List<User> FindAll()
        {
            return db.Users.AsNoTracking().ToList();
        }
    }
}
