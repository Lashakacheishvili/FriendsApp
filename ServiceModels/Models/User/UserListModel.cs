using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceModels.Models.User
{
    public class UserResponseModel
    {
        public IEnumerable<UserItemModel>  Users { get; set; }
        public int TotalCount { get; set; }
    }
    public class UserItemModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string URL { get; set; }
    }
    public class UserRequestModel
    {
        public int? Id { get; set; }
        public string Name { get; set; }
    }
}
