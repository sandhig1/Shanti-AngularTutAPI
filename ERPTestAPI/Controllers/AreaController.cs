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
    [RoutePrefix("api/Area")]
    public class AreaController : ApiController
    {
        private string _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        [Route("getAreas")]
        [HttpGet]
        public async Task<AreaListModel> getAreaList()
        {
            AreaListModel AreaList = new AreaListModel();
            AreaList.data = new List<AreaModel>();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    string query = "SELECT a.AreaId, a.AreaCode, a.AreaName, a.StateId, b.StateCode, b.StateName, a.CityId, c.CityCode, c.CityName " +
                                " FROM gl_Area_m a, gl_state_m b, gl_City_m c where a.StateId = b.StateId and a.CityId = c.CityId";

                    using (var command = new SqlCommand(query, connection))
                    {
                        connection.Open();

                        using (var reader = command.ExecuteReader())
                        {
                            while (await reader.ReadAsync())
                            {
                                AreaModel obj = new AreaModel();

                                obj.AreaId = Convert.ToInt64(reader["AreaId"]);
                                obj.AreaCode = Convert.ToString(reader["AreaCode"]);
                                obj.AreaName = Convert.ToString(reader["AreaName"]);
                                obj.CityId = Convert.ToInt64(reader["CityId"]);
                                obj.CityCode = Convert.ToString(reader["CityCode"]);
                                obj.CityName = Convert.ToString(reader["CityName"]);
                                obj.StateId = Convert.ToInt64(reader["StateId"]);
                                obj.StateCode = Convert.ToString(reader["StateCode"]);
                                obj.StateName = Convert.ToString(reader["StateName"]);

                                AreaList.data.Add(obj);
                            }

                            AreaList.status = true;
                            AreaList.msg = "Success";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                
                Console.WriteLine(ex.Message.ToString());
                Console.WriteLine(ex.StackTrace);

                AreaList.status = false;
                AreaList.msg = ex.Message.ToString();
            }
            return AreaList;
        }   //End public async Task<AreaListModel> getAreaList()

        [Route("getFilteredAreas")]
        [HttpGet]
        public async Task<AreaListModel> getFilteredAreaList()
        {
            AreaListModel AreaList = new AreaListModel();
            AreaList.data = new List<AreaModel>();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    string query = "SELECT a.AreaId, a.AreaCode, a.AreaName, a.StateId, b.StateCode, b.StateName, a.CityId, c.CityCode, c.CityName" +
                            "  FROM gl_Area_m a, gl_state_m b, gl_City_m c where a.StateId = b.StateId and a.CityId = c.CityId";

                    using (var command = new SqlCommand(query, connection))
                    {
                        connection.Open();

                        using (var reader = command.ExecuteReader())
                        {
                            while (await reader.ReadAsync())
                            {
                                AreaModel obj = new AreaModel();

                                obj.AreaId = Convert.ToInt64(reader["AreaId"]);
                                obj.AreaId = Convert.ToInt64(reader["AreaId"]);
                                obj.AreaCode = Convert.ToString(reader["AreaCode"]);
                                obj.AreaName = Convert.ToString(reader["AreaName"]);
                                obj.CityId = Convert.ToInt64(reader["CityId"]);
                                obj.CityCode = Convert.ToString(reader["CityCode"]);
                                obj.CityName = Convert.ToString(reader["CityName"]);
                                obj.StateId = Convert.ToInt64(reader["StateId"]);
                                obj.StateCode = Convert.ToString(reader["StateCode"]);
                                obj.StateName = Convert.ToString(reader["StateName"]);

                                AreaList.data.Add(obj);
                            }

                            AreaList.status = true;
                            AreaList.msg = "Success";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);

                Console.WriteLine(ex.Message.ToString());
                Console.WriteLine(ex.StackTrace);

                AreaList.status = false;
                AreaList.msg = ex.Message.ToString();
            }
            return AreaList;
        }   //End public async Task<AreaListModel> getFilteredAreaList()

        [Route("getAreaDetail")]
        [HttpGet]
        public async Task<AreaDetailModel> getAreaDetail(Int64 id)
        {
            AreaDetailModel AreaDetail = new AreaDetailModel();
            AreaDetail.data = new AreaModel();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    string query = "SELECT a.AreaId, a.AreaCode, a.AreaName, a.StateId, b.StateCode, b.StateName, a.CityId, c.CityCode, c.CityName " +
                                "  FROM gl_Area_m a, gl_state_m b, gl_City_m c where a.StateId = b.StateId and a.CityId = c.CityId and a.AreaId=" + id.ToString();

                    using (var command = new SqlCommand(query, connection))
                    {
                        connection.Open();

                        using (var reader = command.ExecuteReader())
                        {
                            if (await reader.ReadAsync())
                            {
                                AreaDetail.data.AreaId = Convert.ToInt64(reader["AreaId"]);
                                AreaDetail.data.AreaCode = Convert.ToString(reader["AreaCode"]);
                                AreaDetail.data.AreaName = Convert.ToString(reader["AreaName"]);
                                AreaDetail.data.CityId = Convert.ToInt64(reader["CityId"]);
                                AreaDetail.data.CityCode = Convert.ToString(reader["CityCode"]);
                                AreaDetail.data.CityName = Convert.ToString(reader["CityName"]);
                                AreaDetail.data.StateId = Convert.ToInt64(reader["StateId"]);
                                AreaDetail.data.StateCode = Convert.ToString(reader["StateCode"]);
                                AreaDetail.data.StateName = Convert.ToString(reader["StateName"]);                                
                            }

                            AreaDetail.status = true;
                            AreaDetail.msg = "Success";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);

                Console.WriteLine(ex.Message.ToString());
                Console.WriteLine(ex.StackTrace);

                AreaDetail.status = false;
                AreaDetail.msg = ex.Message.ToString();
            }
            return AreaDetail;
        }   //End public async Task<AreaDetailModel> getAreaDetail(int64 id)

        [Route("saveArea")]
        [HttpPost]
        public async Task<AreaReturnModel> saveAreaData([FromBody] AreaModel data)
        {
            AreaReturnModel rtnData = new AreaReturnModel();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    string query = "SP_SaveArea";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add("@sAreaCode", SqlDbType.VarChar, 10).Value = data.AreaCode;
                        command.Parameters.Add("@sAreaName", SqlDbType.VarChar, 100).Value = data.AreaName;
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
            }

            return rtnData;
        }   //End public async Task<AreaReturnModel> saveAreaData([FromBody] AreaRequestModel data)

        [Route("updateArea")]
        [HttpPut]
        public async Task<AreaReturnModel> updateAreaData([FromBody] AreaRequestModel data)
        {
            AreaReturnModel rtnData = new AreaReturnModel();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    string query = "SP_UpdateArea";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add("@iAreaId", SqlDbType.Int).Value = data.AreaId;
                        command.Parameters.Add("@sAreaCode", SqlDbType.VarChar, 10).Value = data.AreaCode;
                        command.Parameters.Add("@sAreaName", SqlDbType.VarChar, 100).Value = data.AreaName;
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
            }

            return rtnData;
        }   //End public async Task<AreaReturnModel> saveAreaData([FromBody] AreaRequestModel data)

        [Route("deleteArea")]
        [HttpDelete]
        public async Task<AreaReturnModel> deleteAreaData(Int64 Id)
        {
            AreaReturnModel rtnData = new AreaReturnModel();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    string query = "SP_DeleteArea";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add("@iAreaId", SqlDbType.Int).Value = Id;
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
        }   //End public async Task<AreaReturnModel> deleteAreaData(Int64 Id)

    }   //End public class AreaController : ControllerBase

}   //End namespace ERP_ASPCoreApi.Controllers