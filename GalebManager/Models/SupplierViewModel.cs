using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GalebManager.Models
{
    public class SupplierViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Telephone { get; set; }

        public static SupplierViewModel MapTo(SupplierDto supplierDto)
        {
            var supplier = new SupplierViewModel();

            supplier.Id = supplierDto.Id;
            supplier.Name = supplierDto.Name;
            supplier.Address = supplierDto.Address;
            supplier.City = supplierDto.City;
            supplier.Telephone = supplierDto.Telephone;

            return supplier;
        }
    }
}
