using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GalebManager.Models;
using GalebManager.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace GalebManager.Controllers
{
    public class InvoiceController : Controller
    {
        private readonly InvoiceService _invoiceService;
        public InvoiceController(IConfiguration config)
        {
            _invoiceService = new InvoiceService(config);
        }
        public async Task<ActionResult> Index()
        {
            var invoiceDtos = await _invoiceService.GetAllInvoices();
            var invoiceViewModels = invoiceDtos.Select(InvoiceViewModel.MapTo);

            return View(invoiceViewModels);
        }

        public async Task<ActionResult> Details(int id)
        {
            var invoiceDto = await _invoiceService.GetInvoice(id);
            var invoiceViewModel = InvoiceViewModel.MapTo(invoiceDto);

            return View(invoiceViewModel);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }

                var invoiceViewModel = _invoiceService.CreateInvoiceRequestModel(collection);
                var invoiceDto = InvoiceViewModel.MapFrom(invoiceViewModel);

                bool result = _invoiceService.CreateInvoice(invoiceDto);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public async Task<ActionResult> Edit(int id)
        {
            var invoiceDto = await _invoiceService.GetInvoice(id);
            var invoiceViewModel = InvoiceViewModel.MapTo(invoiceDto);

            return View(invoiceViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }

                var invoiceViewModel = _invoiceService.CreateInvoiceRequestModel(collection);
                var invoiceDto = InvoiceViewModel.MapFrom(invoiceViewModel);

                bool result = _invoiceService.UpdateInvoice(id, invoiceDto);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}