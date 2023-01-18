using Newtonsoft.Json;

namespace SalePortal.Entities.NovaPoshtaEntities
{
    public class Datum
    {
        [JsonProperty("SiteKey")]
        public string SiteKey { get; set; }

        [JsonProperty("Description")]
        public string Description { get; set; }

        [JsonProperty("DescriptionRu")]
        public string DescriptionRu { get; set; }

        [JsonProperty("ShortAddress")]
        public string ShortAddress { get; set; }

        [JsonProperty("ShortAddressRu")]
        public string ShortAddressRu { get; set; }

        [JsonProperty("Phone")]
        public string Phone { get; set; }

        [JsonProperty("TypeOfWarehouse")]
        public string TypeOfWarehouse { get; set; }

        [JsonProperty("Ref")]
        public string Ref { get; set; }

        [JsonProperty("Number")]
        public string Number { get; set; }

        [JsonProperty("CityRef")]
        public string CityRef { get; set; }

        [JsonProperty("CityDescription")]
        public string CityDescription { get; set; }

        [JsonProperty("CityDescriptionRu")]
        public string CityDescriptionRu { get; set; }

        [JsonProperty("SettlementRef")]
        public string SettlementRef { get; set; }

        [JsonProperty("SettlementDescription")]
        public string SettlementDescription { get; set; }

        [JsonProperty("SettlementAreaDescription")]
        public string SettlementAreaDescription { get; set; }

        [JsonProperty("SettlementRegionsDescription")]
        public string SettlementRegionsDescription { get; set; }

        [JsonProperty("SettlementTypeDescription")]
        public string SettlementTypeDescription { get; set; }

        [JsonProperty("SettlementTypeDescriptionRu")]
        public string SettlementTypeDescriptionRu { get; set; }

        [JsonProperty("Longitude")]
        public string Longitude { get; set; }

        [JsonProperty("Latitude")]
        public string Latitude { get; set; }

        [JsonProperty("PostFinance")]
        public string PostFinance { get; set; }

        [JsonProperty("BicycleParking")]
        public string BicycleParking { get; set; }

        [JsonProperty("PaymentAccess")]
        public string PaymentAccess { get; set; }

        [JsonProperty("POSTerminal")]
        public string POSTerminal { get; set; }

        [JsonProperty("InternationalShipping")]
        public string InternationalShipping { get; set; }

        [JsonProperty("SelfServiceWorkplacesCount")]
        public string SelfServiceWorkplacesCount { get; set; }

        [JsonProperty("TotalMaxWeightAllowed")]
        public string TotalMaxWeightAllowed { get; set; }

        [JsonProperty("PlaceMaxWeightAllowed")]
        public string PlaceMaxWeightAllowed { get; set; }

        [JsonProperty("SendingLimitationsOnDimensions")]
        public SendingLimitationsOnDimensions SendingLimitationsOnDimensions { get; set; }

        [JsonProperty("ReceivingLimitationsOnDimensions")]
        public ReceivingLimitationsOnDimensions ReceivingLimitationsOnDimensions { get; set; }

        [JsonProperty("Reception")]
        public Reception Reception { get; set; }

        [JsonProperty("Delivery")]
        public Delivery Delivery { get; set; }

        [JsonProperty("Schedule")]
        public Schedule Schedule { get; set; }

        [JsonProperty("DistrictCode")]
        public string DistrictCode { get; set; }

        [JsonProperty("WarehouseStatus")]
        public string WarehouseStatus { get; set; }

        [JsonProperty("WarehouseStatusDate")]
        public string WarehouseStatusDate { get; set; }

        [JsonProperty("CategoryOfWarehouse")]
        public string CategoryOfWarehouse { get; set; }

        [JsonProperty("Direct")]
        public string Direct { get; set; }

        [JsonProperty("RegionCity")]
        public string RegionCity { get; set; }

        [JsonProperty("WarehouseForAgent")]
        public string WarehouseForAgent { get; set; }

        [JsonProperty("GeneratorEnabled")]
        public string GeneratorEnabled { get; set; }

        [JsonProperty("MaxDeclaredCost")]
        public string MaxDeclaredCost { get; set; }

        [JsonProperty("WorkInMobileAwis")]
        public string WorkInMobileAwis { get; set; }

        [JsonProperty("DenyToSelect")]
        public string DenyToSelect { get; set; }

        [JsonProperty("CanGetMoneyTransfer")]
        public string CanGetMoneyTransfer { get; set; }

        [JsonProperty("OnlyReceivingParcel")]
        public string OnlyReceivingParcel { get; set; }

        [JsonProperty("PostMachineType")]
        public string PostMachineType { get; set; }

        [JsonProperty("PostalCodeUA")]
        public string PostalCodeUA { get; set; }

        [JsonProperty("WarehouseIndex")]
        public string WarehouseIndex { get; set; }

        [JsonProperty("PostomatFor")]
        public string PostomatFor { get; set; }
    }
}
