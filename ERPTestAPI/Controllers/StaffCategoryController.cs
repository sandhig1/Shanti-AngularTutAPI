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
    [RoutePrefix("api/StaffCategory")]
    public class StaffCategoryController : ApiController
    {
        private string _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        [Route("getStaffCategory")]
        [HttpGet]
        public async Task<StaffCategoryListModel> getStaffCategoryList()
        {
            StaffCategoryListModel StaffCategoryList = new StaffCategoryListModel();
            StaffCategoryList.data = new List<StaffCategoryModel>();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    string query = "SELECT StaffCategoryId, StaffCategoryCode, StaffCategoryName, Description FROM gl_StaffCategory_m ";
                                    

                    using (var command = new SqlCommand(query, connection))
                    {
                        connection.Open();

                        using (var reader = command.ExecuteReader())
                        {
                            while (await reader.ReadAsync())
                            {
                                StaffCategoryModel obj = new StaffCategoryModel();
                                
                                obj.StaffCategoryId = Convert.ToInt64(reader["StaffCategoryId"]);
                                obj.StaffCategoryCode = Convert.ToString(reader["StaffCategoryCode"]);
                                obj.StaffCategoryName = Convert.ToString(reader["StaffCategoryName"]);
                                obj.Description = Convert.ToString(reader["Description"]);

                                StaffCategoryList.data.Add(obj);
                            }

                            StaffCategoryList.status = true;
                            StaffCategoryList.msg = "Success";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                
                Console.WriteLine(ex.Message.ToString());
                Console.WriteLine(ex.StackTrace);

                StaffCategoryList.status = false;
                StaffCategoryList.msg = ex.Message.ToString();
            }
            return StaffCategoryList;
        }   //End public async Task<StaffCategoryListModel> getStaffCategoryList()

        [Route("getFilteredStaffCategory")]
        [HttpGet]
        public async Task<StaffCategoryListModel> getFilteredStaffCategoryList()
        {
            StaffCategoryListModel StaffCategoryList = new StaffCategoryListModel();
            StaffCategoryList.data = new List<StaffCategoryModel>();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    string query = "SELECT StaffCategoryId, StaffCategoryCode, StaffCategoryName, Description FROM gl_StaffCategory_m ";

                    using (var command = new SqlCommand(query, connection))
                    {
                        connection.Open();

                        using (var reader = command.ExecuteReader())
                        {
                            while (await reader.ReadAsync())
                            {
                                StaffCategoryModel obj = new StaffCategoryModel();
                                
                                obj.StaffCategoryId = Convert.ToInt64(reader["StaffCategoryId"]);
                                obj.StaffCategoryCode = Convert.ToString(reader["StaffCategoryCode"]);
                                obj.StaffCategoryName = Convert.ToString(reader["StaffCategoryName"]);
                                obj.Description = Convert.ToString(reader["Description"]);

                                StaffCategoryList.data.Add(obj);
                            }

                            StaffCategoryList.status = true;
                            StaffCategoryList.msg = "Success";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);

                Console.WriteLine(ex.Message.ToString());
                Console.WriteLine(ex.StackTrace);

                StaffCategoryList.status = false;
                StaffCategoryList.msg = ex.Message.ToString();
            }
            return StaffCategoryList;
        }   //End public async Task<StaffCategoryListModel> getFilteredStaffCategoryList()

        [Route("getStaffCategoryDetail")]
        [HttpGet]
        public async Task<StaffCategoryDetailModel> getStaffCategoryDetail(Int64 id)
        {
            StaffCategoryDetailModel StaffCategoryDetail = new StaffCategoryDetailModel();
            StaffCategoryDetail.data = new StaffCategoryModel();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    string query = "SELECT StaffCategoryId, StaffCategoryCode, StaffCategoryName, Description FROM gl_StaffCategory_m where StaffCategoryId=" + id.ToString();

                    using (var command = new SqlCommand(query, connection))
                    {
                        connection.Open();

                        using (var reader = command.ExecuteReader())
                        {
                            if (await reader.ReadAsync())
                            {
                                
                                StaffCategoryDetail.data.StaffCategoryId = Convert.ToInt64(reader["StaffCategoryId"]);
                                StaffCategoryDetail.data.StaffCategoryCode = Convert.ToString(reader["StaffCategoryCode"]);
                                StaffCategoryDetail.data.StaffCategoryName = Convert.ToString(reader["StaffCategoryName"]);
                                StaffCategoryDetail.data.Description = Convert.ToString(reader["Description"]);
                            }

                            StaffCategoryDetail.status = true;
                            StaffCategoryDetail.msg = "Success";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);

                Console.WriteLine(ex.Message.ToString());
                Console.WriteLine(ex.StackTrace);

                StaffCategoryDetail.status = false;
                StaffCategoryDetail.msg = ex.Message.ToString();
            }
            return StaffCategoryDetail;
        }   //End public async Task<StaffCategoryDetailModel> getStaffCategoryDetail(int64 id)

        [Route("saveStaffCategory")]
        [HttpPost]
        public async Task<StaffCategoryReturnModel> saveStaffCategoryData([FromBody] StaffCategoryRequestModel data)
        {
            StaffCategoryReturnModel rtnData = new StaffCategoryReturnModel();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    string query = "SP_SaveStaffCategory";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add("@sStaffCategoryCode", SqlDbType.VarChar, 10).Value = data.StaffCategoryCode;
                        command.Parameters.Add("@sStaffCategoryName", SqlDbType.VarChar, 50).Value = data.StaffCategoryName;
                        command.Parameters.Add("@sDescription", SqlDbType.VarChar, 1000).Value = data.Description;
                        

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
        }   //End public async Task<StaffCategoryReturnModel> saveStaffCategoryData([FromBody] StaffCategoryRequestModel data)

        [Route("updateStaffCategory")]
        [HttpPut]
        public async Task<StaffCategoryReturnModel> updateStaffCategoryData([FromBody] StaffCategoryRequestModel data)
        {
            StaffCategoryReturnModel rtnData = new StaffCategoryReturnModel();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    string query = "SP_UpdateStaffCategory";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add("@iStaffCategoryId", SqlDbType.Int).Value = data.StaffCategoryId;
                        command.Parameters.Add("@sStaffCategoryCode", SqlDbType.VarChar, 10).Value = data.StaffCategoryCode;
                        command.Parameters.Add("@sStaffCategoryName", SqlDbType.VarChar, 50).Value = data.StaffCategoryName;
                        command.Parameters.Add("@sDescription", SqlDbType.VarChar, 20).Value = data.Description;

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
        }   //End public async Task<StaffCategoryReturnModel> saveStaffCategoryData([FromBody] StaffCategoryRequestModel data)

        [Route("deleteStaffCategory")]
        [HttpDelete]
        public async Task<StaffCategoryReturnModel> deleteStaffCategoryData(Int64 Id)
        {
            StaffCategoryReturnModel rtnData = new StaffCategoryReturnModel();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    string query = "SP_DeleteStaffCategory";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add("@iStaffCategoryId", SqlDbType.Int).Value = Id;
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
        }   //End public async Task<StaffCategoryReturnModel> deleteStaffCategoryData(Int64 Id)

    }   //End public class StaffCategoryController : ControllerBase

}   //End namespace ERP_ASPCoreApi.Controllers