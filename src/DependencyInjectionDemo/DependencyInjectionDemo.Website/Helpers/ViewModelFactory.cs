using DependencyInjectionDemo.Interceptors;
using DependencyInjectionDemo.Web;
using System.ComponentModel.Composition;

namespace DependencyInjectionDemo.Website.Helpers
{
    [Export(typeof(IViewModelFactory))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    [Intercept(typeof(LoggingInterceptor), 0)]
    public class ViewModelFactory : IViewModelFactory
    {
        #region ViewModels

        public TViewModel CreateViewModel<TViewModel>()
        {
            var service = DependencyResolver.Current.GetService(typeof(IViewModelBuilder<TViewModel>)) as IViewModelBuilder<TViewModel>;

            return service.Build();
        }

        public TViewModel CreateViewModel<TViewModel, TId>(TId id)
        {
            var service = DependencyResolver.Current.GetService(typeof(IViewModelBuilder<TViewModel, TId>)) as IViewModelBuilder<TViewModel, TId>;

            return service.Build(id);
        }

        #endregion

        #region Commands

        public ICommand<TViewModel> CreateCommand<TViewModel>(TViewModel viewModel, CommandType commandType)
        {
            var service = DependencyResolver.Current.GetService(typeof(ICommandBuilder<TViewModel>)) as ICommandBuilder<TViewModel>;

            return service.Build(viewModel, commandType);
        }

        #endregion

    }
}