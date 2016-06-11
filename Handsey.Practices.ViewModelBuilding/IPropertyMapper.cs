namespace Handsey.Practices.ViewModelBuilding
{
    public interface IPropertyMapper
    {
        void Map<TFrom, TTo>(TFrom @from, TTo to);
    }
}
