namespace Handsey.Practices.ViewModelBuilding
{
    public class ContentHandlerPipeline : IContentHandlerPipeline
    {
        private readonly IHandlerResolver _handlerResolver;

        public ContentHandlerPipeline(IHandlerResolver handlerResolver)
        {
            _handlerResolver = handlerResolver;
        }

        public void Raise<THandlerArgs>(THandlerArgs args)
            where THandlerArgs : HandlerArgs
        {
            var handlers = _handlerResolver.ResolverAll<IHandler<THandlerArgs>>();

            foreach (var handler in handlers)
            {
                handler.Handle(args);
            }
        }
    }
}