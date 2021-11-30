using System;
using System.Linq;
using RestWithASPNETUdemy.Model;
using System.Collections.Generic;
using RestWithASPNETUdemy.Model.Context;

namespace RestWithASPNETUdemy.Services.Implementation
{
    public class PersonServiceImplementation : IPersonService
    {

        #region INJECTION
        private RestFullContext _context;

        public PersonServiceImplementation(RestFullContext context)
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
            if (!Exists(person.Id)) return new Person();

            var result = _context.Persons.SingleOrDefault(p => p.Id.Equals(person.Id));


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


        // Método para verifique se existe
        private bool Exists(int id)
        {
            return _context.Persons.Any(p => p.Id == id);
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

        #region INICIO

        // Counter responsible for generating a fake ID
        // since we are not accessing any database
        //private volatile int count; //volatile: Semple implementando um id maior na sequencia



        // Method responsible for creating a new person.
        // If we had a database this would be the time to persist the data
        //public Person Create(Person person)
        //{
        //    return person;
        //}

        // Method responsible for deleting a person from an ID
        //public void Delete(long id)
        //{
        //    // Our exclusion logic would come here
        //}

        // Method responsible for returning all people,
        // again this information is mocks
        //public List<Person> FindAll()
        //{
        //    List<Person> list = new List<Person>();
        //    for (int i = 0; i < 8; i++)
        //    {
        //        Person person = MockPerson(i);
        //        list.Add(person);
        //    }

        //    return list;
        //}


        // Method responsible for returning a person
        // as we have not accessed any database we are returning a mock
        //public Person FindById(long id)
        //{
        //    return new Person()
        //    {
        //        Id = IncrementeAndGet(),
        //        FirstName = "Cristiano",
        //        LastName = "Campos",
        //        Address = "São Paulo",
        //        Gender = "Male"
        //    };
        //}

        // Method responsible for updating a person for
        // being mock we return the same information passed
        //public Person Update(Person person)
        //{
        //    return person;
        //}

        //private Person MockPerson(int i)
        //{
        //    return new Person()
        //    {
        //        Id = IncrementeAndGet(),
        //        FirstName = "Person Name" + i,
        //        LastName = "Person LastName" + i,
        //        Address = "Some Address" + i,
        //        Gender = "Male"
        //    };
        //}

        //private int IncrementeAndGet()
        //{
        //    return Interlocked.Increment(ref count);
        //}
        #endregion
    }
}
