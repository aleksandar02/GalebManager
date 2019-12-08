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
            var filter = new BillFilterDto()
            {
                DateFrom = DateTime.Now.AddDays(-30),
                DateTo = DateTime.Now.Date.AddHours(23).AddMinutes(59).AddSeconds(59),
                StoreId = -1,
                SupplierId = -1,
                FactureStatus = -1
            };

            var billDtos = await _billService.SearchBills(filter);
            var billViewModels = billDtos.Select(BillViewModel.MapTo);
           
            return View(billViewModels);
        }

        public async Task<IActionResult> Details(int id)
        {
            var billDto = await _billService.GetBill(id);
            var billViewModel = BillViewModel.MapTo(billDto);
            var billJSON = JsonConvert.SerializeObject(billViewModel);

            return Json(billJSON);
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
        public bool UpdateFacturedBill(string json)
        {
            try
            {
                //if (!ModelState.IsValid)
                //{
                //    return false;
                //}

                var billViewModel = JsonConvert.DeserializeObject<BillViewModel>(json);
                billViewModel.DateCreated = DateTime.Now;
                billViewModel.UserCreated = User.Identity.Name;

                var billDto = BillViewModel.MapFrom(billViewModel);

                bool result = _billService.UpdateBill(billDto.Id, billDto);

                return result;
            }
            catch
            {
                return false;
            }
        }

        [HttpGet]
        public async Task<IActionResult> SearchBills(string json)
        {
            var filter = JsonConvert.DeserializeObject<BillFilter>(json);
            var filterDto = BillFilter.MapFrom(filter);

            var bills = await _billService.SearchBills(filterDto);

            return Json(bills);
        }
    }
}