using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPTestAPI.Models
{
    public class StaffModel
    {
        public long StaffId { get; set; }
        public String StaffCode { get; set; }
        public String StaffName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public String DateOfBirthFormatted { get; set; }
        public String Gender { get; set; }
        public long Age { get; set; }
        public String MobileNo { get; set; }
        public String EmailAdd { get; set; }
        public String Address { get; set; }
        public DateTime JoiningDate { get; set; }
        public String JoiningDateFormatted { get; set; }
        public long StaffCategoryId { get; set; }
        public String StaffCategoryCode { get; set; }
        public String StaffCategoryName { get; set; }
        public long DepartmentId { get; set; }
        public String DepartmentCode { get; set; }
        public String DepartmentName { get; set; }
        public String Status { get; set; }

    }

    public class StaffRequestModel
    {
        public long StaffId { get; set; }
        public String StaffCode { get; set; }
        public String StaffName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public String DateOfBirthFormatted { get; set; }
        public String Gender { get; set; }
        public long Age { get; set; }
        public String MobileNo { get; set; }
        public String EmailAdd { get; set; }
        public String Address { get; set; }
        public DateTime JoiningDate { get; set; }
        public String JoiningDateFormatted { get; set; }
        public long StaffCategoryId { get; set; }
        public String StaffCategoryCode { get; set; }
        public String StaffCategoryName { get; set; }
        public long DepartmentId { get; set; }
        public String DepartmentCode { get; set; }
        public String DepartmentName { get; set; }
        public String Status { get; set; }


    }

    public class StaffListModel
    {
        public List<StaffModel> data { get; set; }

        public bool status { get; set; }
        public string msg { get; set; }
    } 

    public class StaffDetailModel
    {
        public StaffModel data { get; set; }

        public bool status { get; set; }
        public string msg { get; set; }
    }

    public class StaffReturnModel
    {
        public bool status { get; set; }
        public string msg { get; set; }
    }
}
