using COMN.Attributes;
using COMN.Extensions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities.Base
{
    public abstract class BaseEntity : IEntity
    {
        public object GetKeyProperty()
        {
            return this.PropertyFindValueByAttribute(typeof(KeyAttribute));
        }

        public object GetParentKeyProperty()
        {
            return this.PropertyFindValueByAttribute(typeof(ParentKeyAttribute));
        }

        public string GetTableName()
        {
            return ((TableAttribute)this.GetType().GetCustomAttributes(typeof(TableAttribute), false).FirstOrDefault()).Name;
        }
    }
}