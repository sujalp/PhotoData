using System;
using System.Collections.Specialized;
using System.Data;
using System.Security;

namespace NewPV.Web
{
    public partial class MainData : System.Web.UI.Page
    {
        private void CaptureAlbumHash(string s)
        {
            if (m_AlubmHash == "")
            {
                m_AlubmHash = s;
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
                    break;
                case 'H':
                    s = String.Format("{0}", drv["AlbumHash"]);
                    CaptureAlbumHash(s);
                    break;
                case 'P':
                    s = String.Format("{0}", drv["AlbumPhoto"]);
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
                case 'I':
                    s = (string)(drv["FlickrId"]);
                    break;
                case 'S':
                    s = (string)(drv["FlickrSecret"]);
                    break;
                case 'O':
                    s = (string)(drv["FlickrOSecret"]);
                    break;
                case 'F':
                    s = (string)(drv["FlickrFarm"]);
                    break;
                case 'V':
                    s = (string)(drv["FlickrServer"]);
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
            string myAlbumQuery = @"SELECT DISTINCTROW FinalPhotos.AlbumTitle, FinalPhotos.AlbumYear, FinalPhotos.AlbumMonth, FinalPhotos.AlbumId, FinalPhotos.AlbumHash, FinalPhotos.AlbumPhoto, Count(*) AS [Total] " +
                @"FROM FinalPhotos ";
            if (subquery != "")
                myAlbumQuery += "WHERE (" + subquery + ") ";
            myAlbumQuery += @"GROUP BY FinalPhotos.AlbumTitle, FinalPhotos.AlbumYear, FinalPhotos.AlbumMonth, FinalPhotos.AlbumId, FinalPhotos.AlbumHash, FinalPhotos.AlbumPhoto ";
            myAlbumQuery += @"ORDER BY FinalPhotos.AlbumYear DESC , FinalPhotos.AlbumMonth DESC , FinalPhotos.AlbumId DESC";
            return myAlbumQuery;
        }

        private string BuildPhotoQuery(string subquery)
        {
            string mySelectQuery = @"SELECT * FROM FinalPhotos WHERE AlbumHash = '" + m_AlubmHash + "' ";
            if (subquery != "")
            {
                mySelectQuery += " And (" + subquery + ")";
            }
            return mySelectQuery;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string subquery;
            string albumid = "";

            NameValueCollection coll = Request.QueryString;
            albumid = GetQueryValue(coll, "aid", albumid);
            subquery = GetQueryValue(coll, "qry", "");

            CaptureAlbumHash(albumid);

            AlbumListGenerator.SelectCommand = BuildAlbumQuery(subquery);
            Albums.DataSource = AlbumListGenerator;
            Albums.DataBind();

            AlbumListGenerator.SelectCommand = BuildPhotoQuery(subquery);
            Photos.DataSource = AlbumListGenerator;
            Photos.DataBind();
        }

        private string m_AlubmHash = "";
    }
}
