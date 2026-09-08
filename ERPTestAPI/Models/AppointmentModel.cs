using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPTestAPI.Models
{
    public class AppointmentModel
    {
        public long AppointmentId { get; set; }
        public String AppointmentNo { get; set; }
        public DateTime AppointmentDate { get; set; }
        public String AppointmentDateFormatted { get; set; }
        public TimeSpan AppointmentTime { get; set; }
        public String AppointmentTimeFormatted { get; set; }
        public long ClinicId { get; set; }
        public String ClinicCode { get; set; }
        public String ClinicName { get; set; }
        public long PatientId { get; set; }
        public String PatientCode { get; set; }
        public String PatientName { get; set; }
        public long DoctorId { get; set; }
        public String DoctorCode { get; set; }
        public String DoctorName { get; set; }
        public String Reason { get; set; }
        public long StatusId { get; set; }
        public String StatusCode { get; set; }
        public String StatusName { get; set; }

    }

    public class AppointmentRequestModel
    {
        public long AppointmentId { get; set; }
        public String AppointmentNo { get; set; }
        public DateTime AppointmentDate { get; set; }
        public String AppointmentDateFormatted { get; set; }
        public DateTime AppointmentTime { get; set; }
        public String AppointmentTimeFormatted { get; set; }
        public long ClinicId { get; set; }
        public String ClinicCode { get; set; }
        public String ClinicName { get; set; }
        public long PatientId { get; set; }
        public String PatientCode { get; set; }
        public String PatientName { get; set; }
        public long DoctorId { get; set; }
        public String DoctorCode { get; set; }
        public String DoctorName { get; set; }
        public String Reason { get; set; }
        public long StatusId { get; set; }
        public String StatusCode { get; set; }
        public String StatusName { get; set; }

    }

    public class AppointmentListModel
    {
        public List<AppointmentModel> data { get; set; }

        public bool status { get; set; }
        public string msg { get; set; }
    } 

    public class AppointmentDetailModel
    {
        public AppointmentModel data { get; set; }

        public bool status { get; set; }
        public string msg { get; set; }
    }

    public class AppointmentReturnModel
    {
        public bool status { get; set; }
        public string msg { get; set; }
    }
}
