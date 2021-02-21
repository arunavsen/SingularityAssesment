using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SingularityAssesment
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public Boolean StatusDelete { get; set; }
        public Boolean StatusLock { get; set; }
    }
}
