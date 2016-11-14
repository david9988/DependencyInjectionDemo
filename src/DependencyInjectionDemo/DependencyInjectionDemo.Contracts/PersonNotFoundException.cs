using System;

namespace DependencyInjectionDemo.Contracts
{
    public class PersonNotFoundException : Exception
    {
        public PersonNotFoundException(int id)
            : base(string.Format("Person with id {0} not found", id))
        {

        }
    }
}