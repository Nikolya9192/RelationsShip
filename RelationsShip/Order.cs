using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelationsShip
{
    public class Order
    {
        public Order()
        {
            Books = new HashSet<Book>();
        }
        public int Number { get; set; }       
        public DateTime Date { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public virtual ICollection<Book> Books { get; set; }

    }
}
