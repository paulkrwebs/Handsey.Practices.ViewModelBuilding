namespace Handsey.Practices.ViewModelBuilding
{
    public interface IViewModelBuilder
    {
        TToCreate Build<TFrom, TToCreate>(TFrom @from, bool mapProperties = true)
            where TToCreate : new();

        TToCreate Build<TData, TFrom, TToCreate>(TData data, TFrom @from, bool mapProperties = true)
            where TToCreate : new();
    }
}
