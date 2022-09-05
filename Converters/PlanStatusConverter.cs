using CryptoExchange.Net.Converters;
using Kuna.Net.Enums;

namespace Kuna.Net.Converters
{
    public class PlanStatusConverter : BaseConverter<PlanStatus>
    {
        public PlanStatusConverter() : this(true) { }
        public PlanStatusConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<PlanStatus, string>> Mapping => new List<KeyValuePair<PlanStatus, string>>
        {
            new KeyValuePair<PlanStatus, string>(PlanStatus.Active, "ACTIVE"),
            new KeyValuePair<PlanStatus, string>(PlanStatus.Canceled, "CANCELED"),
            new KeyValuePair<PlanStatus, string>(PlanStatus.Terminated, "TERMINATED")
        };
    }
}
