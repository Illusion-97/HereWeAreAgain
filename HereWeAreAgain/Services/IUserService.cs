using HereWeAreAgain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HereWeAreAgain.Services
{
    public interface IUserService
    {
        List<User> FindAll();
    }
}
