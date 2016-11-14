namespace DependencyInjectionDemo.Web
{
    public interface ICommandBuilder<TViewModel>
    {
        ICommand<TViewModel> Build(TViewModel viewModel, CommandType commandType);
    }
}
