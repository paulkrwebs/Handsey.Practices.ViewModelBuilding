namespace Handsey.Practices.ViewModelBuilding
{
    public interface IContentHandlerPipeline
    {
        void Raise<THandlerArgs>(THandlerArgs args)
            where THandlerArgs : HandlerArgs;
    }
}