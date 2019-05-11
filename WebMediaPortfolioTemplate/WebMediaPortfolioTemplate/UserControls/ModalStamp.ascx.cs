using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

//ModalStamp user control consists of fixed fields located at the bottom of modal
namespace WebMediaPortfolioTemplate.UserControls
{
    public partial class ModalStamp : System.Web.UI.UserControl
    {
        public HtmlInputControl TxtValidFrom
        {
            get { return txtValidFrom; }
        }

        public HtmlInputControl TxtValidTo
        {
            get { return txtValidTo; }
        }

        public Label LblCreateBy
        {
            get { return lblCreateBy; }
        }

        public Label LblCreateDate
        {
            get { return lblCreateDate; }
        }

        public Label LblUpdateBy
        {
            get { return lblUpdateBy; }
        }

        public Label LblUpdateDate
        {
            get { return lblUpdateDate; }
        }

    }
}