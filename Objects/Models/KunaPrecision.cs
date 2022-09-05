using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kuna.Net.Objects.Models
{
    public class KunaPrecision
    {
        [JsonProperty("real")]
        public long Real { get; set; }

        [JsonProperty("trade")]
        public long Trade { get; set; }
    }
}
