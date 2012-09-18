using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Specialized;
using Helpers;
using System.Data;
using System.Globalization;

namespace PhotoData
{
    public partial class AlbumView : System.Web.UI.Page
    {
        protected void PutPhotoList()
        {
            string s = "";

            if (m_albumHash == "")
            {
                s = "No Photos.";
            }
            else
            {
                AlbumListGenerator.SelectCommand = BuildPhotoQuery(m_subquery, m_albumHash);
                var list = AlbumListGenerator.Select(DataSourceSelectArguments.Empty);

                bool fFound = false;
                foreach (DataRowView row in list)
                {
                    fFound = true;
                    s += "<p style='margin-top:50px;'><center style='font-size:x-large'>";
                    s += row["AlbumTitle"] + " (" + Helper.GetMonth((int)row["AlbumMonth"]) + " " + row["AlbumYear"] + ")</center>";
                    break;
                }
                if (!fFound)
                {
                    s += "No Photos.";
                }
                foreach (DataRowView row in list)
                {
                    string u = "AlbumView.aspx?ah=" + m_albumHash + "&ph=" + row["FileName"];
                    s += "<img onclick=donavigate('" + u + "') style='margin:20px' src='" + Helper.GetUrl(row, "_s") + "'/>";
                }
            }

            Response.Write(s);
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

        public class Rect64
        {
            private ulong uu;
            private double ww, hh;

            public Rect64(ulong u, double w, double h)
            {
                uu = u;
                ww = w / 0xffff;
                hh = h / 0xffff;
            }

            public double Left { get { return ((double)((uu & 0xffff000000000000) >> 48)) * ww; } }
            public double Top { get { return ((double)((uu & 0x0000ffff00000000) >> 32)) * hh; } }
            public double Right { get { return ((double)((uu & 0x00000000ffff0000) >> 16)) * ww; } }
            public double Bottom { get { return ((double)((uu & 0x000000000000ffff) >> 0)) * hh; } }
        }

        private string DumpFaces(int index, DataRowView row)
        {
            string s = "";
            string rects = Helper.GetPhotoString('R', row);
            string names = Helper.GetPhotoString('P', row);

            if (rects != "")
            {
                var rectsA = rects.Split(',');
                var namesA = names.Split(',');

                if (rectsA.Length == namesA.Length)
                {
                    // Dump names here
                    s += "var faces" + index + " = new Array();\r\n";
                    for (int i = 0; i < rectsA.Length; i++)
                    {
                        ulong urect;
                        bool b = ulong.TryParse(rectsA[i], NumberStyles.HexNumber, CultureInfo.InvariantCulture, out urect);
                        if (!b)
                        {
                            s = "";
                            break;
                        }
                        Rect64 rect = new Rect64(urect, 0xffff, 0xffff);
                        ulong t = (ulong)rect.Top;
                        ulong l = (ulong)rect.Left;
                        ulong w = (ulong)rect.Right - l;
                        ulong h = (ulong)rect.Bottom - t;
                        s += "faces" + index + "[" + i + "] = new Face('" + namesA[i].Trim() + "',true," + t + "," + l + "," + w + "," + h + ");\r\n";
                    }
                }
            }
            else if (names != "")
            {
                var namesA = names.Split(',');
                s += "var faces" + index + " = new Array();\r\n";
                for (int i = 0; i < namesA.Length; i++)
                {
                    s += "faces" + index + "[" + i + "] = new Face('" + namesA[i].Trim() + "',false,0,0,0,0);\r\n";
                }
            }

            return s;
        }

        protected void PutPhotoArray()
        {
            string s = "";

            if (m_albumHash != "")
            {
                AlbumListGenerator.SelectCommand = BuildPhotoQuery(m_subquery, m_albumHash);
                var list = AlbumListGenerator.Select(DataSourceSelectArguments.Empty);

                bool fFound = false;
                foreach (DataRowView row in list)
                {
                    fFound = true;
                    s += "<p style='margin-top:50px;'><center style='font-size:x-large'>";
                    s += row["AlbumTitle"] + " (" + Helper.GetMonth((int)row["AlbumMonth"]) + " " + row["AlbumYear"] + ")";
                    s += "<span style='cursor:pointer; font-size:10pt; font-family:Segoe UI; margin-left:20px' onclick='ToggleShowNames()'>Show Names</span>";
                    s+= "</center>";
                    s += "<p style='margin:40'>";
                    break;
                }

                if (fFound)
                {
                    s += "<script>function BuildPhotoList() {";

                    int i = 0;
                    foreach (DataRowView row in list)
                    {
                        var faces = DumpFaces(i, row);
                        s += faces;
                        if (m_photo == i)
                        {
                            s += "index = " + i + ";";
                        }
                        s += "photos[" + i + "] = new Photo(\"" + Helper.GetUrl(row, "") + "\",\"";
                        if ((string)row["PhotoTitle"] == (string)row["AlbumTitle"])
                        {
                            s += "";
                        }
                        else
                        {
                            s += row["PhotoTitle"];
                        }
                        s += "\",";
                        if (faces == "")
                        {
                            s += "null";
                        }
                        else
                        {
                            s += "faces" + i;
                        }
                        s += ");\r\n";
                        i++;
                    }
                    s += "ShowIndex();}</script>";
                }
            }
            Response.Write(s);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            NameValueCollection coll = Request.QueryString;
            m_subquery = Helper.GetQueryValue(coll, "qry", "");
            m_albumHash = Helper.GetQueryValue(coll, "ah", "");
            string photoid = Helper.GetQueryValue(coll, "ph", "0");
            if (!int.TryParse(photoid, out m_photo))
            {
                m_photo = 0;
            }
        }

        private string m_subquery;
        private string m_albumHash;
        private int    m_photo;
    }
}
