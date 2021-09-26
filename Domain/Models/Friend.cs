using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class Friend:BaseEntity
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public int ReceiverUserId { get; set; }
        public User ReceiverUser { get; set; }
    }
}
