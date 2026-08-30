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
    [RoutePrefix("api/State")]
    public class StateController : ApiController
    {
        private string _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        [Route("getStates")]
        [HttpGet]
        public async Task<StateListModel> getStateList()
        {
            StateListModel StateList = new StateListModel();
            StateList.data = new List<StateModel>();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    string query = "SELECT StateId, StateCode, StateName FROM gl_State_m"; //format(GETDATE(), 'dd-MMM-yyyy') + ' ' + convert(varchar(5), GETDATE(), 108) as StateDateFormatted, 

                    using (var command = new SqlCommand(query, connection))
                    {
                        connection.Open();

                        using (var reader = command.ExecuteReader())
                        {
                            while (await reader.ReadAsync())
                            {
                                StateModel obj = new StateModel();

                                obj.StateId = Convert.ToInt64(reader["StateId"]);
                                obj.StateCode = Convert.ToString(reader["StateCode"]);
                                obj.StateName = Convert.ToString(reader["StateName"]);

                                StateList.data.Add(obj);
                            }

                            StateList.status = true;
                            StateList.msg = "Success";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                
                Console.WriteLine(ex.Message.ToString());
                Console.WriteLine(ex.StackTrace);

                StateList.status = false;
                StateList.msg = ex.Message.ToString();
            }
            return StateList;
        }   //End public async Task<StateListModel> getStateList()

        [Route("getFilteredStates")]
        [HttpGet]
        public async Task<StateListModel> getFilteredStateList()
        {
            StateListModel StateList = new StateListModel();
            StateList.data = new List<StateModel>();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    string query = "SELECT StateId, StateCode, StateName FROM gl_State_m"; //format(GETDATE(), 'dd-MMM-yyyy') + ' ' + convert(varchar(5), GETDATE(), 108) as StateDateFormatted, 

                    using (var command = new SqlCommand(query, connection))
                    {
                        connection.Open();

                        using (var reader = command.ExecuteReader())
                        {
                            while (await reader.ReadAsync())
                            {
                                StateModel obj = new StateModel();

                                obj.StateId = Convert.ToInt64(reader["StateId"]);
                                obj.StateCode = Convert.ToString(reader["StateCode"]);
                                obj.StateName = Convert.ToString(reader["StateName"]);
                                
                                StateList.data.Add(obj);
                            }

                            StateList.status = true;
                            StateList.msg = "Success";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);

                Console.WriteLine(ex.Message.ToString());
                Console.WriteLine(ex.StackTrace);

                StateList.status = false;
                StateList.msg = ex.Message.ToString();
            }
            return StateList;
        }   //End public async Task<StateListModel> getFilteredStateList()

        [Route("getStateDetail")]
        [HttpGet]
        public async Task<StateDetailModel> getStateDetail(Int64 id)
        {
            StateDetailModel StateDetail = new StateDetailModel();
            StateDetail.data = new StateModel();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    string query = "SELECT StateId, StateCode, StateName FROM gl_State_m where StateId=" + id.ToString(); //format(GETDATE(), 'dd-MMM-yyyy') + ' ' + convert(varchar(5), GETDATE(), 108) as StateDateFormatted, 

                    using (var command = new SqlCommand(query, connection))
                    {
                        connection.Open();

                        using (var reader = command.ExecuteReader())
                        {
                            if (await reader.ReadAsync())
                            {
                                StateDetail.data.StateId = Convert.ToInt64(reader["StateId"]);
                                StateDetail.data.StateCode = Convert.ToString(reader["StateCode"]);
                                StateDetail.data.StateName = Convert.ToString(reader["StateName"]);
                            }

                            StateDetail.status = true;
                            StateDetail.msg = "Success";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);

                Console.WriteLine(ex.Message.ToString());
                Console.WriteLine(ex.StackTrace);

                StateDetail.status = false;
                StateDetail.msg = ex.Message.ToString();
            }
            return StateDetail;
        }   //End public async Task<StateDetailModel> getStateDetail(int64 id)

        [Route("saveState")]
        [HttpPost]
        public async Task<StateReturnModel> saveStateData([FromBody] StateModel data)
        {
            StateReturnModel rtnData = new StateReturnModel();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    string query = "SP_InsertState";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add("@sStateCode", SqlDbType.VarChar, 10).Value = data.StateCode;
                        command.Parameters.Add("@sStateName", SqlDbType.VarChar, 100).Value = data.StateName;

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
        }   //End public async Task<StateReturnModel> saveStateData([FromBody] StateModel data)

        [Route("updateState")]
        [HttpPut]
        public async Task<StateReturnModel> updateStateData([FromBody] StateModel data)
        {
            StateReturnModel rtnData = new StateReturnModel();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    string query = "SP_UpdateState";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add("@iStateId", SqlDbType.Int).Value = data.StateId;
                        command.Parameters.Add("@sStateCode", SqlDbType.VarChar, 10).Value = data.StateCode;
                        command.Parameters.Add("@sStateName", SqlDbType.VarChar, 100).Value = data.StateName;

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
        }   //End public async Task<StateReturnModel> saveStateData([FromBody] StateModel data)

        [Route("deleteState")]
        [HttpDelete]
        public async Task<StateReturnModel> deleteStateData(Int64 Id)
        {
            StateReturnModel rtnData = new StateReturnModel();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    string query = "SP_DeleteState";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add("@iStateId", SqlDbType.Int).Value = Id;
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
        }   //End public async Task<StateReturnModel> deleteStateData(Int64 Id)

    }   //End public class StateController : ControllerBase

}   //End namespace ERP_ASPCoreApi.Controllers