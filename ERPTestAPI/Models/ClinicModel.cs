using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPTestAPI.Models
{
    public class ClinicModel
    {
        public long ClinicId { get; set; }
        public String ClinicCode { get; set; }
        public String ClinicName { get; set; }
        public String ClinicAddress { get; set; }
        public long StateId { get; set; }
        public String StateCode { get; set; }
        public String StateName { get; set; }

        public long CityId { get; set; }
        public String CityCode { get; set; }
        public String CityName { get; set; }

        public String MobileNo { get; set; }
        public String PhoneNo { get; set; }
        public String EmailAdd { get; set; }
    }

    public class ClinicListModel
    {
        public List<ClinicModel> data { get; set; }

        public bool status { get; set; }
        public string msg { get; set; }
    } 

    public class ClinicDetailModel
    {
        public ClinicModel data { get; set; }

        public bool status { get; set; }
        public string msg { get; set; }
    }

    public class ClinicReturnModel
    {
        public bool status { get; set; }
        public string msg { get; set; }
    }
}
