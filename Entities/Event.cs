using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenerateLambda.Entities
{
    public class Event
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ProductType { get; set; }
        public string QualityType { get; set; }
        public DateTime DateTime { get; set; }
    }
}
