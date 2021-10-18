using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Attributes
{
    internal class TableAttribute : Attribute
    {
        public string TableName { get; set; }
        public string PropertyName { get; set; }
        public string JoinType { get; set; }
        public TableAttribute()
        {
        }
    }
}
