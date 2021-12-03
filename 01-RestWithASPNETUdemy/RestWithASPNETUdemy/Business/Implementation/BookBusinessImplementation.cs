using RestWithASPNETUdemy.Model;
using System.Collections.Generic;
using RestWithASPNETUdemy.Data.VO;
using RestWithASPNETUdemy.Data.Converter.Implementations;

namespace RestWithASPNETUdemy.Repository.Implementation
{
    public class BookBusinessImplementation : IBookBusiness
    {
        #region INJECTION
        private readonly IRepository<Book> _repository;

        private readonly BookConverter _converter;

        public BookBusinessImplementation(IRepository<Book> repository)
        {
            _repository = repository;
            _converter = new BookConverter();
        }

        #endregion

        #region Get
        public List<BookVO> FindAll()
        {
            return _converter.Parse(_repository.FindAll()); 
        }

        public BookVO FindById(int id)
        {
            return _converter.Parse(_repository.FindById(id));
        }
        #endregion

        #region POST
        public BookVO Create(BookVO book) //Chega como VO
        {
            var bookEntity = _converter.Parse(book); //Convert para Book

            bookEntity = _repository.Create(bookEntity); //Cria 

            return _converter.Parse(bookEntity); //Convert para VO
        }
        #endregion

        #region PUT
        public BookVO Update(BookVO book)
        {
            var bookEntity = _converter.Parse(book); //Convert para Book

            bookEntity = _repository.Update(bookEntity); //Cria 

            return _converter.Parse(bookEntity); //Convert para VO
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
