using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kuna.Net.Objects.Models
{
    public class KunaOrderBook
    {
        public IEnumerable<KunaOrderBookEntry> Asks { get; set; }

        public IEnumerable<KunaOrderBookEntry> Bids { get; set; }
    }
}
