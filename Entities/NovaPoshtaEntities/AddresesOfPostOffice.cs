using Newtonsoft.Json;

namespace SalePortal.Entities.NovaPoshtaEntities
{
    public class AddresesOfPostOffice
    {
        [JsonProperty("success")]
        public bool success { get; set; }

        [JsonProperty("data")]
        public List<Datum> data { get; set; }

        [JsonProperty("errors")]
        public List<object> errors { get; set; }

        [JsonProperty("warnings")]
        public List<object> warnings { get; set; }

        [JsonProperty("info")]
        public Info info { get; set; }

        [JsonProperty("messageCodes")]
        public List<object> messageCodes { get; set; }

        [JsonProperty("errorCodes")]
        public List<object> errorCodes { get; set; }

        [JsonProperty("warningCodes")]
        public List<object> warningCodes { get; set; }

        [JsonProperty("infoCodes")]
        public List<object> infoCodes { get; set; }
    }
}
