using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kuna.Net.Objects.Models
{
    public class KunaCurrency
    {
        public long Id { get; set; }

        [JsonProperty("code")]
        public string Asset { get; set; }

        public string Name { get; set; }

        public bool HasMemo { get; set; }

        [JsonProperty("coin")]
        public bool IsCoin { get; set; }

        [JsonProperty("sort_order")]
        public long SortId { get; set; }

        public KunaPrecision Precision { get; set; }
    }
}
