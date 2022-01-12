using RestWithASPNETUdemy.Model;
using System.Collections.Generic;
using RestWithASPNETUdemy.Data.VO;
using RestWithASPNETUdemy.Repository;
using RestWithASPNETUdemy.Data.Converter.Implementations;

namespace RestWithASPNETUdemy.Business.Implementation
{
    public class PersonBusinessImplementation : IPersonBusiness
    {

        #region INJECTION
        //private readonly IRepository<Person> _repository;
        private readonly IPersonRepository _repository;

        private readonly PersonConverter _converter;

        public PersonBusinessImplementation(IPersonRepository repository)
        {
            _repository = repository;
            _converter = new PersonConverter();
        }
        #endregion

        #region GET
        public List<PersonVO> FindAll()
        {
            return _converter.Parse(_repository.FindAll()); //convertendo para VO 
        }

        public PersonVO FindById(int id)
        {
            return _converter.Parse(_repository.FindById(id));
        }
        #endregion

        #region POST
        public PersonVO Create(PersonVO person) //Chega como VO
        {
            var personEntity = _converter.Parse(person); //Convert para Person

            personEntity = _repository.Create(personEntity); //Cria 

            return _converter.Parse(personEntity); //Convert para VO
        }
        #endregion

        #region PUT
        public PersonVO Update(PersonVO person) //Chega como VO
        {
            var personEntity = _converter.Parse(person); //Convert para Person

            personEntity = _repository.Update(personEntity); //Cria 

            return _converter.Parse(personEntity); //Convert para VO
        }
        #endregion

        #region DELETE
        public void Delete(int id)
        {
            _repository.Delete(id);
        }
        #endregion

        #region PATH
        public PersonVO Disable(int id)
        {
            var personEntity = _repository.Disable(id);

            return _converter.Parse(personEntity);
        }
        #endregion
    }
}
