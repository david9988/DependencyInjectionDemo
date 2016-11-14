using DependencyInjectionDemo.Contracts;
using DependencyInjectionDemo.Interceptors;
using DependencyInjectionDemo.Models;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace DependencyInjectionDemo.Services.PersonService
{
    [Export(typeof(IPersonService))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [Intercept(typeof(ExceptionInterceptor), 2)]
    [Intercept(typeof(LoggingInterceptor), 1)]
    public class Service : IPersonService
    {
        private static int _personId = 0;
        private static List<Person> _people;

        static Service()
        {
            InitPeople();
        }

        public int Create(Person newPerson)
        {
            newPerson.PersonId = ++_personId;
            _people.Add(newPerson);

            return newPerson.PersonId;
        }

        public void Delete(int id)
        {
            var person = GetById(id);

            if (person != null)
            {
                _people.Remove(person);
            }
        }

        public IEnumerable<Person> Get()
        {
            return _people;
        }

        public Person GetById(int id)
        {
            var person = _people.FirstOrDefault(p => p.PersonId == id);

            if (person == null)
            {
                throw new PersonNotFoundException(id);
            }

            return person;
        }

        public void Update(Person person)
        {
            var original = GetById(person.PersonId);

            original.Age = person.Age;
            original.FirstName = person.FirstName;
            original.Surname = person.Surname;
            original.Title = person.Title;
        }

        private static void InitPeople()
        {
            _people = new List<Person>
            {
                new Person { Age = 42, FirstName = "Joe", PersonId = ++_personId, Surname = "Bloggs", Title = "Mr" },
                new Person { Age = 40, FirstName = "Jane", PersonId = ++_personId, Surname = "Bloggs", Title = "Mrs" }
            };
        }

    }
}