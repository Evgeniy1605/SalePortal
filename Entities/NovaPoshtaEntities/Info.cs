using Newtonsoft.Json;

namespace SalePortal.Entities.NovaPoshtaEntities
{
    public class Info
    {
        [JsonProperty("totalCount")]
        public int totalCount { get; set; }
    }
}
