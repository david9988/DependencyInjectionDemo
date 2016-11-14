using DependencyInjectionDemo.Contracts;
using DependencyInjectionDemo.Models;
using DependencyInjectionDemo.Web;
using DependencyInjectionDemo.Website.Commands;
using DependencyInjectionDemo.Website.ViewModels;
using System;
using System.ComponentModel.Composition;

namespace DependencyInjectionDemo.Website.Helpers.Builders
{
    [Export(typeof(ICommandBuilder<PersonViewModel>))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class PersonCommandBuilder : ICommandBuilder<PersonViewModel>
    {
        private readonly IMapper<PersonViewModel, Person> _mapper;
        private readonly IPersonService _personService;

        public PersonCommandBuilder(IPersonService personService, IMapper<PersonViewModel,Person> mapper)
        {
            _mapper = mapper;
            _personService = personService;
        }

        public ICommand<PersonViewModel> Build(PersonViewModel viewModel, CommandType commandType)
        {
            if (commandType == CommandType.Update)
            {
                return new UpdatePersonCommand(_personService, _mapper.Map(viewModel));
            }

            throw new ArgumentException("No command for " + commandType.ToString(), "commandType");
        }
    }
}