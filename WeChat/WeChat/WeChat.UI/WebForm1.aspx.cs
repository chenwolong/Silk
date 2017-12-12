using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WeChat.UI
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            List<string> list = new List<string>();
            list.Add("SO201711020005");
            list.Add("SO201711020001");
            string json = JsonConvert.SerializeObject(list);
            Response.Write(json);

        }
    }
}