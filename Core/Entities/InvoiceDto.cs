using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities
{
    public class InvoiceDto
    {
        public int Id { get; set; }
        public int StoreId { get; set; }
        public string StoreName { get; set; }
        public int BillId { get; set; }
        public string BillNumber { get; set; }
        public string Number { get; set; }
        public decimal Sum { get; set; }
        public DateTime Date { get; set; }
        public DateTime DateCreated { get; set; }
        public string UserCreated { get; set; }
        public int SupplierId { get; set; }
        public string SupplierName { get; set; }

    }
}
