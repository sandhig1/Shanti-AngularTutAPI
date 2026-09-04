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
    [RoutePrefix("api/Patient")]
    public class PatientController : ApiController
    {
        private string _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        [Route("getPatients")]
        [HttpGet]
        public async Task<PatientListModel> getPatientList()
        {
            PatientListModel PatientList = new PatientListModel();
            PatientList.data = new List<PatientModel>();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    string query = "SELECT a.PatientId, a.PatientCode, a.PatientName, a.DateOfBirth, a.Gender, a.Age,  a.MobileNo, a.EmailAdd, a.Address, a.BloodGroup, a.Insured, " +
                        "a.StateId, b.StateCode, b.StateName, " +
                        "a.CityId, c.CityCode, c.CityName, " +
                        "a.AreaId, d.AreaCode, d.AreaName " +
                                " FROM gl_Patient_m a, gl_state_m b, gl_City_m c, gl_Area_m d where a.StateId = b.StateId and a.CityId = c.CityId and a.AreaId = d.AreaId";

                    using (var command = new SqlCommand(query, connection))
                    {
                        connection.Open();

                        using (var reader = command.ExecuteReader())
                        {
                            while (await reader.ReadAsync())
                            {
                                PatientModel obj = new PatientModel();

                                obj.PatientId = Convert.ToInt64(reader["PatientId"]);
                                obj.PatientCode = Convert.ToString(reader["PatientCode"]);
                                obj.PatientName = Convert.ToString(reader["PatientName"]);
                                obj.DateOfBirth = Convert.ToDateTime(reader["DateOfBirth"]);
                                obj.Gender = Convert.ToString(reader["Gender"]);
                                obj.Age = Convert.ToInt64(reader["Age"]);
                                obj.MobileNo = Convert.ToString(reader["MobileNo"]);
                                obj.EmailAdd = Convert.ToString(reader["EmailAdd"]);
                                obj.Address = Convert.ToString(reader["Address"]);
                                obj.BloodGroup = Convert.ToString(reader["BloodGroup"]);
                                obj.Insured = Convert.ToBoolean(reader["Insured"]);
                                obj.AreaId = Convert.ToInt64(reader["AreaId"]);
                                obj.AreaCode = Convert.ToString(reader["AreaCode"]);
                                obj.AreaName = Convert.ToString(reader["AreaName"]);
                                obj.CityId = Convert.ToInt64(reader["CityId"]);
                                obj.CityCode = Convert.ToString(reader["CityCode"]);
                                obj.CityName = Convert.ToString(reader["CityName"]);
                                obj.StateId = Convert.ToInt64(reader["StateId"]);
                                obj.StateCode = Convert.ToString(reader["StateCode"]);
                                obj.StateName = Convert.ToString(reader["StateName"]);

                                PatientList.data.Add(obj);
                            }

                            PatientList.status = true;
                            PatientList.msg = "Success";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                
                Console.WriteLine(ex.Message.ToString());
                Console.WriteLine(ex.StackTrace);

                PatientList.status = false;
                PatientList.msg = ex.Message.ToString();
            }
            return PatientList;
        }   //End public async Task<PatientListModel> getPatientList()

        [Route("getFilteredPatients")]
        [HttpGet]
        public async Task<PatientListModel> getFilteredPatientList()
        {
            PatientListModel PatientList = new PatientListModel();
            PatientList.data = new List<PatientModel>();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    string query = "SELECT a.PatientId, a.PatientCode, a.PatientName, a.DateOfBirth, a.Gender, a.Age,  a.MobileNo, a.EmailAdd, a.Address, a.BloodGroup, a.Insured, " +
                        "a.StateId, b.StateCode, b.StateName, " +
                        "a.CityId, c.CityCode, c.CityName, " +
                        "a.AreaId, d.AreaCode, d.AreaName " +
                                " FROM gl_Patient_m a, gl_state_m b, gl_City_m c, gl_Area_m d where a.StateId = b.StateId and a.CityId = c.CityId and a.AreaId = d.AreaId";
                    using (var command = new SqlCommand(query, connection))
                    {
                        connection.Open();

                        using (var reader = command.ExecuteReader())
                        {
                            while (await reader.ReadAsync())
                            {
                                PatientModel obj = new PatientModel();

                                obj.PatientId = Convert.ToInt64(reader["PatientId"]);
                                obj.PatientCode = Convert.ToString(reader["PatientCode"]);
                                obj.PatientName = Convert.ToString(reader["PatientName"]);
                                obj.DateOfBirth = Convert.ToDateTime(reader["DateOfBirth"]);
                                obj.Gender = Convert.ToString(reader["Gender"]);
                                obj.Age = Convert.ToInt64(reader["Age"]);
                                obj.MobileNo = Convert.ToString(reader["MobileNo"]);
                                obj.EmailAdd = Convert.ToString(reader["EmailAdd"]);
                                obj.Address = Convert.ToString(reader["Address"]);
                                obj.BloodGroup = Convert.ToString(reader["BloodGroup"]);
                                obj.Insured = Convert.ToBoolean(reader["Insured"]);
                                obj.AreaId = Convert.ToInt64(reader["AreaId"]);
                                obj.AreaCode = Convert.ToString(reader["AreaCode"]);
                                obj.AreaName = Convert.ToString(reader["AreaName"]);
                                obj.CityId = Convert.ToInt64(reader["CityId"]);
                                obj.CityCode = Convert.ToString(reader["CityCode"]);
                                obj.CityName = Convert.ToString(reader["CityName"]);
                                obj.StateId = Convert.ToInt64(reader["StateId"]);
                                obj.StateCode = Convert.ToString(reader["StateCode"]);
                                obj.StateName = Convert.ToString(reader["StateName"]);

                                PatientList.data.Add(obj);
                            }

                            PatientList.status = true;
                            PatientList.msg = "Success";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);

                Console.WriteLine(ex.Message.ToString());
                Console.WriteLine(ex.StackTrace);

                PatientList.status = false;
                PatientList.msg = ex.Message.ToString();
            }
            return PatientList;
        }   //End public async Task<PatientListModel> getFilteredPatientList()

        [Route("getPatientDetail")]
        [HttpGet]
        public async Task<PatientDetailModel> getPatientDetail(Int64 id)
        {
            PatientDetailModel PatientDetail = new PatientDetailModel();
            PatientDetail.data = new PatientModel();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    string query = "SELECT a.PatientId, a.PatientCode, a.PatientName, a.DateOfBirth, a.Gender, a.Age,  a.MobileNo, a.EmailAdd, a.Address, a.BloodGroup, a.Insured, " +
                        "a.StateId, b.StateCode, b.StateName, " +
                        "a.CityId, c.CityCode, c.CityName, " +
                        "a.AreaId, d.AreaCode, d.AreaName " +
                                " FROM gl_Patient_m a, gl_state_m b, gl_City_m c, gl_Area_m d where a.StateId = b.StateId and a.CityId = c.CityId and a.AreaId = d.AreaId" +id.ToString();

                    using (var command = new SqlCommand(query, connection))
                    {
                        connection.Open();

                        using (var reader = command.ExecuteReader())
                        {
                            if (await reader.ReadAsync())
                            {
                                PatientDetail.data.PatientId = Convert.ToInt64(reader["PatientId"]);
                                PatientDetail.data.PatientCode = Convert.ToString(reader["PatientCode"]);
                                PatientDetail.data.PatientName = Convert.ToString(reader["PatientName"]);
                                PatientDetail.data.DateOfBirth = Convert.ToDateTime(reader["DateOfBirth"]);
                                PatientDetail.data.Gender = Convert.ToString(reader["Gender"]);
                                PatientDetail.data.Age = Convert.ToInt64(reader["Age"]);
                                PatientDetail.data.MobileNo = Convert.ToString(reader["MobileNo"]);
                                PatientDetail.data.EmailAdd = Convert.ToString(reader["EmailAdd"]);
                                PatientDetail.data.Address = Convert.ToString(reader["Address"]);
                                PatientDetail.data.BloodGroup = Convert.ToString(reader["BloodGroup"]);
                                PatientDetail.data.Insured = Convert.ToBoolean(reader["Insured"]);
                                PatientDetail.data.AreaId = Convert.ToInt64(reader["AreaId"]);
                                PatientDetail.data.AreaCode = Convert.ToString(reader["AreaCode"]);
                                PatientDetail.data.AreaName = Convert.ToString(reader["AreaName"]);
                                PatientDetail.data.CityId = Convert.ToInt64(reader["CityId"]);
                                PatientDetail.data.CityCode = Convert.ToString(reader["CityCode"]);
                                PatientDetail.data.CityName = Convert.ToString(reader["CityName"]);
                                PatientDetail.data.StateId = Convert.ToInt64(reader["StateId"]);
                                PatientDetail.data.StateCode = Convert.ToString(reader["StateCode"]);
                                PatientDetail.data.StateName = Convert.ToString(reader["StateName"]);                                
                            }

                            PatientDetail.status = true;
                            PatientDetail.msg = "Success";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);

                Console.WriteLine(ex.Message.ToString());
                Console.WriteLine(ex.StackTrace);

                PatientDetail.status = false;
                PatientDetail.msg = ex.Message.ToString();
            }
            return PatientDetail;
        }   //End public async Task<PatientDetailModel> getPatientDetail(int64 id)

        [Route("savePatient")]
        [HttpPost]
        public async Task<PatientReturnModel> savePatientData([FromBody] PatientRequestModel data)
        {
            PatientReturnModel rtnData = new PatientReturnModel();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    string query = "SP_SavePatient";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add("@sPatientCode", SqlDbType.VarChar, 10).Value = data.PatientCode;
                        command.Parameters.Add("@sPatientName", SqlDbType.VarChar, 100).Value = data.PatientName;
                        command.Parameters.Add("@dDateOfBirth", SqlDbType.Date).Value = data.DateOfBirth;
                        command.Parameters.Add("@sGender", SqlDbType.VarChar, 1).Value = data.Gender;
                        command.Parameters.Add("@iAge", SqlDbType.Int).Value = data.Age;
                        command.Parameters.Add("@sMobileNo", SqlDbType.VarChar, 15).Value = data.MobileNo;
                        command.Parameters.Add("@sEmailAdd", SqlDbType.VarChar, 50).Value = data.EmailAdd;
                        command.Parameters.Add("@sAddress", SqlDbType.VarChar, 100).Value = data.Address;
                        command.Parameters.Add("@sBloodGroup", SqlDbType.VarChar, 5).Value = data.BloodGroup;
                        command.Parameters.Add("@bInsured", SqlDbType.Bit).Value = data.Insured;
                        command.Parameters.Add("@iAreaId", SqlDbType.Int).Value = data.AreaId;
                        command.Parameters.Add("@iCityId", SqlDbType.Int).Value = data.CityId;
                        command.Parameters.Add("@iStateId", SqlDbType.Int).Value = data.StateId;
                        

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
        }   //End public async Task<PatientReturnModel> savePatientData([FromBody] PatientRequestModel data)

        [Route("updatePatient")]
        [HttpPut]
        public async Task<PatientReturnModel> updatePatientData([FromBody] PatientRequestModel data)
        {
            PatientReturnModel rtnData = new PatientReturnModel();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    string query = "SP_UpdatePatient";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add("@iPatientId", SqlDbType.Int).Value = data.PatientId;
                        command.Parameters.Add("@sPatientName", SqlDbType.VarChar, 100).Value = data.PatientName;
                        command.Parameters.Add("@dDateOfBirth", SqlDbType.Date).Value = data.DateOfBirth;
                        command.Parameters.Add("@sGender", SqlDbType.VarChar, 1).Value = data.Gender;
                        command.Parameters.Add("@iAge", SqlDbType.Int).Value = data.Age;
                        command.Parameters.Add("@sMobileNo", SqlDbType.VarChar, 15).Value = data.MobileNo;
                        command.Parameters.Add("@sEmailAdd", SqlDbType.VarChar, 50).Value = data.EmailAdd;
                        command.Parameters.Add("@sAddress", SqlDbType.VarChar, 100).Value = data.Address;
                        command.Parameters.Add("@sBloodGroup", SqlDbType.VarChar, 5).Value = data.BloodGroup;
                        command.Parameters.Add("@bInsured", SqlDbType.Bit).Value = data.Insured;
                        command.Parameters.Add("@iAreaId", SqlDbType.Int).Value = data.AreaId;
                        command.Parameters.Add("@iCityId", SqlDbType.Int).Value = data.CityId;
                        command.Parameters.Add("@iStateId", SqlDbType.Int).Value = data.StateId;

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
        }   //End public async Task<PatientReturnModel> savePatientData([FromBody] PatientRequestModel data)

        [Route("deletePatient")]
        [HttpDelete]
        public async Task<PatientReturnModel> deletePatientData(Int64 Id)
        {
            PatientReturnModel rtnData = new PatientReturnModel();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    string query = "SP_DeletePatient";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add("@iPatientId", SqlDbType.Int).Value = Id;
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
        }   //End public async Task<PatientReturnModel> deletePatientData(Int64 Id)

    }   //End public class PatientController : ControllerBase

}   //End namespace ERP_ASPCoreApi.Controllers