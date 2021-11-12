using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelationsShip
{
    class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int AuthorId { get; set; }
        public Author Author { get; set; }
        public int CountPage { get; set; }
        public int Year { get; set; }
        public int GenreId { get; set; }
        public Genre Genre { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
