using System.Collections.Generic;
using RestWithASPNETUdemy.Data.VO;

namespace RestWithASPNETUdemy.Repository
{
    public interface IBookBusiness
    {
        BookVO Create(BookVO book);
        BookVO FindById(int id);
        List<BookVO> FindAll();
        BookVO Update(BookVO book);
        void Delete(int id);
    }
}
