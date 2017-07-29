using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.Visitors
{
    public struct VisitorSettings
    {
        public int AvailableMemory { get; set; }
        public bool IsCycled { get; set; }

        public VisitorSettings(int availableMemory, bool isCycled)
        {
            AvailableMemory = availableMemory;
            IsCycled = isCycled;
        }
    }
}
