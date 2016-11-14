namespace DependencyInjectionDemo.Web
{
    public interface IViewModelFactory
    {
        #region ViewModels

        TViewModel CreateViewModel<TViewModel>();

        TViewModel CreateViewModel<TViewModel, TId>(TId id);

        #endregion

        #region Commands

        ICommand<TViewModel> CreateCommand<TViewModel>(TViewModel viewModel, CommandType commandType);

        #endregion

    }
}
