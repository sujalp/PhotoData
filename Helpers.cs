using System.Collections.Specialized;
using System.Data;
using System.Security;
using System;

namespace Helpers
{
    public static class Helper
    {
        public static string GetMonth(int m)
        {
            switch (m)
            {
                case 1: return "Jan";
                case 2: return "Feb";
                case 3: return "March";
                case 4: return "April";
                case 5: return "May";
                case 6: return "June";
                case 7: return "July";
                case 8: return "Aug";
                case 9: return "Sept";
                case 10: return "Oct";
                case 11: return "Nov";
                case 12: return "Dec";
                default: return "Unknown";
            }
        }

        public static string GetQueryValue(NameValueCollection coll, string paramName, string defaultValue)
        {
            string[] values = coll.GetValues(paramName);
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

        public static int GetQueryValue(NameValueCollection coll, string paramName, int defaultValue)
        {
            int retval = defaultValue;
            string s = coll.Get(paramName);
            if (s != null)
            {
                int result;
                if (int.TryParse(s, out result))
                {
                    retval = result;
                }
            }

            return retval;
        }

        public static string GetPhotoString(char what, object o)
        {
            string s;
            DataRowView drv = (DataRowView)o;
            switch (what)
            {
                case 'U':
                    s = (string)(drv["FileName"]);
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
                case 'X':
                    s = (string)(drv["AlbumStory"]);
                    break;
                case 'R':
                    s = (string)(drv["Rects"]);
                    break;
                default:
                    s = "Unknown";
                    break;
            }
            s = SecurityElement.Escape(s);
            return s;
        }

        private const string photoUrl = "http://farm{0}.static.flickr.com/{1}/{2}_{3}{4}.{5}";

        private static string UrlFormat(string format, params object[] parameters)
        {
            return String.Format(format, parameters);
        }

        public static string GetUrl(object o, string size)
        {
            string FlickrId = GetPhotoString('I', o);
            string FlickrSecret = GetPhotoString('S', o);
            string FlickrFarm = GetPhotoString('F', o);
            string FlickrServer = GetPhotoString('V', o);
            return UrlFormat(photoUrl, FlickrFarm, FlickrServer, FlickrId, FlickrSecret, size, "jpg");
        }

        public static string BuildYearPhotoCountQuery(string subquery)
        {
            string myQuery = @"SELECT DISTINCTROW FinalPhotos.AlbumYear, Count(*) AS Total FROM FinalPhotos ";
            if (subquery != "")
                myQuery += "WHERE (" + subquery + ") ";
            myQuery += @"GROUP BY FinalPhotos.Albums.AlbumYear ";
            myQuery += @"ORDER BY FinalPhotos.Albums.AlbumYear DESC";
            return myQuery;
        }

        public static string BuildYearQuery(string subquery)
        {
            string myYearQuery = @"SELECT * FROM Albums ";
            if (subquery != "")
                myYearQuery += "WHERE (" + subquery + ") ";
            myYearQuery += @"ORDER BY AlbumYear DESC, AlbumMonth DESC";
            return myYearQuery;
        }

        public static string BuildMonthPhotoCountQuery(string subquery, string year)
        {
            string myQuery = @"SELECT DISTINCTROW FinalPhotos.AlbumMonth, Count(*) AS Total FROM FinalPhotos ";
            myQuery += "WHERE ((AlbumYear = " + year + ")";
            if (subquery != "")
            {
                myQuery += "AND (" + subquery + ")";
            }
            myQuery += ") ";
            myQuery += @"GROUP BY FinalPhotos.Albums.AlbumMonth ";
            myQuery += @"ORDER BY FinalPhotos.Albums.AlbumMonth ASC";
            return myQuery;
        }

        public static string BuildMonthQuery(string subquery, string year)
        {
            string mQuery = @"SELECT * FROM Albums ";
            mQuery += "WHERE ((AlbumYear = " + year + ")";
            if (subquery != "")
                mQuery += " AND (" + subquery + ") ";
            mQuery += ") ";
            mQuery += @"ORDER BY AlbumMonth DESC";
            return mQuery;
        }

        public static string BuildAlbumPhotoCountQuery(string subquery, string year, string month)
        {
            string myQuery = @"SELECT DISTINCTROW FinalPhotos.AlbumHash, Count(*) AS Total FROM FinalPhotos ";
            myQuery += "WHERE ((AlbumYear = " + year + ") AND (AlbumMonth = " + month + ")";
            if (subquery != "")
                myQuery += "AND (" + subquery + ")";
            myQuery += ") ";
            myQuery += @"GROUP BY FinalPhotos.Albums.AlbumHash ";
            myQuery += @"ORDER BY FinalPhotos.Albums.AlbumHash ASC";
            return myQuery;
        }

        public static string BuildAlbumQuery(string subquery, string year, string month)
        {
            string mQuery = @"SELECT * FROM Albums ";
            mQuery += "WHERE ((AlbumYear = " + year + ") AND (AlbumMonth = " + month + ")";
            if (subquery != "")
                mQuery += " AND (" + subquery + ") ";
            mQuery += ") ";
            mQuery += @"ORDER BY AlbumHash ASC";
            return mQuery;
        }
    }
}
