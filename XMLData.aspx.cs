using System;
using System.Collections;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Security;

namespace NewPV.Web
{
    public partial class XMLData : System.Web.UI.Page
    {
        private void CaptureAlbumId(string s)
        {
            if (m_AlbumId == -1)
            {
                try
                {
                    m_AlbumId = int.Parse(s);
                    if (m_AlbumId < 0)
                    {
                        m_AlbumId = -1;
                    }
                }
                catch
                {
                    m_AlbumId = -1;
                }
            }
        }

        public string GetString(char what, object o)
        {
            string s;
            DataRowView drv = (DataRowView)o;
            switch (what)
            {
                case 'T':
                    s = String.Format("{0}", drv["AlbumTitle"]);
                    break;
                case 'Y':
                    s = String.Format("{0}", drv["AlbumYear"]);
                    break;
                case 'M':
                    s = String.Format("{0}", drv["AlbumMonth"]);
                    break;
                case 'I':
                    s = String.Format("{0}", drv["AlbumId"]);
                    CaptureAlbumId(s);
                    break;
                case 'C':
                    s = String.Format("{0}", drv["Total"]);
                    break;
                default:
                    s = "Unknown";
                    break;
            }
            s = SecurityElement.Escape(s);
            return s;
        }

        private static string Transform(string s)
        {
            s = s.Replace('\\', '_');
            s = s.Replace(':', '_');
            return s;
        }

        public string GetPhotoString(char what, object o)
        {
            string s;
            DataRowView drv = (DataRowView)o;
            switch (what)
            {
                case 'U':
                    s = (string)(drv["FileName"]);
                    s = Transform(s);
                    break;
                case 'A':
                    s = (string)(drv["AlbumTitle"]);
                    break;
                case 'P':
                    s = (string)(drv["People"]);
                    break;
                case 'L':
                    s = (string)(drv["PlaceName"]);
                    break;
                case 'D':
                    s = String.Format("{0}/{1}/{2}", drv["Month_"], drv["Date_"], drv["Year_"]);
                    break;
                case 'T':
                    s = (string)(drv["PhotoTitle"]);
                    break;
                default:
                    s = "Unknown";
                    break;
            }
            s = SecurityElement.Escape(s);
            return s;
        }

        private string GetQueryValue(NameValueCollection coll, string paramName, string defaultValue)
        {
            String[] values = coll.GetValues(paramName);
            if (values != null)
            {
                foreach (string val in values)
                {
                    if (val != null)
                    {
                        return val;
                    }
                }
            }
            return defaultValue;
        }

        private string BuildAlbumQuery(string subquery)
        {
            string myAlbumQuery = @"SELECT DISTINCTROW FinalPhotos.AlbumTitle, FinalPhotos.AlbumYear, FinalPhotos.AlbumMonth, FinalPhotos.AlbumId, Count(*) AS [Total] " +
                @"FROM FinalPhotos ";
            if (subquery != "")
                myAlbumQuery += "WHERE (" + subquery + ") ";
            myAlbumQuery += @"GROUP BY FinalPhotos.AlbumTitle, FinalPhotos.AlbumYear, FinalPhotos.AlbumMonth, FinalPhotos.AlbumId ";
            myAlbumQuery += @"ORDER BY FinalPhotos.AlbumYear DESC , FinalPhotos.AlbumMonth DESC , FinalPhotos.AlbumId DESC";
            return myAlbumQuery;
        }

        private string BuildPhotoQuery(string subquery)
        {
            string mySelectQuery = @"SELECT * FROM FinalPhotos WHERE AlbumId = " + m_AlbumId;
            if (subquery != "")
            {
                mySelectQuery += " And (" + subquery + ")";
            }
            return mySelectQuery;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string subquery;
            string albumid = "-1";

            NameValueCollection coll = Request.QueryString;
            albumid = GetQueryValue(coll, "aid", albumid);
            subquery = GetQueryValue(coll, "qry", "");

            CaptureAlbumId(albumid);

            AlbumListGenerator.SelectCommand = BuildAlbumQuery(subquery);
            Albums.DataSource = AlbumListGenerator;
            Albums.DataBind();

            AlbumListGenerator.SelectCommand = BuildPhotoQuery(subquery);
            Photos.DataSource = AlbumListGenerator;
            Photos.DataBind();
        }

        private int m_AlbumId = -1;
    }
}