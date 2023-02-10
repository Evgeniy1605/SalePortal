using Newtonsoft.Json;

namespace SalePortal.Entities.NovaPoshtaEntities
{
    public class ReceivingLimitationsOnDimensions
    {
        [JsonProperty("Width")]
        public int Width { get; set; }

        [JsonProperty("Height")]
        public int Height { get; set; }

        [JsonProperty("Length")]
        public int Length { get; set; }
    }
}
