using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Helpers;
using System.Data;

namespace PhotoData
{
    public partial class Query : System.Web.UI.Page
    {
        string m_qs = null;

        protected void PutPhotoList()
        {
            AlbumListGenerator.SelectCommand = m_qs;
            var list = AlbumListGenerator.Select(DataSourceSelectArguments.Empty);
            foreach (DataRowView row in list)
            {
                Response.Write("<Row ");
                foreach (DataColumn col in row.DataView.Table.Columns)
                {
                    Response.Write(col.ColumnName + "=\"");
                    if (col.DataType == typeof(string))
                    {
                        string s = (string)row[col.ColumnName];
                        s = s.Replace("&", "&amp;");
                        s = s.Replace("<", "&lt;");
                        s = s.Replace('"', ' ');
                        Response.Write(s);
                    }
                    else
                    {
                        Response.Write(row[col.ColumnName]);
                    }
                    Response.Write("\" ");
                }
                Response.Write("/>\r\n");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            var coll = Request.QueryString;
            m_qs = Helper.GetQueryValue(coll, "q", null);
        }
    }
}
