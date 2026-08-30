using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPTestAPI.Models
{
    public class CityModel
    {
        public long CityId { get; set; }
        public String CityCode { get; set; }
        public String CityName { get; set; }
        public long StateId { get; set; }
        public String StateCode { get; set; }
        public String StateName { get; set; }
    }

    public class CityListModel
    {
        public List<CityModel> data { get; set; }

        public bool status { get; set; }
        public string msg { get; set; }
    } 

    public class CityDetailModel
    {
        public CityModel data { get; set; }

        public bool status { get; set; }
        public string msg { get; set; }
    }

    public class CityReturnModel
    {
        public bool status { get; set; }
        public string msg { get; set; }
    }
}
