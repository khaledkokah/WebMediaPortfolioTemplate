using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

//CodingType model
namespace WebMediaPortfolioTemplate.Models
{
    public class CodingType_Model
    {
        public string CODINGTYPE_ID { get; set; }
        public string DESCRIPTION { get; set; }

        public string VALID_FROM { get; set; }
        public string VALID_TO { get; set; }
        public string CREATE_BY { get; set; }
        public string CREATE_DATE { get; set; }
        public string UPDATE_BY { get; set; }
        public string UPDATE_DATE { get; set; }

    }
}