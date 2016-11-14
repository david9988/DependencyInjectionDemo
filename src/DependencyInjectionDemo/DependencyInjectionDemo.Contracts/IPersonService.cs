using DependencyInjectionDemo.Models;
using System.Collections.Generic;

namespace DependencyInjectionDemo.Contracts
{
    public interface IPersonService
    {
        Person GetById(int id);

        IEnumerable<Person> Get();

        int Create(Person newPerson);

        void Update(Person person);

        void Delete(int personId);
    }
}