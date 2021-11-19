using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class UserAnimals : BaseEntity
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public int AnimalId { get; set; }
        public Animal Animal { get; set; }
    }
}
