using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelationsShip
{
    class Author
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }   
        public int CountryId { get; set; }
        public Country Country { get; set; }
        public ICollection<Book> Books { get; set; }
    }
}
