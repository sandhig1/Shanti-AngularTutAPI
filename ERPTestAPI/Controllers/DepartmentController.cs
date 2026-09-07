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
    [RoutePrefix("api/Department")]
    public class DepartmentController : ApiController
    {
        private string _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        [Route("getDepartment")]
        [HttpGet]
        public async Task<DepartmentListModel> getDepartmentList()
        {
            DepartmentListModel DepartmentList = new DepartmentListModel();
            DepartmentList.data = new List<DepartmentModel>();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    string query = "SELECT DepartmentId, DepartmentCode, DepartmentName, Description FROM gl_Department_m ";
                                    

                    using (var command = new SqlCommand(query, connection))
                    {
                        connection.Open();

                        using (var reader = command.ExecuteReader())
                        {
                            while (await reader.ReadAsync())
                            {
                                DepartmentModel obj = new DepartmentModel();
                                
                                obj.DepartmentId = Convert.ToInt64(reader["DepartmentId"]);
                                obj.DepartmentCode = Convert.ToString(reader["DepartmentCode"]);
                                obj.DepartmentName = Convert.ToString(reader["DepartmentName"]);
                                obj.Description = Convert.ToString(reader["Description"]);

                                DepartmentList.data.Add(obj);
                            }

                            DepartmentList.status = true;
                            DepartmentList.msg = "Success";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                
                Console.WriteLine(ex.Message.ToString());
                Console.WriteLine(ex.StackTrace);

                DepartmentList.status = false;
                DepartmentList.msg = ex.Message.ToString();
            }
            return DepartmentList;
        }   //End public async Task<DepartmentListModel> getDepartmentList()

        [Route("getFilteredDepartment")]
        [HttpGet]
        public async Task<DepartmentListModel> getFilteredDepartmentList()
        {
            DepartmentListModel DepartmentList = new DepartmentListModel();
            DepartmentList.data = new List<DepartmentModel>();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    string query = "SELECT DepartmentId, DepartmentCode, DepartmentName, Description FROM gl_Department_m ";

                    using (var command = new SqlCommand(query, connection))
                    {
                        connection.Open();

                        using (var reader = command.ExecuteReader())
                        {
                            while (await reader.ReadAsync())
                            {
                                DepartmentModel obj = new DepartmentModel();
                                
                                obj.DepartmentId = Convert.ToInt64(reader["DepartmentId"]);
                                obj.DepartmentCode = Convert.ToString(reader["DepartmentCode"]);
                                obj.DepartmentName = Convert.ToString(reader["DepartmentName"]);
                                obj.Description = Convert.ToString(reader["Description"]);

                                DepartmentList.data.Add(obj);
                            }

                            DepartmentList.status = true;
                            DepartmentList.msg = "Success";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);

                Console.WriteLine(ex.Message.ToString());
                Console.WriteLine(ex.StackTrace);

                DepartmentList.status = false;
                DepartmentList.msg = ex.Message.ToString();
            }
            return DepartmentList;
        }   //End public async Task<DepartmentListModel> getFilteredDepartmentList()

        [Route("getDepartmentDetail")]
        [HttpGet]
        public async Task<DepartmentDetailModel> getDepartmentDetail(Int64 id)
        {
            DepartmentDetailModel DepartmentDetail = new DepartmentDetailModel();
            DepartmentDetail.data = new DepartmentModel();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    string query = "SELECT DepartmentId, DepartmentCode, DepartmentName, Description FROM gl_Department_m where DepartmentId=" + id.ToString();

                    using (var command = new SqlCommand(query, connection))
                    {
                        connection.Open();

                        using (var reader = command.ExecuteReader())
                        {
                            if (await reader.ReadAsync())
                            {
                                
                                DepartmentDetail.data.DepartmentId = Convert.ToInt64(reader["DepartmentId"]);
                                DepartmentDetail.data.DepartmentCode = Convert.ToString(reader["DepartmentCode"]);
                                DepartmentDetail.data.DepartmentName = Convert.ToString(reader["DepartmentName"]);
                                DepartmentDetail.data.Description = Convert.ToString(reader["Description"]);
                            }

                            DepartmentDetail.status = true;
                            DepartmentDetail.msg = "Success";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);

                Console.WriteLine(ex.Message.ToString());
                Console.WriteLine(ex.StackTrace);

                DepartmentDetail.status = false;
                DepartmentDetail.msg = ex.Message.ToString();
            }
            return DepartmentDetail;
        }   //End public async Task<DepartmentDetailModel> getDepartmentDetail(int64 id)

        [Route("saveDepartment")]
        [HttpPost]
        public async Task<DepartmentReturnModel> saveDepartmentData([FromBody] DepartmentRequestModel data)
        {
            DepartmentReturnModel rtnData = new DepartmentReturnModel();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    string query = "SP_SaveDepartment";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add("@sDepartmentCode", SqlDbType.VarChar, 10).Value = data.DepartmentCode;
                        command.Parameters.Add("@sDepartmentName", SqlDbType.VarChar, 50).Value = data.DepartmentName;
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
        }   //End public async Task<DepartmentReturnModel> saveDepartmentData([FromBody] DepartmentRequestModel data)

        [Route("updateDepartment")]
        [HttpPut]
        public async Task<DepartmentReturnModel> updateDepartmentData([FromBody] DepartmentRequestModel data)
        {
            DepartmentReturnModel rtnData = new DepartmentReturnModel();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    string query = "SP_UpdateDepartment";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add("@iDepartmentId", SqlDbType.Int).Value = data.DepartmentId;
                        command.Parameters.Add("@sDepartmentCode", SqlDbType.VarChar, 10).Value = data.DepartmentCode;
                        command.Parameters.Add("@sDepartmentName", SqlDbType.VarChar, 50).Value = data.DepartmentName;
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
        }   //End public async Task<DepartmentReturnModel> saveDepartmentData([FromBody] DepartmentRequestModel data)

        [Route("deleteDepartment")]
        [HttpDelete]
        public async Task<DepartmentReturnModel> deleteDepartmentData(Int64 Id)
        {
            DepartmentReturnModel rtnData = new DepartmentReturnModel();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    string query = "SP_DeleteDepartment";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add("@iDepartmentId", SqlDbType.Int).Value = Id;
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
        }   //End public async Task<DepartmentReturnModel> deleteDepartmentData(Int64 Id)

    }   //End public class DepartmentController : ControllerBase

}   //End namespace ERP_ASPCoreApi.Controllers