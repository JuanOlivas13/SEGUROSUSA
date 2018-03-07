using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEGUROSUSA
{
    public interface Listener
    {
        void listen(string evento);
    }
}
