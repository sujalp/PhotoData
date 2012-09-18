using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Web.UI;
using Helpers;

namespace PhotoData
{
    public partial class Years : System.Web.UI.Page
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

            s += "<p style='margin-top:50px;'>";

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
            string alignleft = "";
            string fontsz = "x-large";
            string fontsz1 = "x-small";
            if (fFirst)
            {
                img = img.Replace("_s.jpg", ".jpg");
                fFirst = false;
                alignleft = "float:left; width:500; ";
                fontsz = "xx-large";
                fontsz1 = "small";
            }

            s += "<table style='cursor:pointer; display:inline; vertical-align:top; " + alignleft + "margin:10px' onclick=donavigate(" + u + ")><tr><td>";
            s += "<span style='background:green; display:inline'><img style='margin:0px' src='" + img + "'></span> ";
            s += "</td></tr><tr><td style='text-align:right; font-size:" + fontsz + "'>";
            s += row["AlbumYear"];
            s += "<br><span style='font-size:" + fontsz1 + "'>(" + count + ")</span>";
            s += "</td></tr></table>";
            return s;
        }

#if NOTYET
        string GetAnAlbumForYear(int year)
        {
            string s = "";

            string sq = "(AlbumYear = " + year;
            sq += m_subquery == "" ? ")" : (" AND " + m_subquery + ")");
            AlbumListGenerator2.SelectCommand =BuildYearQuery(sq);
            var slct = AlbumListGenerator2.Select(DataSourceSelectArguments.Empty);

            List<DataRowView> list = new List<DataRowView>();
            foreach (DataRowView row1 in slct)
            {
                list.Add(row1);
            }

            Random r = new Random();
            DataRowView row = list[r.Next(list.Count)];

            s += "<div style='margin-left:10px; font-size:xx-large'>" + row["AlbumTitle"] + " (" + row["AlbumYear"] + ", " + Helper.GetMonth((int)row["AlbumMonth"]) + ")</div>";
            s += GetAnAlbum((string)row["AlbumHash"]);

            return s;
        }

        string GetAnAlbum(string albumHash)
        {
            string s = "";
            AlbumListGenerator2.SelectCommand = BuildAlbumQuery(albumHash, m_subquery);
            var slct = AlbumListGenerator2.Select(DataSourceSelectArguments.Empty);

            string u = "'OneAlbum.aspx?ah=" + albumHash + "'";
            foreach (DataRowView row in slct)
            {
                string src = Helper.GetUrl(row, "_s");
                s += "<table style='cursor:pointer; display:inline; margin:10px' onclick=donavigate(" + u + ")><tr><td>";
                s += "<span style='background:green; display:inline'><img style='margin:0px' src='" + src + "'></span> ";
                s += "</td></tr><tr><td style='text-align:right; font-size:x-large'>";
                s += "</td></tr></table>";
            }

            return s;
        }
#endif

        protected void Page_Load(object sender, EventArgs e)
        {
            NameValueCollection coll = Request.QueryString;
            m_subquery = Helper.GetQueryValue(coll, "qry", "");
        }

        private string m_subquery = "";
    }
}
