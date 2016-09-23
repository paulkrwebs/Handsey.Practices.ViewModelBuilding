using BuildUp;
using Handsey.Handlers;

namespace Handsey.Practices.ViewModelBuilding
{
    using System.Threading.Tasks;

    public class ContentHandlerPipeline : IContentHandlerPipeline
    {
        private readonly IApplicaton _application;

        public ContentHandlerPipeline(IApplicaton application)
        {
            _application = application;
        }

        public async Task<bool> RaiseAsync<THandlerArgs>(THandlerArgs args)
            where THandlerArgs : HandlerArgs
        {
            return await _application.InvokeAsync<IHandlerAsync<THandlerArgs>>(async h => await h.HandleAsync(args));
        }

        public bool Raise<THandlerArgs>(THandlerArgs args)
            where THandlerArgs : HandlerArgs
        {
            return _application.Invoke<IHandler<THandlerArgs>>(h => h.Handle(args));
        }
    }
}