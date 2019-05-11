using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

//Coding model
namespace WebMediaPortfolioTemplate.Models
{
    public class Coding_Model
    {
        public string Coding_Id { get; set; }
        public string Coding_Description { get; set; }

        public string VALID_FROM { get; set; }
        public string VALID_TO { get; set; }
        public string CREATE_BY { get; set; }
        public string CREATE_DATE { get; set; }
        public string UPDATE_BY { get; set; }
        public string UPDATE_DATE { get; set; }

    }
}