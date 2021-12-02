using RestWithASPNETUdemy.Model;
using System.Collections.Generic;

namespace RestWithASPNETUdemy.Repository
{
    public interface IBookRepository
    {
        Book Create(Book books);
        Book FindById(int id);
        List<Book> FindAll();
        Book Update(Book books);
        void Delete(int id);
        bool Exists(int id);
    }
}
