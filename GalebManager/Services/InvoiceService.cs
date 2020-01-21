using Core.Entities;
using GalebManager.Models;
using Infrastructure.DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GalebManager.Services
{
    public class InvoiceService
    {
        private readonly IConfiguration _config;
        private readonly string _connectionString;
        private readonly InvoiceDal _invoiceDal;
        public InvoiceService(IConfiguration config)
        {
            this._config = config;
            this._connectionString = config["ConnectionStrings:DefaultConnection"];
            this._invoiceDal = new InvoiceDal(_connectionString);
        }
        public bool CreateInvoice(InvoiceDto invoice)
        {
            return _invoiceDal.CreateInvoice(invoice);
        }
        public async Task<IEnumerable<InvoiceDto>> GetAllInvoices()
        {
            return await _invoiceDal.GetAllInvoices();
        }

        public async Task<IEnumerable<InvoiceDto>> SearchInvoices(InvoiceFilterDto filter)
        {
            return await _invoiceDal.SearchInvoices(filter);
        }

        public async Task<InvoiceDto> GetInvoice(int id)
        {
            return await _invoiceDal.GetInvoice(id);
        }

        public async Task<InvoiceDto> GetInvoicesByBillNumber(string billNumber)
        {
            return await _invoiceDal.GetInvoicesByBillNumber(billNumber);
        }

        public bool UpdateInvoice(int id, InvoiceDto invoice)
        {
            return _invoiceDal.UpdateInvoice(id, invoice);
        }

        public InvoiceViewModel CreateInvoiceRequestModel(IFormCollection collection)
        {
            var invoice = new InvoiceViewModel();

            invoice.StoreId = Convert.ToInt32(collection["StoreId"]);
            invoice.Number = collection["Number"].ToString();
            invoice.BillNumber = collection["BillNumber"].ToString();
            invoice.SupplierId = Convert.ToInt32(collection["SupplierId"]);
            invoice.Date = Convert.ToDateTime(collection["Date"]);
            invoice.Sum = Convert.ToDecimal(collection["Sum"]);
            invoice.DateCreated = DateTime.Now;
            invoice.UserCreated = collection["UserCreated"].ToString();

            return invoice;
        }

        public bool AddBill(int id, BillDto billDto)
        {
            return _invoiceDal.AddBill(id, billDto);
        }
    }
}
