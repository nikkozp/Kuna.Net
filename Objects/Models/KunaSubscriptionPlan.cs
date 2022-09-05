using Kuna.Net.Converters;
using Kuna.Net.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kuna.Net.Objects.Models
{
    public class KunaSubscriptionPlan
    {
        public int Id { get; set; }

        [JsonProperty("plan_id")]
        public PlanType Type { get;set; }

        [JsonProperty("plan_name")]
        public string Name { get; set; }

        [JsonProperty("ends_at")]
        public DateTime TerminatedTime { get; set; }

        [JsonConverter(typeof(PlanStatusConverter))]
        public PlanStatus Status { get; set; }
    }
}
