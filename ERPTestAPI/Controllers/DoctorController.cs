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
    [RoutePrefix("api/Doctor")]
    public class DoctorController : ApiController
    {
        private string _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        [Route("getDoctors")]
        [HttpGet]
        public async Task<DoctorListModel> getDoctorList()
        {
            DoctorListModel DoctorList = new DoctorListModel();
            DoctorList.data = new List<DoctorModel>();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    string query = "SELECT a.DoctorId, a.DoctorCode, a.DoctorName, a.Gender, a.Age, a.Qualification, a.Address, a.MobileNo, a.EmailAdd," +
                        "a.StateId, b.StateCode, b.StateName, " +
                        "a.CityId, c.CityCode, c.CityName " +
                                " FROM gl_Doctor_m a, gl_state_m b, gl_City_m c where a.StateId = b.StateId and a.CityId = c.CityId";

                    using (var command = new SqlCommand(query, connection))
                    {
                        connection.Open();

                        using (var reader = command.ExecuteReader())
                        {
                            while (await reader.ReadAsync())
                            {
                                DoctorModel obj = new DoctorModel();

                                obj.DoctorId = Convert.ToInt64(reader["DoctorId"]);
                                obj.DoctorCode = Convert.ToString(reader["DoctorCode"]);
                                obj.DoctorName = Convert.ToString(reader["DoctorName"]);
                                obj.Gender = Convert.ToString(reader["Gender"]);
                                obj.Age = Convert.ToInt64(reader["Age"]);
                                obj.Qualification = Convert.ToString(reader["Qualification"]);
                                obj.Address = Convert.ToString(reader["Address"]);
                                obj.MobileNo = Convert.ToString(reader["MobileNo"]);
                                obj.EmailAdd = Convert.ToString(reader["EmailAdd"]);
                                obj.CityId = Convert.ToInt64(reader["CityId"]);
                                obj.CityCode = Convert.ToString(reader["CityCode"]);
                                obj.CityName = Convert.ToString(reader["CityName"]);
                                obj.StateId = Convert.ToInt64(reader["StateId"]);
                                obj.StateCode = Convert.ToString(reader["StateCode"]);
                                obj.StateName = Convert.ToString(reader["StateName"]);

                                DoctorList.data.Add(obj);
                            }

                            DoctorList.status = true;
                            DoctorList.msg = "Success";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                
                Console.WriteLine(ex.Message.ToString());
                Console.WriteLine(ex.StackTrace);

                DoctorList.status = false;
                DoctorList.msg = ex.Message.ToString();
            }
            return DoctorList;
        }   //End public async Task<DoctorListModel> getDoctorList()

        [Route("getFilteredDoctors")]
        [HttpGet]
        public async Task<DoctorListModel> getFilteredDoctorList()
        {
            DoctorListModel DoctorList = new DoctorListModel();
            DoctorList.data = new List<DoctorModel>();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    string query = "SELECT a.DoctorId, a.DoctorCode, a.DoctorName, a.Gender, a.Age, a.Qualification, a.Address, a.MobileNo, a.EmailAdd, a.StateId, b.StateCode, b.StateName, a.CityId, c.CityCode, c.CityName" +
                            "  FROM gl_Doctor_m a, gl_state_m b, gl_City_m c where a.StateId = b.StateId and a.CityId = c.CityId";

                    using (var command = new SqlCommand(query, connection))
                    {
                        connection.Open();

                        using (var reader = command.ExecuteReader())
                        {
                            while (await reader.ReadAsync())
                            {
                                DoctorModel obj = new DoctorModel();

                                obj.DoctorId = Convert.ToInt64(reader["DoctorId"]);
                                obj.DoctorId = Convert.ToInt64(reader["DoctorId"]);
                                obj.DoctorCode = Convert.ToString(reader["DoctorCode"]);
                                obj.DoctorName = Convert.ToString(reader["DoctorName"]);
                                obj.Gender = Convert.ToString(reader["Gender"]);
                                obj.Age = Convert.ToInt64(reader["Age"]);
                                obj.Qualification = Convert.ToString(reader["Qualification"]);
                                obj.Address = Convert.ToString(reader["Address"]);
                                obj.MobileNo = Convert.ToString(reader["MobileNo"]);
                                obj.EmailAdd = Convert.ToString(reader["EmailAdd"]);
                                obj.CityId = Convert.ToInt64(reader["CityId"]);
                                obj.CityCode = Convert.ToString(reader["CityCode"]);
                                obj.CityName = Convert.ToString(reader["CityName"]);
                                obj.StateId = Convert.ToInt64(reader["StateId"]);
                                obj.StateCode = Convert.ToString(reader["StateCode"]);
                                obj.StateName = Convert.ToString(reader["StateName"]);

                                DoctorList.data.Add(obj);
                            }

                            DoctorList.status = true;
                            DoctorList.msg = "Success";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);

                Console.WriteLine(ex.Message.ToString());
                Console.WriteLine(ex.StackTrace);

                DoctorList.status = false;
                DoctorList.msg = ex.Message.ToString();
            }
            return DoctorList;
        }   //End public async Task<DoctorListModel> getFilteredDoctorList()

        [Route("getDoctorDetail")]
        [HttpGet]
        public async Task<DoctorDetailModel> getDoctorDetail(Int64 id)
        {
            DoctorDetailModel DoctorDetail = new DoctorDetailModel();
            DoctorDetail.data = new DoctorModel();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    string query = "SELECT a.DoctorId, a.DoctorCode, a.DoctorName,  a.Gender, a.Age, a.Qualification, a.Address, a.MobileNo, a.EmailAdd, a.StateId, b.StateCode, b.StateName, a.CityId, c.CityCode, c.CityName " +
                                "  FROM gl_Doctor_m a, gl_state_m b, gl_City_m c where a.StateId = b.StateId and a.CityId = c.CityId and a.DoctorId=" + id.ToString();

                    using (var command = new SqlCommand(query, connection))
                    {
                        connection.Open();

                        using (var reader = command.ExecuteReader())
                        {
                            if (await reader.ReadAsync())
                            {
                                DoctorDetail.data.DoctorId = Convert.ToInt64(reader["DoctorId"]);
                                DoctorDetail.data.DoctorCode = Convert.ToString(reader["DoctorCode"]);
                                DoctorDetail.data.DoctorName = Convert.ToString(reader["DoctorName"]);
                                DoctorDetail.data.Gender = Convert.ToString(reader["Gender"]);
                                DoctorDetail.data.Age = Convert.ToInt64(reader["Age"]);
                                DoctorDetail.data.Qualification = Convert.ToString(reader["Qualification"]);
                                DoctorDetail.data.Address = Convert.ToString(reader["Address"]);
                                DoctorDetail.data.MobileNo = Convert.ToString(reader["MobileNo"]);
                                DoctorDetail.data.EmailAdd = Convert.ToString(reader["EmailAdd"]);
                                DoctorDetail.data.CityId = Convert.ToInt64(reader["CityId"]);
                                DoctorDetail.data.CityCode = Convert.ToString(reader["CityCode"]);
                                DoctorDetail.data.CityName = Convert.ToString(reader["CityName"]);
                                DoctorDetail.data.StateId = Convert.ToInt64(reader["StateId"]);
                                DoctorDetail.data.StateCode = Convert.ToString(reader["StateCode"]);
                                DoctorDetail.data.StateName = Convert.ToString(reader["StateName"]);                                
                            }

                            DoctorDetail.status = true;
                            DoctorDetail.msg = "Success";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);

                Console.WriteLine(ex.Message.ToString());
                Console.WriteLine(ex.StackTrace);

                DoctorDetail.status = false;
                DoctorDetail.msg = ex.Message.ToString();
            }
            return DoctorDetail;
        }   //End public async Task<DoctorDetailModel> getDoctorDetail(int64 id)

        [Route("saveDoctor")]
        [HttpPost]
        public async Task<DoctorReturnModel> saveDoctorData([FromBody] DoctorModel data)
        {
            DoctorReturnModel rtnData = new DoctorReturnModel();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    string query = "SP_SaveDoctor";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add("@sDoctorCode", SqlDbType.VarChar, 10).Value = data.DoctorCode;
                        command.Parameters.Add("@sDoctorName", SqlDbType.VarChar, 100).Value = data.DoctorName;
                        command.Parameters.Add("@sGender", SqlDbType.VarChar, 1).Value = data.Gender;
                        command.Parameters.Add("@iAge", SqlDbType.Int).Value = data.Age;
                        command.Parameters.Add("@sQualification", SqlDbType.VarChar, 50).Value = data.Qualification;
                        command.Parameters.Add("@sAddress", SqlDbType.VarChar, 100).Value = data.Address;
                        command.Parameters.Add("@sMobileNo", SqlDbType.VarChar, 15).Value = data.MobileNo;
                        command.Parameters.Add("@sEmailAdd", SqlDbType.VarChar, 50).Value = data.EmailAdd;
                        command.Parameters.Add("@iStateId", SqlDbType.Int).Value = data.StateId;
                        command.Parameters.Add("@iCityId", SqlDbType.Int).Value = data.CityId;

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

                rtnData.status = false;
                rtnData.msg = ex.Message.ToString();
            }

            return rtnData;
        }   //End public async Task<DoctorReturnModel> saveDoctorData([FromBody] DoctorRequestModel data)

        [Route("updateDoctor")]
        [HttpPut]
        public async Task<DoctorReturnModel> updateDoctorData([FromBody] DoctorRequestModel data)
        {
            DoctorReturnModel rtnData = new DoctorReturnModel();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    string query = "SP_UpdateDoctor";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add("@iDoctorId", SqlDbType.Int).Value = data.DoctorId;
                        command.Parameters.Add("@sDoctorCode", SqlDbType.VarChar, 10).Value = data.DoctorCode;
                        command.Parameters.Add("@sDoctorName", SqlDbType.VarChar, 100).Value = data.DoctorName;
                        command.Parameters.Add("@sGender", SqlDbType.VarChar, 1).Value = data.Gender;
                        command.Parameters.Add("@iAge", SqlDbType.Int).Value = data.Age;
                        command.Parameters.Add("@sQualification", SqlDbType.VarChar, 50).Value = data.Qualification;
                        command.Parameters.Add("@sAddress", SqlDbType.VarChar, 100).Value = data.Address;
                        command.Parameters.Add("@sMobileNo", SqlDbType.VarChar, 15).Value = data.MobileNo;
                        command.Parameters.Add("@sEmailAdd", SqlDbType.VarChar, 50).Value = data.EmailAdd;
                        command.Parameters.Add("@iStateId", SqlDbType.Int).Value = data.StateId;
                        command.Parameters.Add("@iCityId", SqlDbType.Int).Value = data.CityId;

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

                rtnData.status = false;
                rtnData.msg = ex.Message.ToString();
            }

            return rtnData;
        }   //End public async Task<DoctorReturnModel> saveDoctorData([FromBody] DoctorRequestModel data)

        [Route("deleteDoctor")]
        [HttpDelete]
        public async Task<DoctorReturnModel> deleteDoctorData(Int64 Id)
        {
            DoctorReturnModel rtnData = new DoctorReturnModel();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    string query = "SP_DeleteDoctor";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add("@iDoctorId", SqlDbType.Int).Value = Id;
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
        }   //End public async Task<DoctorReturnModel> deleteDoctorData(Int64 Id)

    }   //End public class DoctorController : ControllerBase

}   //End namespace ERP_ASPCoreApi.Controllers