using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GalebManager.Models
{
    public class InvoiceViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Prodavnica")]
        [Required(ErrorMessage = "Obavezno polje!")]
        public int StoreId { get; set; }
        public string StoreName { get; set; }
        [Display(Name = "Broj racuna")]
        public string BillNumber { get; set; }
        [Display(Name = "Broj fakture")]
        [Required(ErrorMessage = "Obavezno polje!")] 
        public string Number { get; set; }
        [Display(Name = "Iznos")]
        [Required(ErrorMessage = "Obavezno polje!")] 
        public decimal Sum { get; set; }
        [Display(Name = "Datum")]
        [Required(ErrorMessage = "Obavezno polje!")] 
        public DateTime Date { get; set; }
        public DateTime DateCreated { get; set; }
        public string UserCreated { get; set; }
        [Display(Name = "Dobavljac")]
        [Required(ErrorMessage = "Obavezno polje!")] 
        public int SupplierId { get; set; }
        public string SupplierName { get; set; }

        public static InvoiceViewModel MapTo(InvoiceDto invoiceDto)
        {
            var invoice = new InvoiceViewModel();

            invoice.Id = invoiceDto.Id;
            invoice.StoreId = invoiceDto.StoreId;
            invoice.StoreName = invoiceDto.StoreName;
            invoice.BillNumber = invoiceDto.BillNumber;
            invoice.Number = invoiceDto.Number;
            invoice.Sum = invoiceDto.Sum;
            invoice.Date = invoiceDto.Date;
            invoice.DateCreated = invoiceDto.DateCreated;
            invoice.UserCreated = invoiceDto.UserCreated;
            invoice.SupplierId = invoiceDto.SupplierId;
            invoice.SupplierName = invoiceDto.SupplierName;

            return invoice;
        }

        public static InvoiceDto MapFrom(InvoiceViewModel invoiceViewModel)
        {
            var invoiceDto = new InvoiceDto();

            invoiceDto.Id = invoiceViewModel.Id;
            invoiceDto.StoreId = invoiceViewModel.StoreId;
            invoiceDto.StoreName = invoiceViewModel.StoreName;
            invoiceDto.BillNumber = invoiceViewModel.BillNumber;
            invoiceDto.Number = invoiceViewModel.Number;
            invoiceDto.Sum = invoiceViewModel.Sum;
            invoiceDto.Date = invoiceViewModel.Date;
            invoiceDto.DateCreated = invoiceViewModel.DateCreated;
            invoiceDto.UserCreated = invoiceViewModel.UserCreated;
            invoiceDto.SupplierId = invoiceViewModel.SupplierId;
            invoiceDto.SupplierName = invoiceViewModel.SupplierName;

            return invoiceDto;
        }
    }
}
