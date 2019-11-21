using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GalebManager.Models;
using Infrastructure.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace GalebManager.Controllers
{
    public class MetaController : Controller
    {
        private readonly IConfiguration _config;
        private readonly string _connectionString;
        private readonly MetaDal _metaDal;
        public MetaController(IConfiguration config)
        {
            this._config = config;
            this._connectionString = config["ConnectionStrings:DefaultConnection"];
            this._metaDal = new MetaDal(_connectionString);
        }
        public IActionResult GetStores()
        {
            var storeDtos = _metaDal.GetStores();
            var storeViewModels = storeDtos.Select(StoreViewModel.MapTo);
            var storeJSON = JsonConvert.SerializeObject(storeViewModels);

            return Json(storeJSON);
        }

        public IActionResult GetSuppliers()
        {
            var supplierDtos = _metaDal.GetSuppliers();
            var supplierViewModels = supplierDtos.Select(SupplierViewModel.MapTo);
            var supplierJSON = JsonConvert.SerializeObject(supplierViewModels);

            return Json(supplierJSON);
        }
    }
}