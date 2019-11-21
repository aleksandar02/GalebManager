using Core.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DAL
{
    public class MetaDal
    {
        private readonly string _connectionString;
        public MetaDal(string connectionString)
        {
            this._connectionString = connectionString;
        }

        public IEnumerable<StoreDto> GetStores()
        {
            var stores = new List<StoreDto>();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    string sqlProcedure = "GetStores";
                    var command = new SqlCommand(sqlProcedure, connection);
                    command.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var store = new StoreDto();
                            store = MapToStoreDto(reader);

                            stores.Add(store);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }

            return stores;
        }

        public IEnumerable<SupplierDto> GetSuppliers()
        {
            var suppliers = new List<SupplierDto>();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    string sqlProcedure = "GetSuppliers";
                    var command = new SqlCommand(sqlProcedure, connection);
                    command.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var supplier = new SupplierDto();
                            supplier = MapToSupplierDto(reader);

                            suppliers.Add(supplier);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }

            return suppliers;
        }

        private SupplierDto MapToSupplierDto(SqlDataReader reader)
        {
            var supplier = new SupplierDto();

            supplier.Id = Convert.ToInt32(reader["Id"]);
            supplier.Name = reader["Name"].ToString();
            supplier.Address = reader["Address"].ToString();
            supplier.City = reader["City"].ToString();
            supplier.Telephone = reader["Telephone"].ToString();

            return supplier;
        }

        private StoreDto MapToStoreDto(SqlDataReader reader)
        {
            var store = new StoreDto();

            store.Id = Convert.ToInt32(reader["Id"]);
            store.Name = reader["Name"].ToString();
            store.Address = reader["Address"].ToString();
            store.City = reader["City"].ToString();

            return store;
        }
    }
}
