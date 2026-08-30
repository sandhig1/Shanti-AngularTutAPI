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
    [RoutePrefix("api/Clinic")]
    public class ClinicController : ApiController
    {
        private string _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        [Route("getClinics")]
        [HttpGet]
        public async Task<ClinicListModel> getClinicList()
        {
            ClinicListModel ClinicList = new ClinicListModel();
            ClinicList.data = new List<ClinicModel>();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    string query = "Select clinic.ClinicId, clinic.ClinicCode, clinic.ClinicName, clinic.ClinicAddress, "
                                    + " clinic.StateId, states.StateCode, states.StateName, clinic.CityId, city.CityCode, city.CityName,"
	                                + " clinic.MobileNo, clinic.PhoneNo, clinic.EmailAdd"
                                    + " from gl_Clinic_m as clinic, gl_State_m as states, gl_City_m as city"
                                    + " where clinic.StateId = states.StateId and clinic.CityId = city.CityId";

                    using (var command = new SqlCommand(query, connection))
                    {
                        connection.Open();

                        using (var reader = command.ExecuteReader())
                        {
                            while (await reader.ReadAsync())
                            {
                                ClinicModel obj = new ClinicModel();

                                obj.ClinicId = Convert.ToInt64(reader["ClinicId"]);
                                obj.ClinicCode = Convert.ToString(reader["ClinicCode"]);
                                obj.ClinicName = Convert.ToString(reader["ClinicName"]);
                                obj.ClinicAddress = Convert.ToString(reader["ClinicAddress"]);
                                obj.StateId = Convert.ToInt64(reader["StateId"]);
                                obj.StateCode = Convert.ToString(reader["StateCode"]);
                                obj.StateName = Convert.ToString(reader["StateName"]);
                                obj.CityId = Convert.ToInt64(reader["CityId"]);
                                obj.CityCode = Convert.ToString(reader["CityCode"]);
                                obj.CityName = Convert.ToString(reader["CityName"]);

                                obj.MobileNo = Convert.ToString(reader["MobileNo"]);
                                obj.PhoneNo = Convert.ToString(reader["PhoneNo"]);
                                obj.EmailAdd = Convert.ToString(reader["EmailAdd"]);

                                ClinicList.data.Add(obj);
                            }

                            ClinicList.status = true;
                            ClinicList.msg = "Success";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                
                Console.WriteLine(ex.Message.ToString());
                Console.WriteLine(ex.StackTrace);

                ClinicList.status = false;
                ClinicList.msg = ex.Message.ToString();
            }
            return ClinicList;
        }   //End public async Task<ClinicListModel> getClinicList()

        [Route("getFilteredClinics")]
        [HttpGet]
        public async Task<ClinicListModel> getFilteredClinicList()
        {
            ClinicListModel ClinicList = new ClinicListModel();
            ClinicList.data = new List<ClinicModel>();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    string query = "SELECT ClinicId, ClinicCode, ClinicName FROM gl_Clinic_m"; //format(GETDATE(), 'dd-MMM-yyyy') + ' ' + convert(varchar(5), GETDATE(), 108) as ClinicDateFormatted, 

                    using (var command = new SqlCommand(query, connection))
                    {
                        connection.Open();

                        using (var reader = command.ExecuteReader())
                        {
                            while (await reader.ReadAsync())
                            {
                                ClinicModel obj = new ClinicModel();

                                obj.ClinicId = Convert.ToInt64(reader["ClinicId"]);
                                obj.ClinicCode = Convert.ToString(reader["ClinicCode"]);
                                obj.ClinicName = Convert.ToString(reader["ClinicName"]);
                                
                                ClinicList.data.Add(obj);
                            }

                            ClinicList.status = true;
                            ClinicList.msg = "Success";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);

                Console.WriteLine(ex.Message.ToString());
                Console.WriteLine(ex.StackTrace);

                ClinicList.status = false;
                ClinicList.msg = ex.Message.ToString();
            }
            return ClinicList;
        }   //End public async Task<ClinicListModel> getFilteredClinicList()

        [Route("getClinicDetail")]
        [HttpGet]
        public async Task<ClinicDetailModel> getClinicDetail(Int64 id)
        {
            ClinicDetailModel ClinicDetail = new ClinicDetailModel();
            ClinicDetail.data = new ClinicModel();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    string query = "Select clinic.ClinicId, clinic.ClinicCode, clinic.ClinicName, clinic.ClinicAddress, "
                                   + " clinic.StateId, states.StateCode, states.StateName, clinic.CityId, city.CityCode, city.CityName,"
                                   + " clinic.MobileNo, clinic.PhoneNo, clinic.EmailAdd"
                                   + " from gl_Clinic_m as clinic, gl_State_m as states, gl_City_m as city"
                                   + " where clinic.StateId = states.StateId and clinic.CityId = city.CityId" 
                                   + " and  clinic.ClinicId=" + id.ToString();

                    using (var command = new SqlCommand(query, connection))
                    {
                        connection.Open();

                        using (var reader = command.ExecuteReader())
                        {
                            if (await reader.ReadAsync())
                            {
                                ClinicDetail.data.ClinicId = Convert.ToInt64(reader["ClinicId"]);
                                ClinicDetail.data.ClinicCode = Convert.ToString(reader["ClinicCode"]);
                                ClinicDetail.data.ClinicName = Convert.ToString(reader["ClinicName"]);
                                ClinicDetail.data.ClinicAddress = Convert.ToString(reader["ClinicAddress"]);
                                ClinicDetail.data.StateId = Convert.ToInt64(reader["StateId"]);
                                ClinicDetail.data.StateCode = Convert.ToString(reader["StateCode"]);
                                ClinicDetail.data.StateName = Convert.ToString(reader["StateName"]);
                                ClinicDetail.data.CityId = Convert.ToInt64(reader["CityId"]);
                                ClinicDetail.data.CityCode = Convert.ToString(reader["CityCode"]);
                                ClinicDetail.data.CityName = Convert.ToString(reader["CityName"]);

                                ClinicDetail.data.MobileNo = Convert.ToString(reader["MobileNo"]);
                                ClinicDetail.data.PhoneNo = Convert.ToString(reader["PhoneNo"]);
                                ClinicDetail.data.EmailAdd = Convert.ToString(reader["EmailAdd"]);
                            }

                            ClinicDetail.status = true;
                            ClinicDetail.msg = "Success";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);

                Console.WriteLine(ex.Message.ToString());
                Console.WriteLine(ex.StackTrace);

                ClinicDetail.status = false;
                ClinicDetail.msg = ex.Message.ToString();
            }
            return ClinicDetail;
        }   //End public async Task<ClinicDetailModel> getClinicDetail(int64 id)

        [Route("saveClinic")]
        [HttpPost]
        public async Task<ClinicReturnModel> saveClinicData([FromBody] ClinicModel data)
        {
            ClinicReturnModel rtnData = new ClinicReturnModel();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    string query = "SP_SaveClinic";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add("@sClinicCode", SqlDbType.VarChar, 10).Value = data.ClinicCode;
                        command.Parameters.Add("@sClinicName", SqlDbType.VarChar, 100).Value = data.ClinicName;
                        command.Parameters.Add("@sClinicAddress", SqlDbType.VarChar, 100).Value = data.ClinicAddress;
                        command.Parameters.Add("@iStateId", SqlDbType.BigInt).Value = data.StateId;
                        command.Parameters.Add("@iCityId", SqlDbType.BigInt).Value = data.CityId;
                        command.Parameters.Add("@sMobileNo", SqlDbType.VarChar, 15).Value = data.MobileNo;
                        command.Parameters.Add("@sPhoneNo", SqlDbType.VarChar, 20).Value = data.PhoneNo;
                        command.Parameters.Add("@sEmailAdd", SqlDbType.VarChar, 50).Value = data.EmailAdd;

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
        }   //End public async Task<ClinicReturnModel> saveClinicData([FromBody] ClinicModel data)

        [Route("updateClinic")]
        [HttpPut]
        public async Task<ClinicReturnModel> updateClinicData([FromBody] ClinicModel data)
        {
            ClinicReturnModel rtnData = new ClinicReturnModel();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    string query = "SP_UpdateClinic";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add("@iClinicId", SqlDbType.Int).Value = data.ClinicId;

                        command.Parameters.Add("@sClinicCode", SqlDbType.VarChar, 10).Value = data.ClinicCode;
                        command.Parameters.Add("@sClinicName", SqlDbType.VarChar, 100).Value = data.ClinicName;
                        command.Parameters.Add("@sClinicAddress", SqlDbType.VarChar, 100).Value = data.ClinicAddress;
                        command.Parameters.Add("@iStateId", SqlDbType.BigInt).Value = data.StateId;
                        command.Parameters.Add("@iCityId", SqlDbType.BigInt).Value = data.CityId;
                        command.Parameters.Add("@sMobileNo", SqlDbType.VarChar, 15).Value = data.MobileNo;
                        command.Parameters.Add("@sPhoneNo", SqlDbType.VarChar, 20).Value = data.PhoneNo;
                        command.Parameters.Add("@sEmailAdd", SqlDbType.VarChar, 50).Value = data.EmailAdd;

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
        }   //End public async Task<ClinicReturnModel> updateClinicData([FromBody] ClinicModel data)

        [Route("deleteClinic")]
        [HttpDelete]
        public async Task<ClinicReturnModel> deleteClinicData(Int64 Id)
        {
            ClinicReturnModel rtnData = new ClinicReturnModel();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    string query = "SP_DeleteClinic";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add("@iClinicId", SqlDbType.Int).Value = Id;

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
        }   //End public async Task<ClinicReturnModel> deleteClinicData(Int64 Id)

    }   //End public class ClinicController : ControllerBase

}   //End namespace ERP_ASPCoreApi.Controllers