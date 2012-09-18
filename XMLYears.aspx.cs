using System;
using System.Collections.Specialized;
using System.Data;
using System.Security;
using System.Data.SqlClient;
using System.Web.UI;
using System.Collections;
using Helpers;
using System.Collections.Generic;

namespace NewPV.Web
{
    public partial class MainData : System.Web.UI.Page
    {
        protected void PutYearList()
        {
            Hashtable ht = new Hashtable();
            string s = "";

            AlbumListGenerator.SelectCommand = Helper.BuildYearPhotoCountQuery(m_subquery);
            var list = AlbumListGenerator.Select(DataSourceSelectArguments.Empty);
            foreach (DataRowView row in list)
            {
                ht.Add((int)row["AlbumYear"], (int)row["Total"]);
            }

            AlbumListGenerator.SelectCommand = Helper.BuildYearQuery(m_subquery);
            list = AlbumListGenerator.Select(DataSourceSelectArguments.Empty);

            List<DataRowView> oneyear = null;
            int currentyear = -1;
            bool fFirst = true;
            foreach (DataRowView row in list)
            {
                int year = (int)row["AlbumYear"];

                if (ht.ContainsKey(year))
                {
                    if (year != currentyear)
                    {
                        if (oneyear != null)
                        {
                            s += CreateHTMLForAYear(oneyear, (int)ht[currentyear], ref fFirst);
                        }
                        currentyear = year;
                        oneyear = new List<DataRowView>();
                    }
                    oneyear.Add(row);
                }
            }
            s += CreateHTMLForAYear(oneyear, (int)ht[currentyear], ref fFirst);
            Response.Write(s);
        }

        string CreateHTMLForAYear(List<DataRowView> rows, int count, ref bool fFirst)
        {
            if (rows == null)
            {
                return "No Photos";
            }
            Random r = new Random();
            return CreateHTMLForAYear(rows[r.Next(rows.Count)], count, ref fFirst);
        }

        string CreateHTMLForAYear(DataRowView row, int count, ref bool fFirst)
        {
            string s = "";
            string u = "'Months.aspx?ay=" + row["AlbumYear"] + "'";
            string img = (string)row["AlbumPhoto"];
            if (fFirst)
            {
                img = img.Replace("_s.jpg", ".jpg");
            }

            s += "<Year";
            s += " Y='" + row["AlbumYear"] + "'";
            s += " Photo='" + row["AlbumPhoto"] + "'";
            s += " Count='" + count + "'";
            s += "/>\r\n";
            
            fFirst = false;
            return s;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            NameValueCollection coll = Request.QueryString;
            m_subquery = Helper.GetQueryValue(coll, "qry", "");
        }

        private string m_subquery = "";
        private string m_latestalbum = "";
    }
}
