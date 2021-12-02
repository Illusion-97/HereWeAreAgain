using HereWeAreAgain.DAO;
using HereWeAreAgain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HereWeAreAgain.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository UserDao = new UserDAO();
        public List<User> FindAll()
        {
            return UserDao.FindAll();
        }
    }
}
