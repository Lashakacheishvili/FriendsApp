using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Attributes
{
    public sealed class JoinTableAttribute : Attribute
    {
        public JoinTableAttribute()
        {
        }
        public string TableName { get; set; }
        public string PropertyName { get; set; }
        public string TargetPropertyName { get; set; }
        public string JoinType { get; set; }
        public string ColumnName { get; set; }
    }
}
