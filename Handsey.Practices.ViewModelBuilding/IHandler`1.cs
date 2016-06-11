namespace Handsey.Practices.ViewModelBuilding
{
    public interface IHandler<TArgs> : IHandler
        where TArgs : HandlerArgs
    {
        void Handle(TArgs args);
    }
}
