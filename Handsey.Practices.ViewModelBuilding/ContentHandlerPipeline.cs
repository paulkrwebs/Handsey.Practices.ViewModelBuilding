using Handsey.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public Task<bool> RaiseAsync<THandlerArgs>(THandlerArgs args)
            where THandlerArgs : HandlerArgs
        {
            return _application.InvokeAsync<IHandlerAsync<THandlerArgs>>(async h => await h.HandleAsync(args));
        }

        public bool Raise<THandlerArgs>(THandlerArgs args)
            where THandlerArgs : HandlerArgs
        {
            return _application.Invoke<IHandler<THandlerArgs>>(h => h.Handle(args));
        }
    }
}