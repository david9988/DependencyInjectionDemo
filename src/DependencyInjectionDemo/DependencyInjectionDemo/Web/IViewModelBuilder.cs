namespace DependencyInjectionDemo.Web
{
    public interface IViewModelBuilder<TViewModel>
    {
        TViewModel Build();
    }

    public interface IViewModelBuilder<TViewModel, TId>
    {
        TViewModel Build(TId id);
    }
}
