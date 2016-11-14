using DependencyInjectionDemo.Website.ViewModels;
using System.ComponentModel.Composition;
using System.Web.Mvc;

namespace DependencyInjectionDemo.Website.Controllers
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            var model = ViewModelFactory.CreateViewModel<PersonListViewModel>();

            return View(model);
        }

    }
}