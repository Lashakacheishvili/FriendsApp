using System;
using System.Collections.Generic;
using System.Text;
using static Common.Enums.Enums;

namespace Domain.Models
{
    public class Animal:BaseEntity
    {
        public string Name { get; set; }
        public Gender Gender { get; set; }
    }
}
