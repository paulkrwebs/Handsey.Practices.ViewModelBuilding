namespace Handsey.Practices.ViewModelBuilding
{
    using System.Threading.Tasks;

    public interface IContentHandlerPipeline
    {
        Task<bool> Raise<THandlerArgs>(THandlerArgs args)
            where THandlerArgs : HandlerArgs;
    }
}