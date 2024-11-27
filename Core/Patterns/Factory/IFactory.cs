namespace HeavyCavStudios.Core.Patterns.Factory
{
    public interface IFactory<out T>
    {
        T Create();
    }
}
