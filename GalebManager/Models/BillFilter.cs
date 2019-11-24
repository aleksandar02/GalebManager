using Core.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GalebManager.Models
{
    public class BillFilter
    {
        [JsonProperty(PropertyName = "storeId")]
        public int StoreId { get; set; }
        [JsonProperty(PropertyName = "supplierId")]
        public int SupplierId { get; set; }
        [JsonProperty(PropertyName = "dateFrom")]
        public DateTime DateFrom { get; set; }
        [JsonProperty(PropertyName = "dateTo")]
        public DateTime DateTo { get; set; }
        [JsonProperty(PropertyName = "factureStatus")]
        public int FactureStatus { get; set; }

        public static BillFilterDto MapFrom(BillFilter bill)
        {
            var billFilterDto = new BillFilterDto();

            billFilterDto.StoreId = bill.StoreId;
            billFilterDto.SupplierId = bill.SupplierId;
            billFilterDto.DateFrom = bill.DateFrom;
            billFilterDto.DateTo = bill.DateTo;
            billFilterDto.FactureStatus = bill.FactureStatus;

            return billFilterDto;
        }
    }
}
