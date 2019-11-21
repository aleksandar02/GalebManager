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
    public class BillController : Controller
    {
        private readonly BillService _billService;
        public BillController(IConfiguration config)
        {
            _billService = new BillService(config);
        }
        public async Task<ActionResult> Index()
        {
            var billDtos = await _billService.GetAllBIlls();
            var billViewModels = billDtos.Select(BillViewModel.MapTo);

            return View(billViewModels);
        }

        public async Task<ActionResult> Details(int id)
        {
            var billDto = await _billService.GetBill(id);
            var billViewModel = BillViewModel.MapTo(billDto);

            return View(billViewModel);
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
                if(!ModelState.IsValid)
                {
                    return View();
                }

                var billViewModel = _billService.CreateBillRequestModel(collection);
                var billDto = BillViewModel.MapFrom(billViewModel);

                bool result = _billService.CreateBill(billDto);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public async Task<ActionResult> Edit(int id)
        {
            var billDto = await _billService.GetBill(id);
            var billViewModel = BillViewModel.MapTo(billDto);

            return View(billViewModel);
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

                var billViewModel = _billService.CreateBillRequestModel(collection);
                var billDto = BillViewModel.MapFrom(billViewModel);

                bool result = _billService.UpdateBill(id, billDto);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}