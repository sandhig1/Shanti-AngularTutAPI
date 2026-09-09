using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPTestAPI.Models
{
    public class StatusModel
    {
        public long StatusId { get; set; }
        public String StatusCode { get; set; }
        public String StatusName { get; set; }
        public String StatusFor { get; set; }
        public long SeqNo { get; set; }

    }

    public class StatusRequestModel
    {
        public long StatusId { get; set; }
        public String StatusCode { get; set; }
        public String StatusName { get; set; }
        public String StatusFor { get; set; }
        public long SeqNo { get; set; }

    }

    public class StatusListModel
    {
        public List<StatusModel> data { get; set; }

        public bool status { get; set; }
        public string msg { get; set; }
    } 

    public class StatusDetailModel
    {
        public StatusModel data { get; set; }

        public bool status { get; set; }
        public string msg { get; set; }
    }

    public class StatusReturnModel
    {
        public bool status { get; set; }
        public string msg { get; set; }
    }
}
