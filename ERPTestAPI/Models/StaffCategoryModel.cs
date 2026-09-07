using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPTestAPI.Models
{
    public class StaffCategoryModel
    {
        public long StaffCategoryId { get; set; }
        public String StaffCategoryCode { get; set; }
        public String StaffCategoryName { get; set; }
        public String Description { get; set; }

    }

    public class StaffCategoryRequestModel
    {
        public long StaffCategoryId { get; set; }
        public String StaffCategoryCode { get; set; }
        public String StaffCategoryName { get; set; }
        public String Description { get; set; }

    }

    public class StaffCategoryListModel
    {
        public List<StaffCategoryModel> data { get; set; }

        public bool status { get; set; }
        public string msg { get; set; }
    } 

    public class StaffCategoryDetailModel
    {
        public StaffCategoryModel data { get; set; }

        public bool status { get; set; }
        public string msg { get; set; }
    }

    public class StaffCategoryReturnModel
    {
        public bool status { get; set; }
        public string msg { get; set; }
    }
}
