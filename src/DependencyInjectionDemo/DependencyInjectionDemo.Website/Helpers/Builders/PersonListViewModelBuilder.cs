using DependencyInjectionDemo.Contracts;
using DependencyInjectionDemo.Models;
using DependencyInjectionDemo.Web;
using DependencyInjectionDemo.Website.ViewModels;
using System.ComponentModel.Composition;
using System.Linq;

namespace DependencyInjectionDemo.Website.Helpers.Builders
{
    [Export(typeof(IViewModelBuilder<PersonListViewModel>))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class PersonListViewModelBuilder : IViewModelBuilder<PersonListViewModel>
    {
        private readonly IPersonService _personService;
        private readonly IMapper<Person, PersonDisplayNameViewModel> _mapper;

        public PersonListViewModelBuilder(IPersonService personService, IMapper<Person, PersonDisplayNameViewModel> mapper)
        {
            _personService = personService;
            _mapper = mapper;
        }

        public PersonListViewModel Build()
        {
            var people = _personService.Get();
            var result = new PersonListViewModel();

            result.People.AddRange(people.Select(_mapper.Map));

            return result;
        }
    }
}