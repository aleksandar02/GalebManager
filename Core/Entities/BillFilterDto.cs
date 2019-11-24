using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities
{
    public class BillFilterDto
    {
        public int StoreId { get; set; }
        public int SupplierId { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public int FactureStatus { get; set; }
    }
}
