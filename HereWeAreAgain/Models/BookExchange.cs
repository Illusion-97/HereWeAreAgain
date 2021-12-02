using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HereWeAreAgain.Models
{
    public class BookExchange : BaseEntity
    {
        public DateTime CreationDate { get; set; }
        public Book Book { get; set; }
        public User OldOwner { get; set; } // Ancien propriétaire
    }
}
