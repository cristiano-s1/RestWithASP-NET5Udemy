using System.Linq;
using RestWithASPNETUdemy.Model;
using RestWithASPNETUdemy.Model.Context;

namespace RestWithASPNETUdemy.Repository.Generic
{
    public class PersonRepository : GenericRepository<Person>, IPersonRepository
    {
        public PersonRepository(RestFullContext context) : base(context) { }

        public Person Disable(int id)
        {
            if (!_context.Persons.Any(p => p.Id.Equals(id))) return null;

            var user = _context.Persons.FirstOrDefault(p => p.Id.Equals(id));

            if (user != null)
            {
                user.Enabled = false;
                try
                {
                    _context.Entry(user).CurrentValues.SetValues(user);
                    _context.SaveChanges(); 
                }
                catch (System.Exception)
                {

                    throw;
                }
            }

            return user;
           
        }
    }
}
