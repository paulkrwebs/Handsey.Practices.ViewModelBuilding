namespace Handsey.Practices.ViewModelBuilding
{
    public class ViewModelBuilder : IViewModelBuilder
    {
        private readonly IPropertyMapper _propertyMapper;
        private readonly IContentHandlerPipeline _contentHandlerPipeline;

        public ViewModelBuilder(IPropertyMapper propertyMapper, IContentHandlerPipeline contentHandlerPipeline)
        {
            _contentHandlerPipeline = contentHandlerPipeline;
            _propertyMapper = propertyMapper;
        }

        public TToCreate Build<TFrom, TToCreate>(TFrom @from, bool mapProperties = true)
            where TToCreate : new()
        {
            var to = CreateAndDefaultMap<TFrom, TToCreate>(@from, mapProperties);

            var args = new HandlerArgs<TFrom, TToCreate>(@from, to);
            _contentHandlerPipeline.Raise(args);

            return to;
        }

        public TToCreate Build<TData, TFrom, TToCreate>(TData data, TFrom @from, bool mapProperties = true) where TToCreate : new()
        {
            var to = CreateAndDefaultMap<TFrom, TToCreate>(@from, mapProperties);

            var args = new HandlerArgs<TData, TFrom, TToCreate>(data, @from, to);
            _contentHandlerPipeline.Raise(args);

            return to;
        }

        private TToCreate CreateAndDefaultMap<TFrom, TToCreate>(TFrom @from, bool mapProperties) where TToCreate : new()
        {
            var to = new TToCreate();

            if (mapProperties)
            {
                _propertyMapper.Map(@from, to);
            }

            return to;
        }
    }
}