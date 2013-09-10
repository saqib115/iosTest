using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;

/// <summary>
/// Summary description for Messages
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class Messages : System.Web.Services.WebService {

    public Messages () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string HelloWorld() {
        return "Hello World";
    }

    public class Message
    {
        public string id { get; set; }
        public DateTime DateTime { get; set; }
        public string MessageTo { get; set; }
        public string MessageFrom { get; set; }
        public string MessageHeader { get; set; }
        public string MessageBody { get; set; }
        public bool isRead { get; set; }
        public bool isDeleted { get; set; }
    }

    [WebMethod]
    public List<Message> GetMessagesByMessageTo(string MessageTo)
    {
        Message m_obj;
        List<Message> drlist = new List<Message>();
        SqlParameter[] myparam = null;
        myparam = SqlHelperParameterCache.GetSpParameterSet(BVisionConfigurationManager.GetConnectionString(), "sp_Messages");

        myparam[0].Value = 1;
        myparam[3].Value = MessageTo;
        DataTable dtMessage = SqlHelper.ExecuteDataset(BVisionConfigurationManager.GetConnectionString(), CommandType.StoredProcedure, "sp_Messages", myparam).Tables[0];
        for (int i = 0; i < dtMessage.Rows.Count; i++)
        {

            m_obj = new Message();
            m_obj.id = Convert.ToString(dtMessage.Rows[i]["id"]);
            if (Convert.ToString(dtMessage.Rows[i]["DateTime"]) != "" && Convert.ToString(dtMessage.Rows[i]["DateTime"]) != null)
            {
                m_obj.DateTime = Convert.ToDateTime(dtMessage.Rows[i]["DateTime"]);
            }
            m_obj.MessageFrom = Convert.ToString(dtMessage.Rows[i]["MessageFrom"]);
            m_obj.MessageHeader = Convert.ToString(dtMessage.Rows[i]["MessageHeader"]);


            drlist.Add(m_obj);
        }
        dtMessage.Dispose();

        return drlist;
    }
    
}
