using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kuna.Net.Enums
{
    public enum OrderStatus
    {
        Pending,
        New,
        PartiallyFilled,
        Canceling,
        Canceled,
        Filled
    }
}
