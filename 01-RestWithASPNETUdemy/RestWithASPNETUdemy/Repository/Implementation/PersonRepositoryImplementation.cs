using System;
using System.Linq;
using RestWithASPNETUdemy.Model;
using System.Collections.Generic;
using RestWithASPNETUdemy.Model.Context;

namespace RestWithASPNETUdemy.Repository.Implementation
{
    public class PersonRepositoryImplementation : IPersonRepository
    {

        #region INJECTION
        private RestFullContext _context;

        public PersonRepositoryImplementation(RestFullContext context)
        {
            _context = context;
        }
        #endregion

        #region GET
        public List<Person> FindAll()
        {
            return _context.Persons.ToList();
        }

        public Person FindById(int id)
        {
            return _context.Persons.SingleOrDefault(p => p.Id.Equals(id));
        }
        #endregion

        #region POST
        public Person Create(Person person)
        {
            try
            {
                _context.Add(person);
                _context.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }

            return person;
        }
        #endregion

        #region PUT
        public Person Update(Person person)
        {
            if (!Exists(person.Id)) return null;

            var result = _context.Persons.SingleOrDefault(p => p.Id == person.Id);


            if (result != null)
            {
                try
                {
                    _context.Entry(result).CurrentValues.SetValues(person);
                    _context.SaveChanges();
                }
                catch (Exception)
                {

                    throw;
                }
            }

            return person;
        }
        #endregion

        #region DELETE
        public void Delete(int id)
        {
            var result = _context.Persons.SingleOrDefault(p => p.Id == id);

            if (result != null)
            {
                try
                {
                    _context.Persons.Remove(result);
                    _context.SaveChanges();
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }
        #endregion

        #region EXISTS
        // Método para verifique se existe
        public bool Exists(int id)
        {
            return _context.Persons.Any(p => p.Id == id);
        }
        #endregion

    }
}
