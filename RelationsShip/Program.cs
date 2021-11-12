using System;

namespace RelationsShip
{
    class Program
    {
        static void Main(string[] args)
        {
            LibraryDB db = new LibraryDB();

            
            db.SaveChanges();
        }
    }
}
