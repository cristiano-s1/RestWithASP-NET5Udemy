using RestWithASPNETUdemy.Model;
using RestWithASPNETUdemy.Repository;
using System.Collections.Generic;

namespace RestWithASPNETUdemy.Business.Implementation
{
    public class PersonBusinessImplementation : IPersonBusiness
    {

        #region INJECTION
        private readonly IPersonRepository _repository;

        public PersonBusinessImplementation(IPersonRepository repository)
        {
            _repository = repository;
        }
        #endregion

        #region GET
        public List<Person> FindAll()
        {
            return _repository.FindAll();
        }

        public Person FindById(int id)
        {
            return _repository.FindById(id);
        }
        #endregion

        #region POST
        public Person Create(Person person)
        {
            return _repository.Create(person);
        }
        #endregion

        #region PUT
        public Person Update(Person person)
        {
            return _repository.Update(person);
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
