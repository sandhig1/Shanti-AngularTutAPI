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
    [RoutePrefix("api/City")]
    public class CityController : ApiController
    {
        private string _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        [Route("getCities")]
        [HttpGet]
        public async Task<CityListModel> getCityList()
        {
            CityListModel CityList = new CityListModel();
            CityList.data = new List<CityModel>();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    string query = "SELECT a.CityId, a.CityCode, a.CityName, a.StateId, b.StateCode, b.StateName " +
                            "  FROM gl_City_m a, gl_state_m b where a.StateId = b.StateId";

                    using (var command = new SqlCommand(query, connection))
                    {
                        connection.Open();

                        using (var reader = command.ExecuteReader())
                        {
                            while (await reader.ReadAsync())
                            {
                                CityModel obj = new CityModel();

                                obj.CityId = Convert.ToInt64(reader["CityId"]);
                                obj.CityCode = Convert.ToString(reader["CityCode"]);
                                obj.CityName = Convert.ToString(reader["CityName"]);
                                obj.StateId = Convert.ToInt64(reader["StateId"]);
                                obj.StateCode = Convert.ToString(reader["StateCode"]);
                                obj.StateName = Convert.ToString(reader["StateName"]);

                                CityList.data.Add(obj);
                            }

                            CityList.status = true;
                            CityList.msg = "Success";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                
                Console.WriteLine(ex.Message.ToString());
                Console.WriteLine(ex.StackTrace);

                CityList.status = false;
                CityList.msg = ex.Message.ToString();
            }
            return CityList;
        }   //End public async Task<CityListModel> getCityList()

        [Route("getFilteredEnquiries")]
        [HttpGet]
        public async Task<CityListModel> getFilteredCityList()
        {
            CityListModel CityList = new CityListModel();
            CityList.data = new List<CityModel>();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    string query = "SELECT a.CityId, a.CityCode, a.CityName, a.StateId, b.StateCode, b.StateName " +
                            "  FROM gl_City_m a, gl_state_m b where a.StateId = b.StateId";

                    using (var command = new SqlCommand(query, connection))
                    {
                        connection.Open();

                        using (var reader = command.ExecuteReader())
                        {
                            while (await reader.ReadAsync())
                            {
                                CityModel obj = new CityModel();

                                obj.CityId = Convert.ToInt64(reader["CityId"]);
                                obj.CityId = Convert.ToInt64(reader["CityId"]);
                                obj.CityCode = Convert.ToString(reader["CityCode"]);
                                obj.CityName = Convert.ToString(reader["CityName"]);
                                obj.StateId = Convert.ToInt64(reader["StateId"]);
                                obj.StateCode = Convert.ToString(reader["StateCode"]);
                                obj.StateName = Convert.ToString(reader["StateName"]);

                                CityList.data.Add(obj);
                            }

                            CityList.status = true;
                            CityList.msg = "Success";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);

                Console.WriteLine(ex.Message.ToString());
                Console.WriteLine(ex.StackTrace);

                CityList.status = false;
                CityList.msg = ex.Message.ToString();
            }
            return CityList;
        }   //End public async Task<CityListModel> getFilteredCityList()

        [Route("getCityDetail")]
        [HttpGet]
        public async Task<CityDetailModel> getCityDetail(Int64 id)
        {
            CityDetailModel CityDetail = new CityDetailModel();
            CityDetail.data = new CityModel();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    string query = "SELECT a.CityId, a.CityCode, a.CityName, a.StateId, b.StateCode, b.StateName " +
                                "  FROM gl_City_m a, gl_state_m b where a.StateId = b.StateId and a.CityId=" + id.ToString();

                    using (var command = new SqlCommand(query, connection))
                    {
                        connection.Open();

                        using (var reader = command.ExecuteReader())
                        {
                            if (await reader.ReadAsync())
                            {
                                CityDetail.data.CityId = Convert.ToInt64(reader["CityId"]);
                                CityDetail.data.CityCode = Convert.ToString(reader["CityCode"]);
                                CityDetail.data.CityName = Convert.ToString(reader["CityName"]);
                                CityDetail.data.StateId = Convert.ToInt64(reader["StateId"]);
                                CityDetail.data.StateCode = Convert.ToString(reader["StateCode"]);
                                CityDetail.data.StateName = Convert.ToString(reader["StateName"]);                                
                            }

                            CityDetail.status = true;
                            CityDetail.msg = "Success";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);

                Console.WriteLine(ex.Message.ToString());
                Console.WriteLine(ex.StackTrace);

                CityDetail.status = false;
                CityDetail.msg = ex.Message.ToString();
            }
            return CityDetail;
        }   //End public async Task<CityDetailModel> getCityDetail(int64 id)

        [Route("saveCity")]
        [HttpPost]
        public async Task<CityReturnModel> saveCityData([FromBody] CityModel data)
        {
            CityReturnModel rtnData = new CityReturnModel();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    string query = "SP_InsertCity";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add("@sCityCode", SqlDbType.VarChar, 10).Value = data.CityCode;
                        command.Parameters.Add("@sCityName", SqlDbType.VarChar, 100).Value = data.CityName;
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
            }

            return rtnData;
        }   //End public async Task<CityReturnModel> saveCityData([FromBody] CityModel data)

        [Route("updateCity")]
        [HttpPut]
        public async Task<CityReturnModel> updateCityData([FromBody] CityModel data)
        {
            CityReturnModel rtnData = new CityReturnModel();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    string query = "SP_UpdateCity";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add("@iCityId", SqlDbType.Int).Value = data.CityId;
                        command.Parameters.Add("@sCityCode", SqlDbType.VarChar, 10).Value = data.CityCode;
                        command.Parameters.Add("@sCityName", SqlDbType.VarChar, 100).Value = data.CityName;
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
            }

            return rtnData;
        }   //End public async Task<CityReturnModel> saveCityData([FromBody] CityModel data)

        [Route("deleteCity")]
        [HttpDelete]
        public async Task<CityReturnModel> deleteCityData(Int64 Id)
        {
            CityReturnModel rtnData = new CityReturnModel();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    string query = "SP_DeleteCity";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add("@iCityId", SqlDbType.Int).Value = Id;
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
        }   //End public async Task<CityReturnModel> deleteCityData(Int64 Id)

    }   //End public class CityController : ControllerBase

}   //End namespace ERP_ASPCoreApi.Controllers