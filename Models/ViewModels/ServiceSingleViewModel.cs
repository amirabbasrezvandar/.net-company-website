using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace sherkaty.Models.ViewModels
{
    public class ServiceSingleViewModel
    {
        public tbl_service service { get; set; }

        public tbl_service_second secon { get; set; }

        public tbl_service_card card { get; set; }

        public tbl_serivice_fea fea { get; set; }



        public List<tbl_service> see { get; set; }
    }
}