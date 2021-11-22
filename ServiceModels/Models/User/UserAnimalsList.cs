using Common.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceModels.Models.User
{
    public class UserAnimalsListRequest
    {
        public int UserId { get; set; }
    }
    public class UserAnimalsListResponse
    {
        public IEnumerable<UserAnimalsListItems> Animals { get; set; }
        public int TotalCount { get; set; }
    }
    public class UserAnimalsListItems
    {
        [JoinTableAttribute(JoinType ="LEFT",PropertyName = "AnimalId", TableName = "Animals", TargetPropertyName = "Id", ColumnName ="Name")]
        public string AnimalName { get; set; } = string.Empty;
        [JoinTableAttribute(JoinType = "LEFT", PropertyName = "AnimalId", TableName = "Animals", TargetPropertyName = "Id", ColumnName = "Gender")]
        public string AnimalGender { get; set; } = string.Empty;
    }
}
