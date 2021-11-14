using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;


namespace RelationsShip
{

    //    Create the database of library with next data:
    //Books - Name, Pages, Author
    //Authors - Name, Surname, Country
    //Countries - Name
    //Task 2:
    //Create the following queries using LINQ:

    //Get all books which have pages more that some number.
    //Get Books with name starting of some letter (ignore case)
    //Get all books by some author
    //Get all books by authors from some country
    //Get books with name of less than some symbol count
    //Get book with the min page count of some country
    //Get the author who has the least books
    //Get the country that has the most authors
    class Program
    {
        static void Main(string[] args)
        {
            LibraryDB db = new LibraryDB();
            db.SaveChanges();

            //Get all books which have pages more that some number.
            IEnumerable<Book> rezBook = db.Books.Include(s => s.Author).ToList();
            IEnumerable<Author> rezAuthor = db.Authors.Include(s => s.Country).ToList();
            IEnumerable<Country> rezCountry = db.Countries.ToList();

            //Console.ReadLine();
            //rezBook = db.Books.Include;
            static void ShowBooks(IEnumerable<Book> books)
            {
                foreach (var item in books)

                    Console.WriteLine($"{item.Title} - {item.Author.FirstName} {item.Author.LastName} - {item.CountPage} - {item.Year}");

            }

            static void ShowAuthors(IEnumerable<Author> authors)
            {
                foreach (var item in authors)

                    Console.WriteLine($"{item.FirstName} - {item.LastName} - {item.Country.Name}");

            }

            static void ShowCountries(IEnumerable<Country> countries)
            {
                foreach (var item in countries)

                    Console.WriteLine($"{item.Name}");

            }

            Console.WriteLine("Get all books which have pages more that 100:");
            rezBook = db.Books.Where((b) => b.CountPage > 100);
            ShowBooks(rezBook.ToList());
           
            Console.WriteLine();
            Console.WriteLine("Get Books with name starting of some letter(ignore case):");
            rezBook = db.Books.Where((c) => c.Title.ToUpper().StartsWith("F"));  
            ShowBooks(rezBook.ToList());

            Console.WriteLine();
            Console.WriteLine("Get all books by Author Taras Shevchenko:");
            rezBook = db.Books.Where((c) => c.Author.LastName.Contains("Shevchenko"));
            ShowBooks(rezBook.ToList());

            
            Console.WriteLine();
            Console.WriteLine("Get all books by authors from England:");
            rezBook = db.Books.Where((c) => c.Author.Country.Name.Contains("England"));
            ShowBooks(rezBook.ToList());

            Console.WriteLine();
            Console.WriteLine("Get books with name of less than 15 symbol's count:");
            rezBook = db.Books.Where((c) => c.Title.Length < 15);
            ShowBooks(rezBook.ToList());

            Console.WriteLine();
            Console.WriteLine("Get book with the min page count of England:");
            rezBook = db.Books.Where((b) => b.Author.Country.Name.Contains("England")).OrderBy(c => c.CountPage).Take(1);         
            ShowBooks(rezBook.ToList());

            Console.WriteLine();
            Console.WriteLine("Get the author who has the least books:");
            rezAuthor = db.Authors.Where(a => a.Books.Count < 2);
            ShowAuthors(rezAuthor.ToList());

            Console.WriteLine();
            Console.WriteLine("Get the country that has the most authors:");
            rezCountry = db.Countries.Where(c => c.Authors.Count >=2);
            ShowCountries(rezCountry.ToList());
        }
    }
}
