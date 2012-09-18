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
    public partial class Months : System.Web.UI.Page
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

            s += "<Month";
            s += " M='" + row["AlbumMonth"] + "'";
            s += " Photo='" + row["AlbumPhoto"] + "'";
            s += " Count='" + count + "'";
            s += "/>\r\n";

            return s;
        }

        public void PutMonthListX()
        {
            Hashtable ht = new Hashtable();

            AlbumListGenerator.SelectCommand = Helper.BuildMonthPhotoCountQuery(m_subquery, m_year);
            var list = AlbumListGenerator.Select(DataSourceSelectArguments.Empty);
            foreach (DataRowView row in list)
            {
                ht.Add((int)row["AlbumMonth"], (int)row["Total"]);
            }

            AlbumListGenerator.SelectCommand = Helper.BuildMonthQuery(m_subquery, m_year);
            list = AlbumListGenerator.Select(DataSourceSelectArguments.Empty);
            foreach (DataRowView row in list)
            {
                //m_latestalbum = (string)row["AlbumHash"];
                break;
            } 
            
            int currentmonth = -1;
            foreach (DataRowView row in list)
            {
                int month = (int)row["AlbumMonth"];

                if (ht.ContainsKey(month))
                {
                    if (month != currentmonth)
                    {
                        currentmonth = month;
                        string s = "";
                        s += "<Month";
                        s += " M='" + row["AlbumMonth"] + "'";
                        s += " Photo='" + row["AlbumPhoto"] + "'";
                        s += " Count='" + ht[month] + "'";
                        s += "/>\r\n";
                        Response.Write(s);
                    }
                }
            }
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
