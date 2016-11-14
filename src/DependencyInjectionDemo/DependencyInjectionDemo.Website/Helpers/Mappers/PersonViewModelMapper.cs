using DependencyInjectionDemo.Models;
using DependencyInjectionDemo.Website.ViewModels;
using System.ComponentModel.Composition;

namespace DependencyInjectionDemo.Website.Helpers.Mappers
{
    [Export(typeof(IMapper<PersonViewModel, Person>))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class PersonViewModelMapper : IMapper<PersonViewModel, Person>
    {
        public Person Map(PersonViewModel source)
        {
            if (source == null)
            {
                return null;
            }

            var target = new Person
            {
                Age = source.Age,
                FirstName = source.FirstName,
                PersonId = source.PersonId,
                Surname = source.Surname,
                Title = source.Title
            };

            return target;
        }
    }
}