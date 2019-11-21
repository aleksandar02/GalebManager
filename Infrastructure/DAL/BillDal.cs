using Core.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DAL
{
    public class BillDal
    {
        private readonly string _connectionString;
        public BillDal(string connectionString)
        {
            this._connectionString = connectionString;
        }

        public bool CreateBill(BillDto bill)
        {
            bool result = true;

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    string sqlProcedure = "CreateBill";
                    var command = new SqlCommand(sqlProcedure, connection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@StoreId", bill.StoreId);
                    command.Parameters.AddWithValue("@Number", bill.Number);
                    command.Parameters.AddWithValue("@FactureNumber", bill.FactureNumber);
                    command.Parameters.AddWithValue("@SupplierId", bill.SupplierId);
                    command.Parameters.AddWithValue("@Date", bill.Date);
                    command.Parameters.AddWithValue("@Sum", bill.Sum);
                    command.Parameters.AddWithValue("@DateCreated", bill.DateCreated);
                    command.Parameters.AddWithValue("@UserCreated", bill.UserCreated);

                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                result = false;
            }

            return result;
        }

        public Task<BillDto> GetBillByFactureNumber(string factureNumber)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<BillDto>> GetAllBills()
        {
            var bills = new List<BillDto>();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    string sqlProcedure = "GetAllBills";
                    var command = new SqlCommand(sqlProcedure, connection);
                    command.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (await reader.ReadAsync().ConfigureAwait(false))
                        {
                            var bill = new BillDto();
                            bill = MapToBillDto(reader);

                            bills.Add(bill);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }

            return bills;
        }

        public async Task<BillDto> GetBill(int id)
        {
            var bill = new BillDto();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    string sqlProcedure = "GetBill";
                    var command = new SqlCommand(sqlProcedure, connection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@Id", id);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (await reader.ReadAsync().ConfigureAwait(false))
                        {
                            bill = MapToBillDto(reader);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }

            return bill;
        }

        public bool UpdateBill(int id, BillDto bill)
        {
            bool result = true;

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    string sqlProcedure = "UpdateBill";
                    var command = new SqlCommand(sqlProcedure, connection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@Id", id);
                    command.Parameters.AddWithValue("@StoreId", bill.StoreId);
                    command.Parameters.AddWithValue("@Number", bill.Number);
                    command.Parameters.AddWithValue("@FactureNumber", bill.FactureNumber);
                    command.Parameters.AddWithValue("@SupplierId", bill.SupplierId);
                    command.Parameters.AddWithValue("@Date", bill.Date);
                    command.Parameters.AddWithValue("@Sum", bill.Sum);
                    command.Parameters.AddWithValue("@DateCreated", bill.DateCreated);
                    command.Parameters.AddWithValue("@UserCreated", bill.UserCreated);

                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                result = false;
            }

            return result;
        }

        private BillDto MapToBillDto(SqlDataReader reader)
        {
            var bill = new BillDto();

            bill.Id = Convert.ToInt32(reader["Id"]);
            bill.StoreId = Convert.ToInt32(reader["StoreId"]);
            bill.StoreName = reader["StoreName"].ToString();
            bill.Number = reader["Number"].ToString();
            bill.FactureNumber = reader["FactureNumber"].ToString();
            bill.Sum = Convert.ToDecimal(reader["Sum"]);
            bill.Date = Convert.ToDateTime(reader["Date"]);
            bill.SupplierId = Convert.ToInt32(reader["SupplierId"]);
            bill.SupplierName = reader["SupplierName"].ToString();
            bill.DateCreated = Convert.ToDateTime(reader["DateCreated"]);
            bill.UserCreated = reader["UserCreated"].ToString();

            return bill;
        }
    }
}
