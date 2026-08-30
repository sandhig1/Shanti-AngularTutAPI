using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPTestAPI.Models
{
    public class StateModel
    {
        public long StateId { get; set; }
        public String StateCode { get; set; }
        public String StateName { get; set; }
    }

    public class StateListModel
    {
        public List<StateModel> data { get; set; }

        public bool status { get; set; }
        public string msg { get; set; }
    } 

    public class StateDetailModel
    {
        public StateModel data { get; set; }

        public bool status { get; set; }
        public string msg { get; set; }
    }

    public class StateReturnModel
    {
        public bool status { get; set; }
        public string msg { get; set; }
    }
}
