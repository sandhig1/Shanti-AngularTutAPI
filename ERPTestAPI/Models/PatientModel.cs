using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPTestAPI.Models
{
    public class PatientModel
    {
        public long PatientId { get; set; }
        public String PatientCode { get; set; }
        public String PatientName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public String DateOfBirthFormatted { get; set; }
        public String Gender { get; set; }
        public long Age { get; set; }
        public String MobileNo { get; set; }
        public String EmailAdd { get; set; }
        public String Address { get; set; }
        public String BloodGroup { get; set; }
        public bool Insured { get; set; }
        public long? AreaId { get; set; }
        public String AreaCode { get; set; }
        public String AreaName { get; set; }
        public long CityId { get; set; }
        public String CityCode { get; set; }
        public String CityName { get; set; }
        public long StateId { get; set; }
        public String StateCode { get; set; }
        public String StateName { get; set; }
        
    }

    public class PatientRequestModel
    {
        public long PatientId { get; set; }
        public String PatientCode { get; set; }
        public String PatientName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public String DateOfBirthFormatted { get; set; }
        public String Gender { get; set; }
        public long Age { get; set; }
        public String MobileNo { get; set; }
        public String EmailAdd { get; set; }
        public String Address { get; set; }
        public String BloodGroup { get; set; }
        public bool Insured { get; set; }
        public long? AreaId { get; set; }
        public String AreaCode { get; set; }
        public String AreaName { get; set; }
        public long CityId { get; set; }
        public String CityCode { get; set; }
        public String CityName { get; set; }
        public long StateId { get; set; }
        public String StateCode { get; set; }
        public String StateName { get; set; }


    }

    public class PatientListModel
    {
        public List<PatientModel> data { get; set; }

        public bool status { get; set; }
        public string msg { get; set; }
    } 

    public class PatientDetailModel
    {
        public PatientModel data { get; set; }

        public bool status { get; set; }
        public string msg { get; set; }
    }

    public class PatientReturnModel
    {
        public bool status { get; set; }
        public string msg { get; set; }
    }
}
