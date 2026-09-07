using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPTestAPI.Models
{
    public class DepartmentModel
    {
        public long DepartmentId { get; set; }
        public String DepartmentCode { get; set; }
        public String DepartmentName { get; set; }
        public String Description { get; set; }

    }

    public class DepartmentRequestModel
    {
        public long DepartmentId { get; set; }
        public String DepartmentCode { get; set; }
        public String DepartmentName { get; set; }
        public String Description { get; set; }

    }

    public class DepartmentListModel
    {
        public List<DepartmentModel> data { get; set; }

        public bool status { get; set; }
        public string msg { get; set; }
    } 

    public class DepartmentDetailModel
    {
        public DepartmentModel data { get; set; }

        public bool status { get; set; }
        public string msg { get; set; }
    }

    public class DepartmentReturnModel
    {
        public bool status { get; set; }
        public string msg { get; set; }
    }
}
