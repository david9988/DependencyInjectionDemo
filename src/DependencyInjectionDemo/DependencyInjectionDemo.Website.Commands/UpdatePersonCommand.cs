using DependencyInjectionDemo.Contracts;
using DependencyInjectionDemo.Models;
using DependencyInjectionDemo.Web;
using DependencyInjectionDemo.Website.ViewModels;

namespace DependencyInjectionDemo.Website.Commands
{
    public class UpdatePersonCommand : ICommand<PersonViewModel>
    {
        private readonly IPersonService _personService;
        private readonly Person _person;

        public UpdatePersonCommand(IPersonService personService, Person person)
        {
            _personService = personService;
            _person = person;
        }

        public void Execute()
        {
            _personService.Update(_person);
        }
    }
}