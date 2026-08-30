using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPTestAPI.Models
{
    public class AreaModel
    {
        public long AreaId { get; set; }
        public String AreaCode { get; set; }
        public String AreaName { get; set; }
        public long CityId { get; set; }
        public String CityCode { get; set; }
        public String CityName { get; set; }
        public long StateId { get; set; }
        public String StateCode { get; set; }
        public String StateName { get; set; }
    }

    public class AreaRequestModel
    {
        public long AreaId { get; set; }
        public String AreaCode { get; set; }
        public String AreaName { get; set; }
        public long CityId { get; set; }
        public long StateId { get; set; }
    }

    public class AreaListModel
    {
        public List<AreaModel> data { get; set; }

        public bool status { get; set; }
        public string msg { get; set; }
    } 

    public class AreaDetailModel
    {
        public AreaModel data { get; set; }

        public bool status { get; set; }
        public string msg { get; set; }
    }

    public class AreaReturnModel
    {
        public bool status { get; set; }
        public string msg { get; set; }
    }
}
