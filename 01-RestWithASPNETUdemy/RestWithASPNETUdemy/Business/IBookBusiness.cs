using RestWithASPNETUdemy.Model;
using System.Collections.Generic;

namespace RestWithASPNETUdemy.Repository
{
    public interface IBookBusiness
    {
        Book Create(Book books);
        Book FindById(int id);
        List<Book> FindAll();
        Book Update(Book books);
        void Delete(int id);
    }
}
