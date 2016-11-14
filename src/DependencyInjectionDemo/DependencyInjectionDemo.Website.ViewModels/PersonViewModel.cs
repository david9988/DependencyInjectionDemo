using System.ComponentModel;

namespace DependencyInjectionDemo.Website.ViewModels
{
    public class PersonViewModel
    {
        public int PersonId { get; set; }

        [DisplayName("First name")]
        public string FirstName { get; set; }

        public string Surname { get; set; }

        public string Title { get; set; }

        public int Age { get; set; }

        // This is extra to the model.
        public string DisplayName { get; set; }
    }
}