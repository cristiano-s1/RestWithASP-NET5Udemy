using System.Threading;
using RestWithASPNETUdemy.Model;
using System.Collections.Generic;

namespace RestWithASPNETUdemy.Services.Implementation
{
    public class PersonServiceImplementation : IPersonService
    {
        // Counter responsible for generating a fake ID
        // since we are not accessing any database
        private volatile int count; //volatile: Semple implementando um id maior na sequencia

        // Method responsible for creating a new person.
        // If we had a database this would be the time to persist the data
        public Person Create(Person person)
        {
            return person;
        }

        // Method responsible for deleting a person from an ID
        public void Delete(long id)
        {
            // Our exclusion logic would come here
        }

        // Method responsible for returning all people,
        // again this information is mocks
        public List<Person> FindAll()
        {
            List<Person> list = new List<Person>();
            for (int i = 0; i < 8; i++)
            {
                Person person = MockPerson(i);
                list.Add(person);
            }

            return list;
        }


        // Method responsible for returning a person
        // as we have not accessed any database we are returning a mock
        public Person FindById(long id)
        {
            return new Person()
            {
                Id = IncrementeAndGet(),
                FirstName = "Cristiano",
                LastName = "Campos",
                Address = "São Paulo",
                Gender = "Male"
            };
        }

        // Method responsible for updating a person for
        // being mock we return the same information passed
        public Person Update(Person person)
        {
            return person;
        }

        private Person MockPerson(int i)
        {
            return new Person()
            {
                Id = IncrementeAndGet(),
                FirstName = "Person Name" + i,
                LastName = "Person LastName" + i,
                Address = "Some Address" + i,
                Gender = "Male"
            };
        }

        private int IncrementeAndGet()
        {
            return Interlocked.Increment(ref count);
        }
    }
}
