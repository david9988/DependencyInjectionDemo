using DependencyInjectionDemo.Web;
using DependencyInjectionDemo.Website.ViewModels;
using System.ComponentModel.Composition;
using System.Web.Mvc;

namespace DependencyInjectionDemo.Website.Controllers
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class PersonController : BaseController
    {
        public ActionResult Edit(int id)
        {
            var model = ViewModelFactory.CreateViewModel<PersonViewModel, int>(id);

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(int id, PersonViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.PersonId = id;
                var command = ViewModelFactory.CreateCommand(model, CommandType.Update);
                command.Execute();
            }

            return RedirectToAction("Index", "Home");
        }
    }
}