using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now.ToUniversalTime().AddHours(4);
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }
    }
}
