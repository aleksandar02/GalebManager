using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GalebManager.Models
{
    public class StoreViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }

        public static StoreViewModel MapTo (StoreDto storeDto)
        {
            var store = new StoreViewModel();

            store.Id = storeDto.Id;
            store.Name = storeDto.Name;
            store.Address = storeDto.Address;
            store.City = storeDto.City;

            return store;
        }
    }
}
