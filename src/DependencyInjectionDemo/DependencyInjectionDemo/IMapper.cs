namespace DependencyInjectionDemo
{
    public interface IMapper<TSource, TTarget>
    {
        TTarget Map(TSource source);
    }
}
