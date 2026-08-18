using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wine_Festival_project
{
    public interface IConsumables
    {
        void Consume(int quantity);
        bool IsConsumed { get; }
    }
}
