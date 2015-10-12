using System;

namespace SYDQ.Infrastructure.Helpers
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Class)]
    public class ColumnAttribute : Attribute
    {
        private readonly string _description;
        public ColumnAttribute(string description)
        {
            _description = description;
        }

        public string Description
        {
            get { return _description; }
        }
    }
}
