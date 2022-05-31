namespace DAL.Entities.Base
{
    public interface IEntity
    {
        object GetKeyProperty();

        object GetParentKeyProperty();

        string GetTableName();
    }
}