namespace DependencyInjectionDemo.Web
{
    public interface ICommand<TViewModel>
    {
        void Execute();
    }
}
