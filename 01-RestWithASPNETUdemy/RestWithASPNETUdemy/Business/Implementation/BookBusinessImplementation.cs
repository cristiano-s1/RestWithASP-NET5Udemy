using RestWithASPNETUdemy.Model;
using System.Collections.Generic;

namespace RestWithASPNETUdemy.Repository.Implementation
{
    public class BookBusinessImplementation : IBookBusiness
    {
        #region INJECTION
        private readonly IRepository<Book> _repository;

        public BookBusinessImplementation(IRepository<Book> repository)
        {
            _repository = repository;
        }

        #endregion

        #region Get
        public List<Book> FindAll()
        {
            return _repository.FindAll();
        }

        public Book FindById(int id)
        {
            return _repository.FindById(id);
        }
        #endregion

        #region POST
        public Book Create(Book book)
        {
           return _repository.Create(book);
        }
        #endregion

        #region PUT
        public Book Update(Book book)
        {
           return _repository.Update(book);
        }
        #endregion

        #region DELETE
        public void Delete(int id)
        {
            _repository.Delete(id);
        }
        #endregion

    }
}
