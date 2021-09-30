using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceModels
{
    public class BaseRequestModel
    {
        public bool TakeAll { get; set; }
        public int Limit { get; set; } = 20;
        public int Page { get; set; } = 1;
        public int OffSet {
            get
            {
                return Limit * (Page - 1);
            }
            set => OffSet = value;
        }
       
    }
}
