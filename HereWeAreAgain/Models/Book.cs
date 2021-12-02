using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HereWeAreAgain.Models
{
    public class Book : BaseEntity
    {

        public string ISBN { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public double Price { get; set; }
        public DateTime EditionDate { get; set; }
        public enum BookState { VERY_GOOD, GOOD, MEDIOCRE, WORN };
        public BookState State{ get; set; }
        public User Owner { get; set; } //Propriétaire du livre
        [NotMapped]
        public int OwnerId { get; set; }
        public int PointsValue { get; set; }
    }
}
