namespace Handsey.Practices.ViewModelBuilding
{
    using System.Threading.Tasks;

    public interface IViewModelBuilder
    {
        Task<TToCreate> BuildAsync<TFrom, TToCreate>(TFrom @from)
            where TToCreate : new();

        Task<TToCreate> BuildAsync<TData, TFrom, TToCreate>(TData data, TFrom @from)
            where TToCreate : new();
    }
}