using Core.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DAL
{
    public class InvoiceDal
    {
        private readonly string _connectionString;
        public InvoiceDal(string connectionString)
        {
            this._connectionString = connectionString;
        }

        public bool CreateInvoice(InvoiceDto invoice)
        {
            bool result = true;

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    string sqlProcedure = "CreateInvoice";
                    var command = new SqlCommand(sqlProcedure, connection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@StoreId", invoice.StoreId);
                    command.Parameters.AddWithValue("@Number", invoice.Number);
                    command.Parameters.AddWithValue("@BillNumber", invoice.BillNumber);
                    command.Parameters.AddWithValue("@BillId", invoice.BillId);
                    command.Parameters.AddWithValue("@SupplierId", invoice.SupplierId);
                    command.Parameters.AddWithValue("@Date", invoice.Date);
                    command.Parameters.AddWithValue("@Sum", invoice.Sum);
                    command.Parameters.AddWithValue("@DateCreated", invoice.DateCreated);
                    command.Parameters.AddWithValue("@UserCreated", invoice.UserCreated);


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

        public Task<InvoiceDto> GetInvoicesByBillNumber(string billNumber)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<InvoiceDto>> GetAllInvoices()
        {
            var invoices = new List<InvoiceDto>();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    string sqlProcedure = "GetAllInvoices";
                    var command = new SqlCommand(sqlProcedure, connection);
                    command.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (await reader.ReadAsync().ConfigureAwait(false))
                        {
                            var invoice = new InvoiceDto();
                            invoice = MapToInvoiceDto(reader);

                            invoices.Add(invoice);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }

            return invoices;
        }

        public async Task<InvoiceDto> GetInvoice(int id)
        {
            var invoice = new InvoiceDto();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    string sqlProcedure = "GetInvoice";
                    var command = new SqlCommand(sqlProcedure, connection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@Id", id);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (await reader.ReadAsync().ConfigureAwait(false))
                        {
                            invoice = MapToInvoiceDto(reader);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }

            return invoice;
        }
        public bool UpdateInvoice(int id, InvoiceDto invoice)
        {
            bool result = true;

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    string sqlProcedure = "UpdateInvoice";
                    var command = new SqlCommand(sqlProcedure, connection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@Id", id);
                    command.Parameters.AddWithValue("@StoreId", invoice.StoreId);
                    command.Parameters.AddWithValue("@Number", invoice.Number);
                    command.Parameters.AddWithValue("@BillNumber", invoice.BillNumber);
                    command.Parameters.AddWithValue("@BillId", invoice.BillId);
                    command.Parameters.AddWithValue("@SupplierId", invoice.SupplierId);
                    command.Parameters.AddWithValue("@Date", invoice.Date);
                    command.Parameters.AddWithValue("@Sum", invoice.Sum);
                    command.Parameters.AddWithValue("@DateCreated", invoice.DateCreated);
                    command.Parameters.AddWithValue("@UserCreated", invoice.UserCreated);

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

        private InvoiceDto MapToInvoiceDto(SqlDataReader reader)
        {
            var invoice = new InvoiceDto();

            invoice.Id = Convert.ToInt32(reader["Id"]);
            invoice.StoreId = Convert.ToInt32(reader["StoreId"]);
            invoice.StoreName = reader["StoreName"].ToString();
            invoice.Number = reader["Number"].ToString();
            invoice.BillNumber = reader["BillNumber"].ToString();
            invoice.BillId = Convert.ToInt32(reader["BillId"]);
            invoice.SupplierId = Convert.ToInt32(reader["SupplierId"]);
            invoice.SupplierName = reader["SupplierName"].ToString();
            invoice.Date = Convert.ToDateTime(reader["Date"]);
            invoice.DateCreated = Convert.ToDateTime(reader["DateCreated"]);
            invoice.Sum = Convert.ToDecimal(reader["Sum"]);
            invoice.UserCreated = reader["UserCreated"].ToString();

            return invoice;
        }

    }
}
