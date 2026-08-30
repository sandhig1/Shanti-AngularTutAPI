using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.Data.SqlClient;
using System.Data;
using System.Web.Http;
using System.Configuration;

using ERPTestAPI.Models;

using System.Web.Http.Cors;


namespace ERPTestAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/Enquiry")]
    //[Authorize]
    public class EnquiryController : ApiController
    {
        private string _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        [Route("getEnquiries")]
        [HttpGet]
        public async Task<EnquiryListModel> getEnquiryList()
        {
            EnquiryListModel EnquiryList = new EnquiryListModel();
            EnquiryList.data = new List<EnquiryModel>();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    string query = "SELECT EnquiryId, EnquiryNo, EnquiryDate, CustomerName, CustAddress, PhoneNo, EmailAdd, EnquiryDetail, Remarks FROM gl_Enquiry_m"; //format(GETDATE(), 'dd-MMM-yyyy') + ' ' + convert(varchar(5), GETDATE(), 108) as EnquiryDateFormatted, 

                    using (var command = new SqlCommand(query, connection))
                    {
                        connection.Open();

                        using (var reader = command.ExecuteReader())
                        {
                            while (await reader.ReadAsync())
                            {
                                EnquiryModel obj = new EnquiryModel();

                                obj.EnquiryId = Convert.ToInt64(reader["EnquiryId"]);
                                obj.EnquiryNo = Convert.ToString(reader["EnquiryNo"]);
                                obj.EnquiryDate = Convert.ToDateTime(reader["EnquiryDate"]);
                                //obj.EnquiryDateFormatted = Convert.ToString(reader["EnquiryDateFormatted"]);
                                obj.CustomerName = Convert.ToString(reader["CustomerName"]);
                                obj.CustomerAddress = Convert.ToString(reader["CustAddress"]);
                                obj.PhoneNo = Convert.ToString(reader["PhoneNo"]);
                                obj.EmailAdd = Convert.ToString(reader["EmailAdd"]);
                                obj.EnquiryDetail = Convert.ToString(reader["EnquiryDetail"]);

                                EnquiryList.data.Add(obj);
                            }

                            EnquiryList.status = true;
                            EnquiryList.msg = "Success";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                
                Console.WriteLine(ex.Message.ToString());
                Console.WriteLine(ex.StackTrace);

                EnquiryList.status = false;
                EnquiryList.msg = ex.Message.ToString();
            }
            return EnquiryList;
        }   //End public async Task<EnquiryListModel> getEnquiryList()

        [Route("getFilteredEnquiries")]
        [HttpGet]
        public async Task<EnquiryListModel> getFilteredEnquiryList()
        {
            EnquiryListModel EnquiryList = new EnquiryListModel();
            EnquiryList.data = new List<EnquiryModel>();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    string query = "SELECT EnquiryId, EnquiryNo, EnquiryDate, CustomerName, CustAddress, PhoneNo, EmailAdd, EnquiryDetail, Remarks FROM gl_Enquiry_m"; //format(GETDATE(), 'dd-MMM-yyyy') + ' ' + convert(varchar(5), GETDATE(), 108) as EnquiryDateFormatted, 

                    using (var command = new SqlCommand(query, connection))
                    {
                        connection.Open();

                        using (var reader = command.ExecuteReader())
                        {
                            while (await reader.ReadAsync())
                            {
                                EnquiryModel obj = new EnquiryModel();

                                obj.EnquiryId = Convert.ToInt64(reader["EnquiryId"]);
                                obj.EnquiryNo = Convert.ToString(reader["EnquiryNo"]);
                                obj.EnquiryDate = Convert.ToDateTime(reader["EnquiryDate"]);
                                //obj.EnquiryDateFormatted = Convert.ToString(reader["EnquiryDateFormatted"]);
                                obj.CustomerName = Convert.ToString(reader["CustomerName"]);
                                obj.CustomerAddress = Convert.ToString(reader["CustAddress"]);
                                obj.PhoneNo = Convert.ToString(reader["PhoneNo"]);
                                obj.EmailAdd = Convert.ToString(reader["EmailAdd"]);
                                obj.EnquiryDetail = Convert.ToString(reader["EnquiryDetail"]);

                                EnquiryList.data.Add(obj);
                            }

                            EnquiryList.status = true;
                            EnquiryList.msg = "Success";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);

                Console.WriteLine(ex.Message.ToString());
                Console.WriteLine(ex.StackTrace);

                EnquiryList.status = false;
                EnquiryList.msg = ex.Message.ToString();
            }
            return EnquiryList;
        }   //End public async Task<EnquiryListModel> getEnquiryList()

        [Route("getEnquiryDetail")]
        [HttpGet]
        public async Task<EnquiryDetailModel> getEnquiryDetail(Int64 id)
        {
            EnquiryDetailModel EnquiryDetail = new EnquiryDetailModel();
            EnquiryDetail.data = new EnquiryModel();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    string query = "SELECT EnquiryId, EnquiryNo, EnquiryDate, CustomerName, CustAddress, PhoneNo, EmailAdd, EnquiryDetail, Remarks FROM gl_Enquiry_m where EnquiryId=" + id.ToString(); //format(GETDATE(), 'dd-MMM-yyyy') + ' ' + convert(varchar(5), GETDATE(), 108) as EnquiryDateFormatted, 

                    using (var command = new SqlCommand(query, connection))
                    {
                        connection.Open();

                        using (var reader = command.ExecuteReader())
                        {
                            if (await reader.ReadAsync())
                            {
                                EnquiryDetail.data.EnquiryId = Convert.ToInt64(reader["EnquiryId"]);
                                EnquiryDetail.data.EnquiryNo = Convert.ToString(reader["EnquiryNo"]);
                                EnquiryDetail.data.EnquiryDate = Convert.ToDateTime(reader["EnquiryDate"]);
                                //obj.EnquiryDateFormatted = Convert.ToString(reader["EnquiryDateFormatted"]);
                                EnquiryDetail.data.CustomerName = Convert.ToString(reader["CustomerName"]);
                                EnquiryDetail.data.CustomerAddress = Convert.ToString(reader["CustAddress"]);
                                EnquiryDetail.data.PhoneNo = Convert.ToString(reader["PhoneNo"]);
                                EnquiryDetail.data.EmailAdd = Convert.ToString(reader["EmailAdd"]);
                                EnquiryDetail.data.EnquiryDetail = Convert.ToString(reader["EnquiryDetail"]);
                            }

                            EnquiryDetail.status = true;
                            EnquiryDetail.msg = "Success";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);

                Console.WriteLine(ex.Message.ToString());
                Console.WriteLine(ex.StackTrace);

                EnquiryDetail.status = false;
                EnquiryDetail.msg = ex.Message.ToString();
            }
            return EnquiryDetail;
        }   //End public async Task<EnquiryDetailModel> getEnquiryDetail(int64 id)

        [Route("saveEnquiry")]
        [HttpPost]
        public async Task<EnquiryReturnModel> saveEnquiryData([FromBody] EnquiryModel data)
        {
            EnquiryReturnModel rtnData = new EnquiryReturnModel();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    string query = "SP_InsertEnquiry";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add("@sCustomerName", SqlDbType.VarChar, 100).Value = data.CustomerName;
                        command.Parameters.Add("@sCustomerAdd", SqlDbType.VarChar, 200).Value = data.CustomerAddress;
                        command.Parameters.Add("@sPhoneNo", SqlDbType.VarChar, 50).Value = data.PhoneNo;
                        command.Parameters.Add("@sEmailAdd", SqlDbType.VarChar, 100).Value = data.EmailAdd;
                        command.Parameters.Add("@sEnquiryDetail", SqlDbType.VarChar, int.MaxValue).Value = data.EnquiryDetail;

                        connection.Open();

                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                rtnData.status = Boolean.Parse(reader["Flag"].ToString());
                                rtnData.msg = reader["Msg"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);

                Console.WriteLine(ex.Message.ToString());
                Console.WriteLine(ex.StackTrace);
            }

            return rtnData;
        }   //End public async Task<EnquiryReturnModel> saveEnquiryData([FromBody] EnquiryModel data)

        [Route("updateEnquiry")]
        [HttpPut]
        public async Task<EnquiryReturnModel> updateEnquiryData([FromBody] EnquiryModel data)
        {
            EnquiryReturnModel rtnData = new EnquiryReturnModel();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    string query = "SP_UpdateEnquiry"; 

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add("@iEnquiryId", SqlDbType.Int).Value = data.EnquiryId;
                        command.Parameters.Add("@sCustomerName", SqlDbType.VarChar, 100).Value = data.CustomerName;
                        command.Parameters.Add("@sCustomerAdd", SqlDbType.VarChar, 200).Value = data.CustomerAddress;
                        command.Parameters.Add("@sPhoneNo", SqlDbType.VarChar, 50).Value = data.PhoneNo;
                        command.Parameters.Add("@sEmailAdd", SqlDbType.VarChar, 100).Value = data.EmailAdd;
                        command.Parameters.Add("@sEnquiryDetail", SqlDbType.VarChar, int.MaxValue).Value = data.EnquiryDetail;

                        connection.Open();

                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                rtnData.status = Boolean.Parse(reader["Flag"].ToString());
                                rtnData.msg = reader["Msg"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);

                Console.WriteLine(ex.Message.ToString());
                Console.WriteLine(ex.StackTrace);
            }

            return rtnData;
        }   //End public async Task<EnquiryReturnModel> saveEnquiryData([FromBody] EnquiryModel data)

        [Route("deleteEnquiry")]
        [HttpDelete]
        public async Task<EnquiryReturnModel> deleteEnquiryData(Int64 Id)
        {
            EnquiryReturnModel rtnData = new EnquiryReturnModel();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    string query = "SP_DeleteEnquiry"; 

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add("@iEnquiryId", SqlDbType.Int).Value = Id;
                        connection.Open();

                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                rtnData.status = Boolean.Parse(reader["Flag"].ToString());
                                rtnData.msg = reader["Msg"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);

                Console.WriteLine(ex.Message.ToString());
                Console.WriteLine(ex.StackTrace);
            }

            return rtnData;
        }   //End public async Task<EnquiryReturnModel> deleteEnquiryData(Int64 Id)

    }   //End public class EnquiryController : ControllerBase

}   //End namespace ERP_ASPCoreApi.Controllers