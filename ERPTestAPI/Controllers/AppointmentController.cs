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
    [RoutePrefix("api/Appointment")]
    public class AppointmentController : ApiController
    {
        private string _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        [Route("getAppointments")]
        [HttpGet]
        public async Task<AppointmentListModel> getAppointmentList()
        {
            AppointmentListModel AppointmentList = new AppointmentListModel();
            AppointmentList.data = new List<AppointmentModel>();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    string query = "SELECT a.AppointmentId, a.AppointmentNo, a.AppointmentDate, dbo.GetDateFormat1(a.AppointmentDate) as AppointmentDateFormatted, " +
                                    " a.AppointmentTime, convert(varchar(5), a.AppointmentTime) as AppointmentTimeFormatted, " +
                                    " a.ClinicId, b.ClinicCode,  b.ClinicName,  " +
                                    " a.PatientId, c.PatientCode,  c.PatientName,  " +
                                    " a.DoctorId, d.DoctorCode,  d.DoctorName,  " +
                                    " a.StatusId, e.StatusCode, e.StatusName, a.Reason " +
                                    " FROM gl_Appointment_m a inner join gl_Clinic_m b on a.ClinicId = b.ClinicId " +
                                    " inner join gl_Patient_m c on a.PatientId = c.PatientId " +
                                    " inner join gl_Doctor_m d on a.DoctorId = d.DoctorId " +
                                    " inner join gl_Status_m e on a.StatusId = e.StatusId ";

                    using (var command = new SqlCommand(query, connection))
                    {
                        connection.Open();

                        using (var reader = command.ExecuteReader())
                        {
                            while (await reader.ReadAsync())
                            {
                                AppointmentModel obj = new AppointmentModel();

                                obj.AppointmentId = Convert.ToInt64(reader["AppointmentId"]);
                                obj.AppointmentNo = Convert.ToString(reader["AppointmentNo"]);
                                obj.AppointmentDate = Convert.ToDateTime(reader["AppointmentDate"]);
                                obj.AppointmentDateFormatted = Convert.ToString(reader["AppointmentDateFormatted"]);
                                obj.AppointmentTime = reader.IsDBNull(reader.GetOrdinal("AppointmentTime"))? TimeSpan.Zero:reader.GetTimeSpan(reader.GetOrdinal("AppointmentTime"));
                                obj.AppointmentTimeFormatted = Convert.ToString(reader["AppointmentTimeFormatted"]);
                                obj.ClinicId = Convert.ToInt64(reader["ClinicId"]);
                                obj.ClinicCode = Convert.ToString(reader["ClinicCode"]);
                                obj.ClinicName = Convert.ToString(reader["ClinicName"]);
                                obj.PatientId = Convert.ToInt64(reader["PatientId"]);
                                obj.PatientCode = Convert.ToString(reader["PatientCode"]);
                                obj.PatientName = Convert.ToString(reader["PatientName"]);
                                obj.DoctorId = Convert.ToInt64(reader["DoctorId"]);
                                obj.DoctorCode = Convert.ToString(reader["DoctorCode"]);
                                obj.DoctorName = Convert.ToString(reader["DoctorName"]);
                                obj.Reason = Convert.ToString(reader["Reason"]);
                                obj.StatusId = Convert.ToInt64(reader["StatusId"]);
                                obj.StatusCode = Convert.ToString(reader["StatusCode"]);
                                obj.StatusName = Convert.ToString(reader["StatusName"]);

                                AppointmentList.data.Add(obj);
                            }

                            AppointmentList.status = true;
                            AppointmentList.msg = "Success";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                
                Console.WriteLine(ex.Message.ToString());
                Console.WriteLine(ex.StackTrace);

                AppointmentList.status = false;
                AppointmentList.msg = ex.Message.ToString();
            }
            return AppointmentList;
        }   //End public async Task<AppointmentListModel> getAppointmentList()

        [Route("getFilteredAppointments")]
        [HttpGet]
        public async Task<AppointmentListModel> getFilteredAppointmentList()
        {
            AppointmentListModel AppointmentList = new AppointmentListModel();
            AppointmentList.data = new List<AppointmentModel>();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    string query = "SELECT a.AppointmentId, a.AppointmentNo, a.AppointmentDate, dbo.GetDateFormat1(a.AppointmentDate) as AppointmentDateFormatted," +
                        " a.AppointmentTime, convert(varchar(5), a.AppointmentTime) as AppointmentTimeFormatted,  " +
                        "a.ClinicId, b.ClinicCode, b.ClinicName, " +
                        "a.PatientId, c.PatientCode, c.PatientName, " +
                        "a.DoctorId, d.DoctorCode, d.DoctorName, a.Reason, " +
                        "a.StatusId, e.StatusCode, e.StatusName " +
                        " FROM gl_Appointment_m a, gl_Clinic_m b, gl_Patient_m c, gl_Doctor_m d, gl_Status_m e where a.ClinicId = b.ClinicId and a.PatientId = c.PatientId and a.DoctorId = d.DoctorId and a.StatusId = e.StatusId ";
                    using (var command = new SqlCommand(query, connection))
                    {
                        connection.Open();

                        using (var reader = command.ExecuteReader())
                        {
                            while (await reader.ReadAsync())
                            {
                                AppointmentModel obj = new AppointmentModel();

                                obj.AppointmentId = Convert.ToInt64(reader["AppointmentId"]);
                                obj.AppointmentNo = Convert.ToString(reader["AppointmentNo"]);
                                obj.AppointmentDate = Convert.ToDateTime(reader["AppointmentDate"]);
                                obj.AppointmentDateFormatted = Convert.ToString(reader["AppointmentDateFormatted"]);
                                obj.AppointmentTime = reader.IsDBNull(reader.GetOrdinal("AppointmentTime")) ? TimeSpan.Zero : reader.GetTimeSpan(reader.GetOrdinal("AppointmentTime"));
                                obj.AppointmentTimeFormatted = Convert.ToString(reader["AppointmentTimeFormatted"]);
                                obj.ClinicId = Convert.ToInt64(reader["ClinicId"]);
                                obj.ClinicCode = Convert.ToString(reader["ClinicCode"]);
                                obj.ClinicName = Convert.ToString(reader["ClinicName"]);
                                obj.PatientId = Convert.ToInt64(reader["PatientId"]);
                                obj.PatientCode = Convert.ToString(reader["PatientCode"]);
                                obj.PatientName = Convert.ToString(reader["PatientName"]);
                                obj.DoctorId = Convert.ToInt64(reader["DoctorId"]);
                                obj.DoctorCode = Convert.ToString(reader["DoctorCode"]);
                                obj.DoctorName = Convert.ToString(reader["DoctorName"]);
                                obj.Reason = Convert.ToString(reader["Reason"]);
                                obj.StatusId = Convert.ToInt64(reader["StatusId"]);
                                obj.StatusCode = Convert.ToString(reader["StatusCode"]);
                                obj.StatusName = Convert.ToString(reader["StatusName"]);

                                AppointmentList.data.Add(obj);
                            }

                            AppointmentList.status = true;
                            AppointmentList.msg = "Success";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);

                Console.WriteLine(ex.Message.ToString());
                Console.WriteLine(ex.StackTrace);

                AppointmentList.status = false;
                AppointmentList.msg = ex.Message.ToString();
            }
            return AppointmentList;
        }   //End public async Task<AppointmentListModel> getFilteredAppointmentList()

        [Route("getAppointmentDetail")]
        [HttpGet]
        public async Task<AppointmentDetailModel> getAppointmentDetail(Int64 id)
        {
            AppointmentDetailModel AppointmentDetail = new AppointmentDetailModel();
            AppointmentDetail.data = new AppointmentModel();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    string query = "SELECT a.AppointmentId, a.AppointmentNo, a.AppointmentDate, dbo.GetDateFormat1(a.AppointmentDate) as AppointmentDateFormatted, " +
                                    " a.AppointmentTime, convert(varchar(5), a.AppointmentTime) as AppointmentTimeFormatted, " +
                                    " a.ClinicId, b.ClinicCode,  b.ClinicName,  " +
                                    " a.PatientId, c.PatientCode,  c.PatientName,  " +
                                    " a.DoctorId, d.DoctorCode,  d.DoctorName,  " +
                                    " a.StatusId, e.StatusCode, e.StatusName, a.Reason " +
                                    " FROM gl_Appointment_m a inner join gl_Clinic_m b on a.ClinicId = b.ClinicId " +
                                    " inner join gl_Patient_m c on a.PatientId = c.PatientId " +
                                    " inner join gl_Doctor_m d on a.DoctorId = d.DoctorId " +
                                    " inner join gl_Status_m e on a.StatusId = e.StatusId " +
                                    " Where a.AppointmentId = " +id.ToString();

                    using (var command = new SqlCommand(query, connection))
                    {
                        connection.Open();

                        using (var reader = command.ExecuteReader())
                        {
                            if (await reader.ReadAsync())
                            {
                                AppointmentDetail.data.AppointmentId = Convert.ToInt64(reader["AppointmentId"]);
                                AppointmentDetail.data.AppointmentNo = Convert.ToString(reader["AppointmentNo"]);
                                AppointmentDetail.data.AppointmentDate = Convert.ToDateTime(reader["AppointmentDate"]);
                                AppointmentDetail.data.AppointmentDateFormatted = Convert.ToString(reader["AppointmentDateFormatted"]);
                                AppointmentDetail.data.AppointmentTime = reader.IsDBNull(reader.GetOrdinal("AppointmentTime")) ? TimeSpan.Zero : reader.GetTimeSpan(reader.GetOrdinal("AppointmentTime"));
                                AppointmentDetail.data.AppointmentTimeFormatted = Convert.ToString(reader["AppointmentTimeFormatted"]);
                                AppointmentDetail.data.ClinicId = Convert.ToInt64(reader["ClinicId"]);
                                AppointmentDetail.data.ClinicCode = Convert.ToString(reader["ClinicCode"]);
                                AppointmentDetail.data.ClinicName = Convert.ToString(reader["ClinicName"]);
                                AppointmentDetail.data.PatientId = Convert.ToInt64(reader["PatientId"]);
                                AppointmentDetail.data.PatientCode = Convert.ToString(reader["PatientCode"]);
                                AppointmentDetail.data.PatientName = Convert.ToString(reader["PatientName"]);
                                AppointmentDetail.data.DoctorId = Convert.ToInt64(reader["DoctorId"]);
                                AppointmentDetail.data.DoctorCode = Convert.ToString(reader["DoctorCode"]);
                                AppointmentDetail.data.DoctorName = Convert.ToString(reader["DoctorName"]);
                                AppointmentDetail.data.Reason = Convert.ToString(reader["Reason"]);
                                AppointmentDetail.data.StatusId = Convert.ToInt64(reader["StatusId"]);
                                AppointmentDetail.data.StatusCode = Convert.ToString(reader["StatusCode"]);
                                AppointmentDetail.data.StatusName = Convert.ToString(reader["StatusName"]);
                            }

                            AppointmentDetail.status = true;
                            AppointmentDetail.msg = "Success";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);

                Console.WriteLine(ex.Message.ToString());
                Console.WriteLine(ex.StackTrace);

                AppointmentDetail.status = false;
                AppointmentDetail.msg = ex.Message.ToString();
            }
            return AppointmentDetail;
        }   //End public async Task<AppointmentDetailModel> getAppointmentDetail(int64 id)

        [Route("saveAppointment")]
        [HttpPost]
        public async Task<AppointmentReturnModel> saveAppointmentData([FromBody] AppointmentRequestModel data)
        {
            AppointmentReturnModel rtnData = new AppointmentReturnModel();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    string query = "SP_SaveAppointment";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add("@sAppointmentNo", SqlDbType.VarChar, 10).Value = data.AppointmentNo;
                        command.Parameters.Add("@dAppointmentDate", SqlDbType.Date).Value = data.AppointmentDate;
                        command.Parameters.Add("@tAppointmentTime", SqlDbType.Time).Value = data.AppointmentTime;
                        command.Parameters.Add("@iClinicId", SqlDbType.Int).Value = data.ClinicId;
                        command.Parameters.Add("@iPatientId", SqlDbType.Int).Value = data.PatientId;
                        command.Parameters.Add("@iDoctorId", SqlDbType.Int).Value = data.PatientId;
                        command.Parameters.Add("@sReason", SqlDbType.VarChar, 1000).Value = data.Reason;
                        command.Parameters.Add("@iStatusId", SqlDbType.Int).Value = data.StatusId;
                        

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
        }   //End public async Task<AppointmentReturnModel> saveAppointmentData([FromBody] AppointmentRequestModel data)

        [Route("updateAppointment")]
        [HttpPut]
        public async Task<AppointmentReturnModel> updateAppointmentData([FromBody] AppointmentRequestModel data)
        {
            AppointmentReturnModel rtnData = new AppointmentReturnModel();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    string query = "SP_UpdateAppointment";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add("@iAppointmentId", SqlDbType.Int).Value = data.AppointmentId;
                        command.Parameters.Add("@sAppointmentNo", SqlDbType.VarChar, 10).Value = data.AppointmentNo;
                        command.Parameters.Add("@dAppointmentDate", SqlDbType.Date).Value = data.AppointmentDate;
                        command.Parameters.Add("@tAppointmentTime", SqlDbType.Time).Value = data.AppointmentTime;
                        command.Parameters.Add("@iClinicId", SqlDbType.Int).Value = data.ClinicId;
                        command.Parameters.Add("@iPatientId", SqlDbType.Int).Value = data.PatientId;
                        command.Parameters.Add("@iDoctorId", SqlDbType.Int).Value = data.PatientId;
                        command.Parameters.Add("@sReason", SqlDbType.VarChar, 1000).Value = data.Reason;
                        command.Parameters.Add("@iStatusId", SqlDbType.Int).Value = data.StatusId;

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
        }   //End public async Task<AppointmentReturnModel> saveAppointmentData([FromBody] AppointmentRequestModel data)

        [Route("deleteAppointment")]
        [HttpDelete]
        public async Task<AppointmentReturnModel> deleteAppointmentData(Int64 Id)
        {
            AppointmentReturnModel rtnData = new AppointmentReturnModel();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    string query = "SP_DeleteAppointment";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add("@iAppointmentId", SqlDbType.Int).Value = Id;
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
        }   //End public async Task<AppointmentReturnModel> deleteAppointmentData(Int64 Id)

    }   //End public class AppointmentController : ControllerBase

}   //End namespace ERP_ASPCoreApi.Controllers