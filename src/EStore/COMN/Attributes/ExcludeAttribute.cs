namespace COMN.Attributes
{
    [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = true)]
    public sealed class ExcludeAttribute : Attribute
    {
        public string[] NavigationPropertyPath { get; set; }

        public ExcludeAttribute(params string[] navigationPropertyPath)
        {
            this.NavigationPropertyPath = navigationPropertyPath;
        }
    }
}