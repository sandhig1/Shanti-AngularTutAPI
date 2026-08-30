using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPTestAPI.Models
{
    public class EnquiryModel
    {
        public long EnquiryId { get; set; }
        public String EnquiryNo { get; set; }
        public DateTime EnquiryDate { get; set; }
        //public String EnquiryDateFormatted { get; set; }
        public String CustomerName { get; set; }
        public String CustomerAddress { get; set; }
        public String PhoneNo { get; set; }
        public String EmailAdd { get; set; }
        public String EnquiryDetail { get; set; }
    }

    public class EnquiryListModel
    {
        public List<EnquiryModel> data { get; set; }

        public bool status { get; set; }
        public string msg { get; set; }
    } 

    public class EnquiryDetailModel
    {
        public EnquiryModel data { get; set; }

        public bool status { get; set; }
        public string msg { get; set; }
    }

    public class EnquiryReturnModel
    {
        public bool status { get; set; }
        public string msg { get; set; }
    }
}
