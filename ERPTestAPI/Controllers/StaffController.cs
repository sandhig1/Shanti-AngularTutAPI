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
    [RoutePrefix("api/Staff")]
    public class StaffController : ApiController
    {
        private string _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        [Route("getStaffs")]
        [HttpGet]
        public async Task<StaffListModel> getStaffList()
        {
            StaffListModel StaffList = new StaffListModel();
            StaffList.data = new List<StaffModel>();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    string query = "SELECT a.StaffId, a.StaffCode, a.StaffName, a.DateOfBirth, dbo.GetDateFormat1(a.DateOfBirth) as DateofBirthFormatted, " +
                                    " a.Gender, a.Age,  a.MobileNo, a.EmailAdd, " +
                                    " a.Address, a.JoiningDate, dbo.GetDateFormat1(a.JoiningDate) as JoiningDateFormatted, a.StaffCategoryId, b.StaffCategoryCode, b.StaffCategoryName, a.DepartmentId, c.DepartmentCode, c.DepartmentName, " +
                                    " a.Status, a.ClinicId, d.ClinicCode, d.ClinicName " +
                                    " FROM gl_Staff_m a inner join gl_StaffCategory_m b on a.StaffCategoryId = b.StaffCategoryId " +
                                    " inner join gl_Department_m c on a.DepartmentId = c.DepartmentId " +
                                    " inner join gl_Clinic_m d on a.ClinicId = d.ClinicId ";

                    using (var command = new SqlCommand(query, connection))
                    {
                        connection.Open();

                        using (var reader = command.ExecuteReader())
                        {
                            while (await reader.ReadAsync())
                            {
                                StaffModel obj = new StaffModel();

                                obj.StaffId = Convert.ToInt64(reader["StaffId"]);
                                obj.StaffCode = Convert.ToString(reader["StaffCode"]);
                                obj.StaffName = Convert.ToString(reader["StaffName"]);
                                obj.DateOfBirth = Convert.ToDateTime(reader["DateOfBirth"]);
                                obj.DateOfBirthFormatted = Convert.ToString(reader["DateOfBirthFormatted"]);
                                obj.Gender = Convert.ToString(reader["Gender"]);
                                obj.Age = Convert.ToInt64(reader["Age"]);
                                obj.MobileNo = Convert.ToString(reader["MobileNo"]);
                                obj.EmailAdd = Convert.ToString(reader["EmailAdd"]);
                                obj.Address = Convert.ToString(reader["Address"]);
                                obj.JoiningDate = Convert.ToDateTime(reader["JoiningDate"]);
                                obj.JoiningDateFormatted = Convert.ToString(reader["JoiningDateFormatted"]);
                                obj.StaffCategoryId = Convert.ToInt64(reader["StaffCategoryId"]);
                                obj.StaffCategoryCode = Convert.ToString(reader["StaffCategoryCode"]);
                                obj.StaffCategoryName = Convert.ToString(reader["StaffCategoryName"]);
                                obj.DepartmentId = Convert.ToInt64(reader["DepartmentId"]);
                                obj.DepartmentCode = Convert.ToString(reader["DepartmentCode"]);
                                obj.DepartmentName = Convert.ToString(reader["DepartmentName"]);
                                obj.Status = Convert.ToString(reader["Status"]);
                                obj.ClinicId = Convert.ToInt64(reader["ClinicId"]);
                                obj.ClinicCode = Convert.ToString(reader["ClinicCode"]);
                                obj.ClinicName = Convert.ToString(reader["ClinicName"]);

                                StaffList.data.Add(obj);
                            }

                            StaffList.status = true;
                            StaffList.msg = "Success";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                
                Console.WriteLine(ex.Message.ToString());
                Console.WriteLine(ex.StackTrace);

                StaffList.status = false;
                StaffList.msg = ex.Message.ToString();
            }
            return StaffList;
        }   //End public async Task<StaffListModel> getStaffList()

        [Route("getFilteredStaffs")]
        [HttpGet]
        public async Task<StaffListModel> getFilteredStaffList()
        {
            StaffListModel StaffList = new StaffListModel();
            StaffList.data = new List<StaffModel>();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    string query = "SELECT a.StaffId, a.StaffCode, a.StaffName, a.DateOfBirth, a.Gender, a.Age,  a.MobileNo, a.EmailAdd, a.Address, a.JoiningDate, dbo.GetDateFormat1(a.JoiningDate) as JoiningDateFormatted, a.StaffCategoryId, b.StaffCategoryCode, b.StaffCategoryName, a.DepartmentId, c.DepartmentCode, c.DepartmentName, " +
                        "a.StaffCategoryId, b.StaffCategoryCode, b.StaffCategoryName, " +
                        "a.DepartmentId, c.DepartmentCode, c.DepartmentName, a.Status, a.ClinicId, d.ClinicCode, d.ClinicName " +
                        " FROM gl_Staff_m a, gl_StaffCategory_m b, gl_Department_m c, gl_Clinic_m d where a.StaffCategoryId = b.StaffCategoryId and a.DepartmentId = c.DepartmentId and a.ClinicId = d.ClinicId ";
                    using (var command = new SqlCommand(query, connection))
                    {
                        connection.Open();

                        using (var reader = command.ExecuteReader())
                        {
                            while (await reader.ReadAsync())
                            {
                                StaffModel obj = new StaffModel();

                                obj.StaffId = Convert.ToInt64(reader["StaffId"]);
                                obj.StaffCode = Convert.ToString(reader["StaffCode"]);
                                obj.StaffName = Convert.ToString(reader["StaffName"]);
                                obj.DateOfBirth = Convert.ToDateTime(reader["DateOfBirth"]);
                                obj.Gender = Convert.ToString(reader["Gender"]);
                                obj.Age = Convert.ToInt64(reader["Age"]);
                                obj.MobileNo = Convert.ToString(reader["MobileNo"]);
                                obj.EmailAdd = Convert.ToString(reader["EmailAdd"]);
                                obj.Address = Convert.ToString(reader["Address"]);
                                obj.JoiningDate = Convert.ToDateTime(reader["JoiningDate"]);
                                obj.JoiningDateFormatted = Convert.ToString(reader["JoiningDateFormatted"]);
                                obj.StaffCategoryId = Convert.ToInt64(reader["StaffCategoryId"]);
                                obj.StaffCategoryCode = Convert.ToString(reader["StaffCategoryCode"]);
                                obj.StaffCategoryName = Convert.ToString(reader["StaffCategoryName"]);
                                obj.DepartmentId = Convert.ToInt64(reader["DepartmentId"]);
                                obj.DepartmentCode = Convert.ToString(reader["DepartmentCode"]);
                                obj.DepartmentName = Convert.ToString(reader["DepartmentName"]);
                                obj.Status = Convert.ToString(reader["Status"]);
                                obj.ClinicId = Convert.ToInt64(reader["ClinicId"]);
                                obj.ClinicCode = Convert.ToString(reader["ClinicCode"]);
                                obj.ClinicName = Convert.ToString(reader["ClinicName"]);

                                StaffList.data.Add(obj);
                            }

                            StaffList.status = true;
                            StaffList.msg = "Success";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);

                Console.WriteLine(ex.Message.ToString());
                Console.WriteLine(ex.StackTrace);

                StaffList.status = false;
                StaffList.msg = ex.Message.ToString();
            }
            return StaffList;
        }   //End public async Task<StaffListModel> getFilteredStaffList()

        [Route("getStaffDetail")]
        [HttpGet]
        public async Task<StaffDetailModel> getStaffDetail(Int64 id)
        {
            StaffDetailModel StaffDetail = new StaffDetailModel();
            StaffDetail.data = new StaffModel();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    string query = "SELECT a.StaffId, a.StaffCode, a.StaffName, a.DateOfBirth, dbo.GetDateFormat1(a.DateOfBirth) as DateofBirthFormatted, " +
                                    " a.Gender, a.Age,  a.MobileNo, a.EmailAdd, " +
                                    " a.Address, a.JoiningDate, dbo.GetDateFormat1(a.JoiningDate) as JoiningDateFormatted, a.StaffCategoryId, b.StaffCategoryCode, b.StaffCategoryName, a.DepartmentId, c.DepartmentCode, c.DepartmentName, " +
                                    " a.ClinicId, a.Status " +
                                    " FROM gl_Staff_m a inner join gl_StaffCategory_m b on a.StaffCategoryId = b.StaffCategoryId " +
                                    " inner join gl_Department_m c on a.DepartmentId = c.DepartmentId " +
                                    " inner join gl_Clinic_m d on a.ClinicId = d.ClinicId " +
                                    " Where a.StaffId = " +id.ToString();

                    using (var command = new SqlCommand(query, connection))
                    {
                        connection.Open();

                        using (var reader = command.ExecuteReader())
                        {
                            if (await reader.ReadAsync())
                            {
                                StaffDetail.data.StaffId = Convert.ToInt64(reader["StaffId"]);
                                StaffDetail.data.StaffCode = Convert.ToString(reader["StaffCode"]);
                                StaffDetail.data.StaffName = Convert.ToString(reader["StaffName"]);
                                StaffDetail.data.DateOfBirth = Convert.ToDateTime(reader["DateOfBirth"]);
                                StaffDetail.data.DateOfBirthFormatted = Convert.ToString(reader["DateOfBirthFormatted"]);
                                StaffDetail.data.Gender = Convert.ToString(reader["Gender"]);
                                StaffDetail.data.Age = Convert.ToInt64(reader["Age"]);
                                StaffDetail.data.MobileNo = Convert.ToString(reader["MobileNo"]);
                                StaffDetail.data.EmailAdd = Convert.ToString(reader["EmailAdd"]);
                                StaffDetail.data.Address = Convert.ToString(reader["Address"]);
                                StaffDetail.data.JoiningDate = Convert.ToDateTime(reader["JoiningDate"]);
                                StaffDetail.data.JoiningDateFormatted = Convert.ToString(reader["JoiningDateFormatted"]);
                                StaffDetail.data.StaffCategoryId = Convert.ToInt64(reader["StaffCategoryId"]);
                                StaffDetail.data.StaffCategoryCode = Convert.ToString(reader["StaffCategoryCode"]);
                                StaffDetail.data.StaffCategoryName = Convert.ToString(reader["StaffCategoryName"]);
                                StaffDetail.data.DepartmentId = Convert.ToInt64(reader["DepartmentId"]);
                                StaffDetail.data.DepartmentCode = Convert.ToString(reader["DepartmentCode"]);
                                StaffDetail.data.DepartmentName = Convert.ToString(reader["DepartmentName"]);
                                StaffDetail.data.Status = Convert.ToString(reader["Status"]);
                                StaffDetail.data.ClinicId = Convert.ToInt64(reader["ClinicId"]);
                             
                            }

                            StaffDetail.status = true;
                            StaffDetail.msg = "Success";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);

                Console.WriteLine(ex.Message.ToString());
                Console.WriteLine(ex.StackTrace);

                StaffDetail.status = false;
                StaffDetail.msg = ex.Message.ToString();
            }
            return StaffDetail;
        }   //End public async Task<StaffDetailModel> getStaffDetail(int64 id)

        [Route("saveStaff")]
        [HttpPost]
        public async Task<StaffReturnModel> saveStaffData([FromBody] StaffRequestModel data)
        {
            StaffReturnModel rtnData = new StaffReturnModel();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    string query = "SP_SaveStaff";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add("@sStaffCode", SqlDbType.VarChar, 10).Value = data.StaffCode;
                        command.Parameters.Add("@sStaffName", SqlDbType.VarChar, 50).Value = data.StaffName;
                        command.Parameters.Add("@dDateOfBirth", SqlDbType.Date).Value = data.DateOfBirth;
                        command.Parameters.Add("@sGender", SqlDbType.VarChar, 1).Value = data.Gender;
                        command.Parameters.Add("@iAge", SqlDbType.Int).Value = data.Age;
                        command.Parameters.Add("@sMobileNo", SqlDbType.VarChar, 15).Value = data.MobileNo;
                        command.Parameters.Add("@sEmailAdd", SqlDbType.VarChar, 50).Value = data.EmailAdd;
                        command.Parameters.Add("@sAddress", SqlDbType.VarChar, 100).Value = data.Address;
                        command.Parameters.Add("@dJoiningDate", SqlDbType.Date).Value = data.JoiningDate;
                        command.Parameters.Add("@iStaffCategoryId", SqlDbType.Int).Value = data.StaffCategoryId;
                        command.Parameters.Add("@iDepartmentId", SqlDbType.Int).Value = data.DepartmentId;
                        command.Parameters.Add("@sStatus", SqlDbType.VarChar, 20).Value = data.Status;
                        command.Parameters.Add("@iClinicId", SqlDbType.Int).Value = data.ClinicId;


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
        }   //End public async Task<StaffReturnModel> saveStaffData([FromBody] StaffRequestModel data)

        [Route("updateStaff")]
        [HttpPut]
        public async Task<StaffReturnModel> updateStaffData([FromBody] StaffRequestModel data)
        {
            StaffReturnModel rtnData = new StaffReturnModel();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    string query = "SP_UpdateStaff";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add("@iStaffId", SqlDbType.Int).Value = data.StaffId;
                        command.Parameters.Add("@sStaffCode", SqlDbType.VarChar, 10).Value = data.StaffCode;
                        command.Parameters.Add("@sStaffName", SqlDbType.VarChar, 50).Value = data.StaffName;
                        command.Parameters.Add("@dDateOfBirth", SqlDbType.Date).Value = data.DateOfBirth;
                        command.Parameters.Add("@sGender", SqlDbType.VarChar, 1).Value = data.Gender;
                        command.Parameters.Add("@iAge", SqlDbType.Int).Value = data.Age;
                        command.Parameters.Add("@sMobileNo", SqlDbType.VarChar, 15).Value = data.MobileNo;
                        command.Parameters.Add("@sEmailAdd", SqlDbType.VarChar, 50).Value = data.EmailAdd;
                        command.Parameters.Add("@sAddress", SqlDbType.VarChar, 100).Value = data.Address;
                        command.Parameters.Add("@dJoiningDate", SqlDbType.Date).Value = data.JoiningDate;
                        command.Parameters.Add("@iStaffCategoryId", SqlDbType.Int).Value = data.StaffCategoryId;
                        command.Parameters.Add("@iDepartmentId", SqlDbType.Int).Value = data.DepartmentId;
                        command.Parameters.Add("@sStatus", SqlDbType.VarChar, 20).Value = data.Status;
                        command.Parameters.Add("@iClinicId", SqlDbType.Int).Value = data.ClinicId;

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
        }   //End public async Task<StaffReturnModel> saveStaffData([FromBody] StaffRequestModel data)

        [Route("deleteStaff")]
        [HttpDelete]
        public async Task<StaffReturnModel> deleteStaffData(Int64 Id)
        {
            StaffReturnModel rtnData = new StaffReturnModel();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    string query = "SP_DeleteStaff";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add("@iStaffId", SqlDbType.Int).Value = Id;
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
        }   //End public async Task<StaffReturnModel> deleteStaffData(Int64 Id)

    }   //End public class StaffController : ControllerBase

}   //End namespace ERP_ASPCoreApi.Controllers