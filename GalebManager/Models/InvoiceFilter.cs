using Core.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GalebManager.Models
{
    public class InvoiceFilter
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

        public static InvoiceFilterDto MapFrom(InvoiceFilter invoiceFilter)
        {
            var invoiceFilterDto = new InvoiceFilterDto();

            invoiceFilterDto.StoreId = invoiceFilter.StoreId;
            invoiceFilterDto.SupplierId = invoiceFilter.SupplierId;
            invoiceFilterDto.DateFrom = invoiceFilter.DateFrom;
            invoiceFilterDto.DateTo = invoiceFilter.DateTo;
            invoiceFilterDto.FactureStatus = invoiceFilter.FactureStatus;

            return invoiceFilterDto;
        }
    }
}
