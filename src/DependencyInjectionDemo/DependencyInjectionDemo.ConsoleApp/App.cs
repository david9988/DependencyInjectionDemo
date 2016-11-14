using DependencyInjectionDemo.ConsoleApp.Formatters;
using DependencyInjectionDemo.ConsoleApp.ViewModels;
using DependencyInjectionDemo.Contracts;
using DependencyInjectionDemo.Models;
using System;
using System.ComponentModel.Composition;

namespace DependencyInjectionDemo.ConsoleApp
{
    [Export(typeof(IApp))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class App : IApp
    {
        private readonly IPersonService _personService;
        private readonly IMapper<Person, PersonViewModel> _personMapper;

        [Import]
        private IScreenFormatter<PersonViewModel> _personViewModelFormatter { get; set; }

        [ImportingConstructor]
        public App(IPersonService personService, IMapper<Person, PersonViewModel> personMapper)
        {
            _personService = personService;
            _personMapper = personMapper;
        }
        
        public void Run()
        {
            var inputValue = string.Empty;

            Console.WriteLine("Started...");

            while (!inputValue.Equals("x"))
            {
                Console.WriteLine("Enter the id: ");

                inputValue = Console.ReadLine();

                if (inputValue != "x")
                {
                    var id = 0;
                    if (int.TryParse(inputValue,out id))
                    {
                        PersonViewModel person = null;

                        try
                        {
                            person = _personMapper.Map(GetPerson(id));
                        }
                        catch
                        {

                        }

                        if (person == null)
                        {
                            Console.WriteLine("No person found with that id");
                        }
                        else
                        {
                            Console.WriteLine("Found: ");
                            Console.WriteLine(_personViewModelFormatter.Format(person));
                        }
                    }
                }
            }

        }

        private Person GetPerson(int id)
        {
            return _personService.GetById(id);
        }
    }
}