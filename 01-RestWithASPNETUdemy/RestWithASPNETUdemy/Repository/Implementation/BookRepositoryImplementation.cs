using System;
using System.Linq;
using System.Collections.Generic;
using RestWithASPNETUdemy.Model;
using RestWithASPNETUdemy.Model.Context;

namespace RestWithASPNETUdemy.Repository.Implementation
{
    public class BookRepositoryImplementation : IBookRepository
    {
        #region INJECTION
        private RestFullContext _context;

        public BookRepositoryImplementation(RestFullContext context)
        {
            _context = context;
        }

        #endregion

        #region Get
        public List<Book> FindAll()
        {
            return _context.Books.ToList();
        }

        public Book FindById(int id)
        {
            return _context.Books.SingleOrDefault(x => x.Id == id);
        }
        #endregion

        #region POST
        public Book Create(Book book)
        {
            try
            {
                _context.Add(book);
                _context.SaveChanges();            
            }
            catch (Exception)
            {

                throw;
            }

            return book;
        }
        #endregion

        #region PUT
        public Book Update(Book book)
        {
            if (!Exists(book.Id)) return null;
            {
                var result = _context.Books.SingleOrDefault(x => x.Id == book.Id);

                if (result != null)
                {
                    try
                    {
                        _context.Entry(result).CurrentValues.SetValues(book);
                        _context.SaveChanges();
                    }
                    catch (Exception)
                    {

                        throw;
                    }            
                }

                return book;
            }
        }
        #endregion

        #region DELETE
        public void Delete(int id)
        {
            var result = _context.Books.SingleOrDefault(x => x.Id == id);

            if (result != null)
            {
                try
                {
                    _context.Books.Remove(result);
                    _context.SaveChanges();
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }
        #endregion

        #region MÉTODO EXISTS
        public bool Exists(int id)
        {
            return _context.Books.Any(p => p.Id == id);
        }
        #endregion






    }
}
