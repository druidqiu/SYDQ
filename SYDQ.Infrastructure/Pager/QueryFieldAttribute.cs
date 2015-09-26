using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SYDQ.Infrastructure.Pager
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class QueryFieldAttribute : Attribute
    {

    }
}
