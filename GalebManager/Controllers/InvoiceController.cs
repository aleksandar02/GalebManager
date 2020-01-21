using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using GalebManager.Models;
using GalebManager.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace GalebManager.Controllers {
    public class InvoiceController : Controller {
        private readonly InvoiceService _invoiceService;
        public InvoiceController (IConfiguration config) {
            _invoiceService = new InvoiceService (config);
        }
        public async Task<ActionResult> Index () {
            var filter = new InvoiceFilterDto()
            {
                DateFrom = DateTime.Now.AddDays(-30),
                DateTo = DateTime.Now.Date.AddHours(23).AddMinutes(59).AddSeconds(59),
                StoreId = -1,
                SupplierId = -1,
                FactureStatus = -1
            };

            var invoiceDtos = await _invoiceService.SearchInvoices(filter);
            var invoiceViewModels = invoiceDtos.Select(InvoiceViewModel.MapTo);

            return View (invoiceViewModels);
        }

        public async Task<ActionResult> Details (int id) {
            var invoiceDto = await _invoiceService.GetInvoice (id);
            var invoiceViewModel = InvoiceViewModel.MapTo (invoiceDto);
            var invoiceJson = JsonConvert.SerializeObject(invoiceViewModel);

            return Json(invoiceJson);
        }

        public ActionResult Create () {
            return View ();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create (IFormCollection collection) {
            try {
                if (!ModelState.IsValid) {
                    return View ();
                }

                var invoiceViewModel = _invoiceService.CreateInvoiceRequestModel (collection);
                var invoiceDto = InvoiceViewModel.MapFrom (invoiceViewModel);

                bool result = _invoiceService.CreateInvoice (invoiceDto);
                return RedirectToAction (nameof (Index));
            } catch {
                return View ();
            }
        }

        public async Task<ActionResult> Edit (int id) {
            var invoiceDto = await _invoiceService.GetInvoice (id);
            var invoiceViewModel = InvoiceViewModel.MapTo (invoiceDto);

            return View (invoiceViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit (int id, IFormCollection collection) {
            try {
                if (!ModelState.IsValid) {
                    return View ();
                }

                var invoiceViewModel = _invoiceService.CreateInvoiceRequestModel (collection);
                var invoiceDto = InvoiceViewModel.MapFrom (invoiceViewModel);

                bool result = _invoiceService.UpdateInvoice (id, invoiceDto);
                return RedirectToAction (nameof (Index));
            } catch {
                return View ();
            }
        }

        [HttpPost]
        public bool UpdateInvoice(string json)
        {
            try
            {
                var invoiceViewModel = JsonConvert.DeserializeObject<InvoiceViewModel>(json);
                invoiceViewModel.DateCreated = DateTime.Now;
                invoiceViewModel.UserCreated = User.Identity.Name;

                var invoiceDto = InvoiceViewModel.MapFrom(invoiceViewModel);

                bool result = _invoiceService.UpdateInvoice(invoiceDto.Id, invoiceDto);

                return result;
            }
            catch
            {
                return false;
            }
        }

        [HttpPost]
        public bool AddBill([FromRoute]int id, string json)
        {
            try
            {
                var billViewModel = JsonConvert.DeserializeObject<BillViewModel>(json);
                billViewModel.DateCreated = DateTime.Now;
                billViewModel.UserCreated = User.Identity.Name;

                var billDto = BillViewModel.MapFrom(billViewModel);

                bool result = _invoiceService.AddBill(id, billDto);

                return result;
            }
            catch
            {
                return false;
            }
        }

        [HttpGet]
        public async Task<IActionResult> SearchInvoices(string json)
        {
            var filter = JsonConvert.DeserializeObject<InvoiceFilter>(json);
            var filterDto = InvoiceFilter.MapFrom(filter);

            var invoices = await _invoiceService.SearchInvoices(filterDto);

            return Json(invoices);
        }
    }
}