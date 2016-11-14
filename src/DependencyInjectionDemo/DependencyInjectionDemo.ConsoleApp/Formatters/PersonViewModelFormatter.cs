using DependencyInjectionDemo.ConsoleApp.ViewModels;
using System.ComponentModel.Composition;
using System.Text;

namespace DependencyInjectionDemo.ConsoleApp.Formatters
{
    [Export(typeof(IScreenFormatter<PersonViewModel>))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class PersonViewModelFormatter : IScreenFormatter<PersonViewModel>
    {
        public string Format(PersonViewModel objectToFormat)
        {
            var sb = new StringBuilder();

            sb.AppendFormat("  Display name: {0}", objectToFormat.DisplayName).AppendLine();
            sb.AppendFormat("  Id:           {0}", objectToFormat.PersonId).AppendLine();
            sb.AppendFormat("  First name:   {0}", objectToFormat.FirstName).AppendLine();
            sb.AppendFormat("  Surname:      {0}", objectToFormat.Surname).AppendLine();
            sb.AppendFormat("  Title:        {0}", objectToFormat.Title).AppendLine();
            sb.AppendFormat("  Age:          {0}", objectToFormat.Age);

            return sb.ToString();
        }
    }
}