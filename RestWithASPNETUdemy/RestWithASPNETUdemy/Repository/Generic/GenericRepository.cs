using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using RestWithASPNETUdemy.Model.Base;
using RestWithASPNETUdemy.Model.Context;

namespace RestWithASPNETUdemy.Repository.Generic
{
    public class GenericRepository<T> : IRepository<T> where T : BaseEntity
    {

        #region INJECTION
        private RestFullContext _context;

        //Dataset generico
        private DbSet<T> dataset;

        public GenericRepository(RestFullContext context)
        {
            _context = context;
            dataset = _context.Set<T>();
        }
        #endregion

        #region GET
        public List<T> FindAll()
        {
            return dataset.ToList();    
        }

        public T FindById(int id)
        {
            return dataset.SingleOrDefault(p => p.Id.Equals(id));
        }
        #endregion

        #region POST
        public T Create(T item)
        {
            try
            {
                dataset.Add(item);
                _context.SaveChanges();

                return item;
            }
            catch (Exception)
            {

                throw;
            }       
        }
        #endregion

        #region PUT
        public T Update(T item)
        {
            var result = dataset.SingleOrDefault(p => p.Id.Equals(item.Id));

            if (result != null)
            {
                try
                {
                    _context.Entry(result).CurrentValues.SetValues(item);
                    _context.SaveChanges();
                    return result;
                }
                catch (Exception)
                {
                    throw;
                }
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region DELETE
        public void Delete(int id)
        {
            var result = dataset.SingleOrDefault(p => p.Id.Equals(id));

            if (result != null)
            {
                try
                {
                    dataset.Remove(result);
                    _context.SaveChanges();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
        #endregion

        #region METODO EXISTS
        public bool Exists(int id)
        {
            return dataset.Any(p => p.Id.Equals(id));
        }
        #endregion
    }
}
