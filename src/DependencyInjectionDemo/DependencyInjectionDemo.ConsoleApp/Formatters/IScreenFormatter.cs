namespace DependencyInjectionDemo.ConsoleApp.Formatters
{
    public interface IScreenFormatter<T>
    {
        string Format(T objectToFormat);
    }
}
