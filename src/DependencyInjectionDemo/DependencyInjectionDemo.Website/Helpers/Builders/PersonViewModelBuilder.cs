using DependencyInjectionDemo.Contracts;
using DependencyInjectionDemo.Models;
using DependencyInjectionDemo.Web;
using DependencyInjectionDemo.Website.ViewModels;
using System.ComponentModel.Composition;

namespace DependencyInjectionDemo.Website.Helpers.Builders
{
    [Export(typeof(IViewModelBuilder<PersonViewModel, int>))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class PersonViewModelBuilder : IViewModelBuilder<PersonViewModel, int>
    {
        private readonly IPersonService _personService;
        private readonly IMapper<Person, PersonViewModel> _mapper;

        public PersonViewModelBuilder(IPersonService personService, IMapper<Person, PersonViewModel> mapper)
        {
            _personService = personService;
            _mapper = mapper;
        }
        public PersonViewModel Build(int id)
        {
            var person = _personService.GetById(id);
            var result = _mapper.Map(person);

            return result;
        }
    }
}