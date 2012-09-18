using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Specialized;
using Helpers;
using System.Collections;
using System.Data;

namespace PhotoData
{
    public partial class Albums : System.Web.UI.Page
    {
        protected void PutAlbumList()
        {
            Hashtable ht = new Hashtable();
            string s = "";

            AlbumListGenerator.SelectCommand = Helper.BuildAlbumPhotoCountQuery(m_subquery, m_year, m_month);
            var list = AlbumListGenerator.Select(DataSourceSelectArguments.Empty);
            foreach (DataRowView row in list)
            {
                ht.Add((string)row["AlbumHash"], (int)row["Total"]);
            }

            AlbumListGenerator.SelectCommand = Helper.BuildAlbumQuery(m_subquery, m_year, m_month);
            list = AlbumListGenerator.Select(DataSourceSelectArguments.Empty);

            s += "<p style='margin-top:50px;'><center style='font-size:x-large'>Photos for the month of ";
            s += Helper.GetMonth(int.Parse(m_month)) + " ";
            s += "<span style='cursor:pointer' onclick=\"donavigate('Months.aspx?ay=" + m_year;
            s += "')\">" + m_year + "</span></center>";

            string currentalbum = "";
            foreach (DataRowView row in list)
            {
                string album = (string)row["AlbumHash"];

                if (ht.ContainsKey(album))
                {
                    if (album != currentalbum)
                    {
                        s += CreateHTMLForAnAlbum(row, (int)ht[album], m_year, m_month);
                        currentalbum = album;
                    }
                }
            }
            s = "<span style='vertical-align:top'>" + s + "</span>";
            Response.Write(s);
        }

        string CreateHTMLForAnAlbum(DataRowView row, int count, string year, string month)
        {
            string s = "";
            string u = "'OneAlbum.aspx?ah=" + row["AlbumHash"] + "'";
            s += "<table style='cursor:pointer; display:inline; margin:10px' onclick=donavigate(" + u + ")><tr><td align=right>";
            s += "<span style='background:green; display:inline'><img style='margin:0px' src='" + row["AlbumPhoto"] + "'></span> ";
            s += "</td></tr><tr><td style='text-align:right; width:150px'>";
            s += row["AlbumTitle"];
            s += "<span style='font-size:x-small'> (" + count + ")</span>";
            s += "</td></tr></table>";

            return s;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            NameValueCollection coll = Request.QueryString;
            m_subquery = Helper.GetQueryValue(coll, "qry", "");
            m_year = Helper.GetQueryValue(coll, "ay", "2010");
            m_month = Helper.GetQueryValue(coll, "am", "1");
        }

        private string m_subquery;
        private string m_year;
        private string m_month;
    }
}
