using DependencyInjectionDemo.Models;
using DependencyInjectionDemo.Website.ViewModels;
using System.ComponentModel.Composition;

namespace DependencyInjectionDemo.Website.Helpers.Mappers
{
    [Export(typeof(IMapper<Person, PersonDisplayNameViewModel>))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class PersonListMapper : IMapper<Person, PersonDisplayNameViewModel>
    {
        public PersonDisplayNameViewModel Map(Person source)
        {
            if (source == null)
            {
                return null;
            }

            var target = new PersonDisplayNameViewModel
            {
                DisplayName = (source.FirstName + " " + source.Surname).Trim(),
                PersonId = source.PersonId
            };

            return target;
        }
    }
}