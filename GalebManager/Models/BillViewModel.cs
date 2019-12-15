using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GalebManager.Models
{
    public class BillViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Prodavnica")]
        [Required(ErrorMessage = "Obavezno polje!")]
        public int StoreId { get; set; }
        
        public string StoreName { get; set; }
        [Display(Name = "Broj racuna")]
        [Required(ErrorMessage = "Obavezno polje!")]
        public string Number { get; set; }
        [Display(Name = "Broj fakture")]
        public string FactureNumber { get; set; }
        [Display(Name = "Dobavljac")]
        [Required(ErrorMessage = "Obavezno polje!")]
        public int SupplierId { get; set; }
        public string SupplierName { get; set; }
        [Display(Name = "Datum")]
        [Required(ErrorMessage = "Obavezno polje!")]
        public DateTime Date { get; set; }
        public DateTime DateCreated { get; set; }
        [Display(Name = "Iznos")]
        [Required(ErrorMessage = "Obavezno polje!")]
        public decimal Sum { get; set; }
        public string UserCreated { get; set; }

        public static BillViewModel MapTo(BillDto billDto)
        {
            var bill = new BillViewModel();

            bill.Id = billDto.Id;
            bill.StoreId = billDto.StoreId;
            bill.StoreName = billDto.StoreName;
            bill.Number = billDto.Number;
            bill.FactureNumber = billDto.FactureNumber;
            bill.SupplierId = billDto.SupplierId;
            bill.SupplierName = billDto.SupplierName;
            bill.Date = billDto.Date;
            bill.DateCreated = billDto.DateCreated;
            bill.Sum = billDto.Sum;
            bill.UserCreated = billDto.UserCreated;

            return bill;
        }

        public static BillDto MapFrom(BillViewModel billViewModel)
        {
            var billDto = new BillDto();

            billDto.Id = billViewModel.Id;
            billDto.StoreId = billViewModel.StoreId;
            billDto.StoreName = billViewModel.StoreName;
            billDto.Number = billViewModel.Number;
            billDto.FactureNumber = billViewModel.FactureNumber;
            billDto.SupplierId = billViewModel.SupplierId;
            billDto.SupplierName = billViewModel.SupplierName;
            billDto.Date = billViewModel.Date;
            billDto.DateCreated = billViewModel.DateCreated;
            billDto.Sum = billViewModel.Sum;
            billDto.UserCreated = billViewModel.UserCreated;

            return billDto;
        }
    }
}
