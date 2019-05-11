using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

//Content model
namespace WebMediaPortfolioTemplate.Models
{
    public class Content_Model
    {
        public string CONTENT_ID { get; set; }
        public string DESCRIPTION { get; set; }
        public string URL { get; set; }
        public string VIDEO_URL { get; set; }

        public string VALID_FROM { get; set; }
        public string VALID_TO { get; set; }
        public string CREATE_BY { get; set; }
        public string CREATE_DATE { get; set; }
        public string UPDATE_BY { get; set; }
        public string UPDATE_DATE { get; set; }
    }
}