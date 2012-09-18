using System;
using System.Collections;
using System.Collections.Specialized;
using System.Data;
using System.Web.UI;
using Helpers;

namespace PhotoData
{
    public partial class XMLAlbums : System.Web.UI.Page
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
            Response.Write(s);
        }

        string CreateHTMLForAnAlbum(DataRowView row, int count, string year, string month)
        {
            string s = "";

            s += "<Album AH='" + row["AlbumHash"] + "'";
            s += " Photo='" + row["AlbumPhoto"] + "'";
            s += " Title='" + row["AlbumTitle"] + "'";
            s += " Count='" + count + "'";
            s += "/>\r\n";

            return s;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            NameValueCollection coll = Request.QueryString;
            m_subquery = Helper.GetQueryValue(coll, "qry", "");
            m_year = Helper.GetQueryValue(coll, "ay", "2011");
            m_month = Helper.GetQueryValue(coll, "am", "1");
        }

        private string m_subquery;
        private string m_year;
        private string m_month;
    }
}
