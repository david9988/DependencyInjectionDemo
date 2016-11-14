using System.Collections.Generic;

namespace DependencyInjectionDemo.Website.ViewModels
{
    public class PersonListViewModel
    {
        public PersonListViewModel()
        {
            People = new List<PersonDisplayNameViewModel>();
        }
        public List<PersonDisplayNameViewModel> People { get; private set; }
    }
}