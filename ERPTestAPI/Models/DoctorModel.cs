using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPTestAPI.Models
{
    public class DoctorModel
    {
        public long DoctorId { get; set; }
        public String DoctorCode { get; set; }
        public String DoctorName { get; set; }
        public String Gender { get; set; }
        public long Age { get; set; }
        public String Qualification { get; set; }
        public String Address { get; set; }
        public String MobileNo { get; set; }
        public String EmailAdd { get; set; }
        public long CityId { get; set; }
        public String CityCode { get; set; }
        public String CityName { get; set; }
        public long StateId { get; set; }
        public String StateCode { get; set; }
        public String StateName { get; set; }
        
    }

    public class DoctorRequestModel
    {
        public long DoctorId { get; set; }
        public String DoctorCode { get; set; }
        public String DoctorName { get; set; }
        public String Gender { get; set; }
        public long Age { get; set; }
        public String Qualification { get; set; }
        public String Address { get; set; }
        public String MobileNo { get; set; }
        public String EmailAdd { get; set; }
        public long CityId { get; set; }
        public long StateId { get; set; }
        
    }

    public class DoctorListModel
    {
        public List<DoctorModel> data { get; set; }

        public bool status { get; set; }
        public string msg { get; set; }
    } 

    public class DoctorDetailModel
    {
        public DoctorModel data { get; set; }

        public bool status { get; set; }
        public string msg { get; set; }
    }

    public class DoctorReturnModel
    {
        public bool status { get; set; }
        public string msg { get; set; }
    }
}
