using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceModels.Models.User
{
    public class AddFriendRequestModel
    {
        public int UserId { get; set; }
        public int ReceiverUserId { get; set; }
    }
}
