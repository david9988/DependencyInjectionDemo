using DependencyInjectionDemo.Models;
using DependencyInjectionDemo.Website.ViewModels;
using System.ComponentModel.Composition;

namespace DependencyInjectionDemo.Website.Helpers.Mappers
{
    [Export(typeof(IMapper<Person, PersonViewModel>))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class PersonMapper : IMapper<Person, PersonViewModel>
    {
        public PersonViewModel Map(Person source)
        {
            if (source == null)
            {
                return null;
            }

            var target = new PersonViewModel
            {
                Age = source.Age,
                DisplayName = (source.FirstName + " " + source.Surname).Trim(),
                FirstName = source.FirstName,
                PersonId = source.PersonId,
                Surname = source.Surname,
                Title = source.Title
            };

            return target;
        }
    }
}