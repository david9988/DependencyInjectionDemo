using DependencyInjectionDemo.Web;
using System.ComponentModel.Composition;
using System.Web.Mvc;

namespace DependencyInjectionDemo.Website.Controllers
{
    public class BaseController : Controller
    {
        [Import]
        protected IViewModelFactory ViewModelFactory { get; private set; }
    }
}