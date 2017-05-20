using System;
using System.Linq;

namespace RepositoryPattern
{
    public class Program
    {
        private static readonly Context _context = new Context();
        private static readonly Repository<Context> _repo;

        static Program()
        {
            _repo = new Repository<Context>(_context);
        }

        static void Main(string[] args)
        {
            Console.WriteLine("-------------------- Add New Book ------------------------");

            var book = new Book();
            book.Name = "Fi";
            _repo.Save<Book>(book);

            var books = _repo.Filter<Book>(s => s.Id > 0).ToList();
            Console.WriteLine("-------------------- Book List ------------------------");
            var id = 0;
            foreach (var item in books)
            {
                id = item.Id;
                Console.WriteLine("Id: " + item.Id + ", Name: "+item.Name);
            }

            Console.WriteLine("-------------------- Get Book ------------------------");

            var getBook = _repo.Find<Book>(s=>s.Id.Equals(id));
            Console.WriteLine("Id: " + getBook.Id + ", Name: " + getBook.Name);

            Console.WriteLine("-------------------- Update Book ------------------------");
            getBook.Name = "Name updated";
            _repo.Update<Book>(getBook);
            Console.WriteLine("-------------------- New Book List ------------------------");

            var newBookList = _repo.Filter<Book>(s => s.Id > 0).ToList();
            foreach (var item in newBookList)
            {
                id = item.Id;
                Console.WriteLine("Id: " + item.Id + ", Name: " + item.Name);
            }


            Console.WriteLine("-------------------- Delete Book ------------------------");
            _repo.Delete<Book>(getBook);
            Console.WriteLine("-------------------- New Book List ------------------------");

            var newBookList2 = _repo.Filter<Book>(s => s.Id > 0).ToList();
            foreach (var item in newBookList2)
            {
                id = item.Id;
                Console.WriteLine("Id: " + item.Id + ", Name: " + item.Name);
            }

            Console.ReadKey();
        }
    }
}
