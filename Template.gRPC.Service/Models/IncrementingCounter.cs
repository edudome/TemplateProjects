using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Template.gRPC.Service.Models
{
    public class IncrementingCounter
    {
        public int Count { get; private set; }

        public void Increment(int amount)
        {
            Count += amount;
        }

        public void SetEmpty()
        {
            Count = 0;
        }
    }
}
