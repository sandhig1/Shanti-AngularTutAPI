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
    [RoutePrefix("api/Status")]
    public class StatusController : ApiController
    {
        private string _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        [Route("getStatus")]
        [HttpGet]
        public async Task<StatusListModel> getStatusList()
        {
            StatusListModel StatusList = new StatusListModel();
            StatusList.data = new List<StatusModel>();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    string query = "SELECT StatusId, StatusCode, StatusName, StatusFor, SeqNo FROM gl_Status_m ";
                                    

                    using (var command = new SqlCommand(query, connection))
                    {
                        connection.Open();

                        using (var reader = command.ExecuteReader())
                        {
                            while (await reader.ReadAsync())
                            {
                                StatusModel obj = new StatusModel();
                                
                                obj.StatusId = Convert.ToInt64(reader["StatusId"]);
                                obj.StatusCode = Convert.ToString(reader["StatusCode"]);
                                obj.StatusName = Convert.ToString(reader["StatusName"]);
                                obj.StatusFor = Convert.ToString(reader["StatusFor"]);
                                obj.SeqNo = Convert.ToInt64(reader["SeqNo"]);

                                StatusList.data.Add(obj);
                            }

                            StatusList.status = true;
                            StatusList.msg = "Success";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                
                Console.WriteLine(ex.Message.ToString());
                Console.WriteLine(ex.StackTrace);

                StatusList.status = false;
                StatusList.msg = ex.Message.ToString();
            }
            return StatusList;
        }   //End public async Task<StatusListModel> getStatusList()

        [Route("getFilteredStatus")]
        [HttpGet]
        public async Task<StatusListModel> getFilteredStatusList()
        {
            StatusListModel StatusList = new StatusListModel();
            StatusList.data = new List<StatusModel>();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    string query = "SELECT StatusId, StatusCode, StatusName, StatusFor, SeqNo FROM gl_Status_m ";

                    using (var command = new SqlCommand(query, connection))
                    {
                        connection.Open();

                        using (var reader = command.ExecuteReader())
                        {
                            while (await reader.ReadAsync())
                            {
                                StatusModel obj = new StatusModel();
                                
                                obj.StatusId = Convert.ToInt64(reader["StatusId"]);
                                obj.StatusCode = Convert.ToString(reader["StatusCode"]);
                                obj.StatusName = Convert.ToString(reader["StatusName"]);
                                obj.StatusFor = Convert.ToString(reader["StatusFor"]);
                                obj.SeqNo = Convert.ToInt64(reader["SeqNo"]);

                                StatusList.data.Add(obj);
                            }

                            StatusList.status = true;
                            StatusList.msg = "Success";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);

                Console.WriteLine(ex.Message.ToString());
                Console.WriteLine(ex.StackTrace);

                StatusList.status = false;
                StatusList.msg = ex.Message.ToString();
            }
            return StatusList;
        }   //End public async Task<StatusListModel> getFilteredStatusList()

        [Route("getStatusDetail")]
        [HttpGet]
        public async Task<StatusDetailModel> getStatusDetail(Int64 id)
        {
            StatusDetailModel StatusDetail = new StatusDetailModel();
            StatusDetail.data = new StatusModel();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    string query = "SELECT StatusId, StatusCode, StatusName, StatusFor, SeqNo FROM gl_Status_m where StatusId=" + id.ToString();

                    using (var command = new SqlCommand(query, connection))
                    {
                        connection.Open();

                        using (var reader = command.ExecuteReader())
                        {
                            if (await reader.ReadAsync())
                            {
                                
                                StatusDetail.data.StatusId = Convert.ToInt64(reader["StatusId"]);
                                StatusDetail.data.StatusCode = Convert.ToString(reader["StatusCode"]);
                                StatusDetail.data.StatusName = Convert.ToString(reader["StatusName"]);
                                StatusDetail.data.StatusFor = Convert.ToString(reader["StatusFor"]);
                                StatusDetail.data.SeqNo = Convert.ToInt64(reader["SeqNo"]);
                            }

                            StatusDetail.status = true;
                            StatusDetail.msg = "Success";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);

                Console.WriteLine(ex.Message.ToString());
                Console.WriteLine(ex.StackTrace);

                StatusDetail.status = false;
                StatusDetail.msg = ex.Message.ToString();
            }
            return StatusDetail;
        }   //End public async Task<StatusDetailModel> getStatusDetail(int64 id)

        [Route("saveStatus")]
        [HttpPost]
        public async Task<StatusReturnModel> saveStatusData([FromBody] StatusRequestModel data)
        {
            StatusReturnModel rtnData = new StatusReturnModel();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    string query = "SP_SaveStatus";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add("@sStatusCode", SqlDbType.VarChar, 10).Value = data.StatusCode;
                        command.Parameters.Add("@sStatusName", SqlDbType.VarChar, 50).Value = data.StatusName;
                        command.Parameters.Add("@sStatusFor", SqlDbType.VarChar, 50).Value = data.StatusFor;
                        command.Parameters.Add("@iSeqNo", SqlDbType.Int).Value = data.SeqNo;


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
        }   //End public async Task<StatusReturnModel> saveStatusData([FromBody] StatusRequestModel data)

        [Route("updateStatus")]
        [HttpPut]
        public async Task<StatusReturnModel> updateStatusData([FromBody] StatusRequestModel data)
        {
            StatusReturnModel rtnData = new StatusReturnModel();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    string query = "SP_UpdateStatus";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add("@iStatusId", SqlDbType.Int).Value = data.StatusId;
                        command.Parameters.Add("@sStatusCode", SqlDbType.VarChar, 10).Value = data.StatusCode;
                        command.Parameters.Add("@sStatusName", SqlDbType.VarChar, 50).Value = data.StatusName;
                        command.Parameters.Add("@sStatusFor", SqlDbType.VarChar, 50).Value = data.StatusFor;
                        command.Parameters.Add("@iSeqNo", SqlDbType.Int).Value = data.SeqNo;

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
        }   //End public async Task<StatusReturnModel> saveStatusData([FromBody] StatusRequestModel data)

        [Route("deleteStatus")]
        [HttpDelete]
        public async Task<StatusReturnModel> deleteStatusData(Int64 Id)
        {
            StatusReturnModel rtnData = new StatusReturnModel();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    string query = "SP_DeleteStatus";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add("@iStatusId", SqlDbType.Int).Value = Id;
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
        }   //End public async Task<StatusReturnModel> deleteStatusData(Int64 Id)

    }   //End public class StatusController : ControllerBase

}   //End namespace ERP_ASPCoreApi.Controllers