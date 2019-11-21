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
    public class BillService
    {
        private readonly IConfiguration _config;
        private readonly string _connectionString;
        private readonly BillDal _billDal;
        public BillService(IConfiguration config)
        {
            this._config = config;
            this._connectionString = config["ConnectionStrings:DefaultConnection"];
            this._billDal = new BillDal(_connectionString);
        }

        public bool CreateBill(BillDto bill)
        {
            return _billDal.CreateBill(bill);
        }
        public async Task<IEnumerable<BillDto>> GetAllBIlls()
        {
            return await _billDal.GetAllBills();
        }

        public async Task<BillDto> GetBill(int id)
        {
            return await _billDal.GetBill(id);
        }
       
        public bool UpdateBill(int id, BillDto bill)
        {
            return _billDal.UpdateBill(id, bill);
        }

        public BillViewModel CreateBillRequestModel(IFormCollection collection)
        {
            var billViewModel = new BillViewModel();

            billViewModel.StoreId = Convert.ToInt32(collection["StoreId"]);
            billViewModel.Number = collection["Number"].ToString();
            billViewModel.FactureNumber = collection["FactureNumber"].ToString();
            billViewModel.SupplierId = Convert.ToInt32(collection["SupplierId"]);
            billViewModel.Date = Convert.ToDateTime(collection["Date"]);
            billViewModel.Sum = Convert.ToDecimal(collection["Sum"]);
            billViewModel.DateCreated = DateTime.Now;
            billViewModel.UserCreated = collection["UserCreated"].ToString();

            return billViewModel;
        }
    }
}
