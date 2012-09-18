using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Specialized;
using Helpers;
using System.Data;
using System.Collections;

namespace PhotoData
{
    public partial class Months1 : System.Web.UI.Page
    {
        protected void PutMonthList()
        {
            Hashtable ht = new Hashtable();
            string s = "";

            AlbumListGenerator.SelectCommand = Helper.BuildMonthPhotoCountQuery(m_subquery, m_year);
            var list = AlbumListGenerator.Select(DataSourceSelectArguments.Empty);
            foreach (DataRowView row in list)
            {
                ht.Add((int)row["AlbumMonth"], (int)row["Total"]);
            }

            AlbumListGenerator.SelectCommand = Helper.BuildMonthQuery(m_subquery, m_year);
            list = AlbumListGenerator.Select(DataSourceSelectArguments.Empty);

            s += "<p style='margin-top:50px;'><center style='font-size:x-large'>Photos for the year of " + m_year + "</center>";

            List<DataRowView> onemonth = null;
            int currentmonth = -1;
            foreach (DataRowView row in list)
            {
                int month = (int)row["AlbumMonth"];

                if (ht.ContainsKey(month))
                {
                    if (month != currentmonth)
                    {
                        if (onemonth != null)
                        {
                            s += CreateHTMLForAMonth(onemonth, (int)ht[currentmonth], m_year);
                        }
                        currentmonth = month;
                        onemonth = new List<DataRowView>();
                    }
                    onemonth.Add(row);
                }
            }
            s += CreateHTMLForAMonth(onemonth, (int)ht[currentmonth], m_year);
            Response.Write(s);
        }

        string CreateHTMLForAMonth(List<DataRowView> rows, int count, string year)
        {
            if (rows == null)
            {
                return "No Photos";
            }
            Random r = new Random();
            return CreateHTMLForAMonth(rows[r.Next(rows.Count)], count, year);
        }

        string CreateHTMLForAMonth(DataRowView row, int count, string year)
        {
            string s = "";
            string u = "'Albums.aspx?ay=" + year + "&am=" + row["AlbumMonth"] + "'";
            s += "<table style='cursor:pointer; display:inline; margin:10px' onclick=donavigate(" + u + ")><tr><td>";
            s += "<span style='background:green; display:inline'><img style='margin:0px' src='" + row["AlbumPhoto"] + "'></span> ";
            s += "</td></tr><tr><td style='text-align:right; font-size:x-large'>";
            s += Helper.GetMonth((int)row["AlbumMonth"]);
            s += "<br><span style='font-size:x-small'>(" + count + ")</span>";
            s += "</td></tr></table>";

            return s;
        }
        
        protected void Page_Load(object sender, EventArgs e)
        {
            NameValueCollection coll = Request.QueryString;
            m_subquery = Helper.GetQueryValue(coll, "qry", "");
            m_year = Helper.GetQueryValue(coll, "ay", "2011");
        }

        private string m_subquery;
        private string m_year;
    }
}
