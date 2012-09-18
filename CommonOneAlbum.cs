using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Helpers;
using System.Collections.Specialized;
using System.Data;
using System.Xml;
using System.Text;
using System.IO;

namespace PhotoData
{
    public class CommonOneAlbum : System.Web.UI.Page
    {
        public CommonOneAlbum(string subquery, string albumhash, AccessDataSource generator, HttpResponse response)
        {
            m_subquery = subquery;
            m_albumHash = albumhash;
            m_generator = generator;
            m_response = response;
        }

        private string BuildPhotoQuery(string subquery, string albumHash)
        {
            string mQuery = @"SELECT * FROM FinalPhotos ";
            mQuery += "WHERE ((AlbumHash = '" + albumHash + "')";
            if (subquery != "")
                mQuery += " AND (" + subquery + ") ";
            mQuery += ") ";
            mQuery += "ORDER BY FileName ASC";
            return mQuery;
        }

        public void PutXMLPhotoList()
        {
            string s = "";

            if (m_albumHash != "")
            {
                int countit = 0;
                string mpfs = "";
                bool foundmpfs = false;
                m_generator.SelectCommand = BuildPhotoQuery(m_subquery, m_albumHash);
                var list = m_generator.Select(DataSourceSelectArguments.Empty);
                foreach (DataRowView row in list)
                {
                    countit++;
                    if (!foundmpfs)
                    {
                        mpfs = (string)row["AlbumPhoto"];
                        if (mpfs != "")
                        {
                            var sa = mpfs.Split('_');
                            if (sa.Length == 3)
                            {
                                mpfs = sa[1];
                                foundmpfs = true;
                            }
                        }
                    }
                }

                m_generator.SelectCommand = BuildPhotoQuery(m_subquery, m_albumHash);
                list = m_generator.Select(DataSourceSelectArguments.Empty);

                if (countit != 0)
                {
                    foreach (DataRowView row in list)
                    {
                        s += "<PhotoEntry " ;
                        s += " Photo='" + Helper.GetUrl(row, "") + "'";
                        s += " Title='" + ((string)row["PhotoTitle"]).Replace("'", "&apos;") + "'";
                        s += " AlbumTitle='" + ((string)row["AlbumTitle"]).Replace("'", "&apos;") + "'";
                        s += " Main='";
                        s += (((string)row["FlickrSecret"] == mpfs) ? "y" : "n") + "'";
                        s += "/>\r\n";
                    }
                }
            }
            m_response.Write(s);
        }

        public void PutPhotoList()
        {
            string s = "";

            if (m_albumHash == "")
            {
                s = "No Photos.";
            }
            else
            {
                int countit = 0;
                string mpfs = "";
                bool foundmpfs = false;
                m_generator.SelectCommand = BuildPhotoQuery(m_subquery, m_albumHash);
                var list = m_generator.Select(DataSourceSelectArguments.Empty);
                foreach (DataRowView row in list)
                {
                    countit++;
                    if (!foundmpfs)
                    {
                        mpfs = (string)row["AlbumPhoto"];
                        if (mpfs != "")
                        {
                            var sa = mpfs.Split('_');
                            if (sa.Length == 3)
                            {
                                mpfs = sa[1];
                                foundmpfs = true;
                            }
                        }
                    }
                }

                m_generator.SelectCommand = BuildPhotoQuery(m_subquery, m_albumHash);
                list = m_generator.Select(DataSourceSelectArguments.Empty);

                bool fFound = false;
                foreach (DataRowView row in list)
                {
                    fFound = true;
                    s += "<p style='margin-top:50px;'><center style='font-size:x-large'>";
                    s += row["AlbumTitle"] + " (";
                    s += "<span style='cursor:pointer' onclick=\"donavigate('Albums.aspx?ay=" + row["AlbumYear"] + "&am=" + row["AlbumMonth"] + "')\">" + Helper.GetMonth((int)row["AlbumMonth"]) + "</span>  ";
                    s += "<span style='cursor:pointer' onclick=\"donavigate('Months.aspx?ay=" + row["AlbumYear"] + "')\">" + row["AlbumYear"] + " - " + countit + " photos</span>)";
                    s += "</center>";
                    break;
                }
                if (!fFound)
                {
                    s += "No Photos.";
                }
                else
                {
                    int count = 0;
                    foreach (DataRowView row in list)
                    {
                        if ((string)row["FlickrSecret"] == mpfs)
                        {
                            string u = "AlbumView.aspx?ah=" + m_albumHash + "&ph=" + count.ToString();
                            s += "<table style='margin:20px; border:1px red; float:left'><tr><td width=300 align=center>";
                            s += "<img onclick=donavigate('" + u + "') style='cursor:pointer' src='" + Helper.GetUrl(row, "") + "'/><br>";
                            s += "<div>" + Helper.GetPhotoString('X', row) + "</div>";
                            s += "</td></tr></table>";
                            //s += "<img onclick=donavigate('" + u + "') style='float:left; cursor:pointer; margin:20px' src='" + Helper.GetUrl(row, "") + "'/>";
                            break;
                        }
                        count++;
                    }

                    count = 0;
                    foreach (DataRowView row in list)
                    {
                        if ((string)row["FlickrSecret"] != mpfs)
                        {
                            string u = "AlbumView.aspx?ah=" + m_albumHash + "&ph=" + count.ToString();
                            s += "<img onclick=donavigate('" + u + "') style='cursor:pointer; margin:20px' src='" + Helper.GetUrl(row, "_s") + "'/>";
                        }
                        count++;
                    }
                }
            }

            m_response.Write(s);
        }

        private string m_subquery;
        private string m_albumHash;
        private AccessDataSource m_generator;
        private HttpResponse m_response;
    }
}
