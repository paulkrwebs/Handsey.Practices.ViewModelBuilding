namespace Handsey.Practices.ViewModelBuilding
{
    using System.Threading.Tasks;
    using BuildUp;
    using Handsey.Handlers;


    public class HandseyContentHandlerPipeline : IContentHandlerPipeline
    {
        private readonly IApplicaton _application;

        public HandseyContentHandlerPipeline(IApplicaton application)
        {
            _application = application;
        }

        public async Task<bool> RaiseAsync<THandlerArgs>(THandlerArgs args)
            where THandlerArgs : ContentHandlerArgs
        {
            return await _application.InvokeAsync<IContentHandlerAsync<THandlerArgs>>(async h => await h.HandleAsync(args));
        }

        public bool Raise<THandlerArgs>(THandlerArgs args)
            where THandlerArgs : ContentHandlerArgs
        {
            return _application.Invoke<IContentHandler<THandlerArgs>>(h => h.Handle(args));
        }
    }
}