using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities
{
    public class BillDto
    {
        public int Id { get; set; }
        public int StoreId { get; set; }
        public string StoreName { get; set; }
        public string Number { get; set; }
        public string FactureNumber { get; set; }
        public int SupplierId { get; set; }
        public string SupplierName { get; set; }
        public DateTime Date { get; set; }
        public DateTime DateCreated { get; set; }
        public decimal Sum { get; set; }
        public string UserCreated { get; set; }
    }
}
