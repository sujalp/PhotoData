using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Web.UI;
using Helpers;

namespace PhotoData
{
    public class GroupInformation
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Query { get; set; }
        public string GroupingField { get; set; }
        public string GroupTitleField { get; set; }
        public string ThumbnailQuery { get; set; }
        public string CountQuery { get; set; }
        public string ClickUrl { get; set; }
        public int    TileView { get; set; }
    };

    // ((InStr([PhotoTitle],"Swarali"))<>"0") 
    public partial class Collection : System.Web.UI.Page
    {
        GroupInformation[] m_Groups = {
            new GroupInformation() {
                Id = "Vancouver2012",
                Title = "Vancouver - August 2012" ,
                Query = "select distinctrow AlbumTitle, AlbumHash from Albums where "+
                        "AlbumHash in (select distinctrow FinalPhotos.Albums.AlbumHash from FinalPhotos "+
                        "where " +
                        "(PlaceName in ('Vancouver') and AlbumYear = 2012) "+
                        "group by FinalPhotos.Albums.AlbumHash) "+
                        "order by AlbumTitle",
                GroupingField = "AlbumHash",
                GroupTitleField = "$$RD0$$",
                ThumbnailQuery = "select * from Albums where AlbumHash = '$$GF$$'",
                CountQuery = "select * from FinalPhotos where FinalPhotos.Albums.AlbumHash = '$$GF$$'",
                ClickUrl = "OneAlbum.aspx?ah=$$GF$$",
                TileView = 0
            },  

            new GroupInformation() {
                Id = "Camping2012",
                Title = "Camping in the Canadian Rockies - August 2012" ,
                Query = "select distinctrow AlbumTitle, AlbumHash from Albums where "+
                        "AlbumHash in (select distinctrow FinalPhotos.Albums.AlbumHash from FinalPhotos "+
                        "where ( ( " +
                        "(InStr([AlbumTitle],\"Camping\")<>\"0\"))      " +
                        "and AlbumYear = 2012)"+
                        "group by FinalPhotos.Albums.AlbumHash) "+
                        "order by AlbumTitle",
                GroupingField = "AlbumHash",
                GroupTitleField = "$$RD0$$",
                ThumbnailQuery = "select * from Albums where AlbumHash = '$$GF$$'",
                CountQuery = "select * from FinalPhotos where FinalPhotos.Albums.AlbumHash = '$$GF$$'",
                ClickUrl = "OneAlbum.aspx?ah=$$GF$$",
                TileView = 0
            },  

            new GroupInformation() {
                Id = "Reunion2012",
                Title = "Parikh Family Reunion - August 2012" ,
                Query = "select distinctrow AlbumTitle, AlbumHash from Albums where "+
                        "AlbumHash in (select distinctrow FinalPhotos.Albums.AlbumHash from FinalPhotos "+
                        "where ( ( " +
                        "(InStr([AlbumTitle],\"Reunion\")<>\"0\"))      " +
                        "and AlbumYear = 2012)"+
                        "group by FinalPhotos.Albums.AlbumHash) "+
                        "order by AlbumTitle",
                GroupingField = "AlbumHash",
                GroupTitleField = "$$RD0$$",
                ThumbnailQuery = "select * from Albums where AlbumHash = '$$GF$$'",
                CountQuery = "select * from FinalPhotos where FinalPhotos.Albums.AlbumHash = '$$GF$$'",
                ClickUrl = "OneAlbum.aspx?ah=$$GF$$",
                TileView = 0
            },  

            new GroupInformation() {
                Id = "DadsCookout2012",
                Title = "Dads Cookout - March 2012" ,
                Query = "select distinctrow AlbumTitle, AlbumHash from Albums where "+
                        "AlbumHash in (select distinctrow FinalPhotos.Albums.AlbumHash from FinalPhotos "+
                        "where ( ( " +
                        "(InStr([AlbumTitle],\"Cookout\")<>\"0\"))      " +
                        "and AlbumYear = 2012)"+
                        "group by FinalPhotos.Albums.AlbumHash) "+
                        "order by AlbumTitle",
                GroupingField = "AlbumHash",
                GroupTitleField = "$$RD0$$",
                ThumbnailQuery = "select * from Albums where AlbumHash = '$$GF$$'",
                CountQuery = "select * from FinalPhotos where FinalPhotos.Albums.AlbumHash = '$$GF$$'",
                ClickUrl = "OneAlbum.aspx?ah=$$GF$$",
                TileView = 0
            },  

 
            new GroupInformation() { 
                Id = "NorthIndia2010",
                Title = "North India - December 2011" ,
                Query = "select distinctrow AlbumTitle, AlbumHash from Albums where "+
                        "AlbumHash in (select distinctrow FinalPhotos.Albums.AlbumHash from FinalPhotos "+
                        "where (PlaceName in ('Fatehpur', 'Sikri', 'Delhi', 'Agra', 'Sikandara', 'New Delhi', 'Gurgaon') and AlbumYear = 2011) "+
                        "group by FinalPhotos.Albums.AlbumHash) "+
                        "order by AlbumTitle",
                GroupingField = "AlbumHash",
                GroupTitleField = "$$RD0$$",
                ThumbnailQuery = "select * from Albums where AlbumHash = '$$GF$$'",
                CountQuery = "select * from FinalPhotos where FinalPhotos.Albums.AlbumHash = '$$GF$$'",
                ClickUrl = "OneAlbum.aspx?ah=$$GF$$",
                TileView = 0
            },
            
            new GroupInformation() {
                Id = "Gujarat2011",
                Title = "Gujarat - November 2011" ,
                Query = "select distinctrow AlbumTitle, AlbumHash from Albums where "+
                        "AlbumHash in (select distinctrow FinalPhotos.Albums.AlbumHash from FinalPhotos "+
                        "where ( ( " +
                        "(InStr([AlbumTitle],\"Abu\")<>\"0\") or " + 
                        "(InStr([AlbumTitle],\"Gujarat\")<>\"0\") or " + 
                        "(InStr([AlbumTitle],\"Jain\")<>\"0\") or " + 
                        "(InStr([AlbumTitle],\"Chayya\")<>\"0\") or " + 
                        "(InStr([AlbumTitle],\"Sankheshwar\")<>\"0\"))      " +
                        "and AlbumYear = 2011)"+
                        "group by FinalPhotos.Albums.AlbumHash) "+
                        "order by AlbumTitle",
                GroupingField = "AlbumHash",
                GroupTitleField = "$$RD0$$",
                ThumbnailQuery = "select * from Albums where AlbumHash = '$$GF$$'",
                CountQuery = "select * from FinalPhotos where FinalPhotos.Albums.AlbumHash = '$$GF$$'",
                ClickUrl = "OneAlbum.aspx?ah=$$GF$$",
                TileView = 0
            },            
            
            new GroupInformation() {
                Id = "Konkan2011",
                Title = "Konkan - November 2011" ,
                Query = "select distinctrow AlbumTitle, AlbumHash from Albums where "+
                        "AlbumHash in (select distinctrow FinalPhotos.Albums.AlbumHash from FinalPhotos "+
                        "where ( ( " +
                        "(InStr([AlbumTitle],\"Konkan\")<>\"0\") or " + 
                        "(InStr([AlbumTitle],\"Koyna\")<>\"0\") or " + 
                        "(InStr([AlbumTitle],\"Guhagar\")<>\"0\") or " + 
                        "(InStr([AlbumTitle],\"Guhagar\")<>\"0\") or " + 
                        "(InStr([AlbumTitle],\"Talkeshwar\")<>\"0\"))      " +
                        "and AlbumYear = 2011)"+
                        "group by FinalPhotos.Albums.AlbumHash) "+
                        "order by AlbumTitle",
                GroupingField = "AlbumHash",
                GroupTitleField = "$$RD0$$",
                ThumbnailQuery = "select * from Albums where AlbumHash = '$$GF$$'",
                CountQuery = "select * from FinalPhotos where FinalPhotos.Albums.AlbumHash = '$$GF$$'",
                ClickUrl = "OneAlbum.aspx?ah=$$GF$$",
                TileView = 0
            },            

            new GroupInformation() {
                Id = "Mahabaleshwar2011",
                Title = "Mahabaleshwar - November 2011" ,
                Query = "select distinctrow AlbumTitle, AlbumHash from Albums where "+
                        "AlbumHash in (select distinctrow FinalPhotos.Albums.AlbumHash from FinalPhotos "+
                        "where ( ( (InStr([AlbumTitle],\"Mahabaleshwar\")<>\"0\") or (InStr([AlbumTitle],\"Karting\")<>\"0\")) and AlbumYear = 2011)"+
                        "group by FinalPhotos.Albums.AlbumHash) "+
                        "order by AlbumTitle",
                GroupingField = "AlbumHash",
                GroupTitleField = "$$RD0$$",
                ThumbnailQuery = "select * from Albums where AlbumHash = '$$GF$$'",
                CountQuery = "select * from FinalPhotos where FinalPhotos.Albums.AlbumHash = '$$GF$$'",
                ClickUrl = "OneAlbum.aspx?ah=$$GF$$",
                TileView = 0
            },            
            
            new GroupInformation() { 
                Id = "Alaska2010",
                Title = "Alaska Vacation - July/August 2011" ,
                Query = "select distinctrow AlbumTitle, AlbumHash from Albums where "+
                        "AlbumHash in (select distinctrow FinalPhotos.Albums.AlbumHash from FinalPhotos "+
                        "where (PlaceName in ('Anchorage', 'Seward', 'Denali National Park', 'Spencer Glacier', 'Anchrage', 'Matanuska Glacier') and Year_ = 2011) "+
                        "group by FinalPhotos.Albums.AlbumHash) "+
                        "order by AlbumTitle",
                GroupingField = "AlbumHash",
                GroupTitleField = "$$RD0$$",
                ThumbnailQuery = "select * from Albums where AlbumHash = '$$GF$$'",
                CountQuery = "select * from FinalPhotos where FinalPhotos.Albums.AlbumHash = '$$GF$$'",
                ClickUrl = "OneAlbum.aspx?ah=$$GF$$",
                TileView = 0
            },

            new GroupInformation() {
                Id = "SwaruBirthdays",
                Title = "Swarali's Birthdays Over The Years",
                Query = "SELECT DISTINCTROW AlbumTitle, AlbumHash, AlbumYear "+
                        "FROM Albums " +
                        "WHERE AlbumHash in (select distinctrow FinalPhotos.Albums.AlbumHash from FinalPhotos "+
                        "where (    (InStr([AlbumTitle],\"Birth\")<>\"0\") and (InStr([AlbumTitle],\"Swarali\")<>\"0\") and (Month_ = 10 or Month_ = 11 or Month_ = 12)) "+
                        "group by FinalPhotos.Albums.AlbumHash) "+
                        "ORDER BY AlbumYear desc",
                GroupingField = "AlbumHash",
                GroupTitleField = "$$RD2$$ - $$RD0$$",
                ThumbnailQuery = "select * from Albums where AlbumHash = '$$GF$$'",
                CountQuery = "select * from FinalPhotos where FinalPhotos.Albums.AlbumHash = '$$GF$$'",
                ClickUrl = "OneAlbum.aspx?ah=$$GF$$",
                TileView = 0
            },

            new GroupInformation() {
                Id = "KinnerBirthdays",
                Title = "Kinner's Birthdays Over The Years",
                Query = "SELECT DISTINCTROW AlbumTitle, AlbumHash, AlbumYear "+
                        "FROM Albums " +
                        "WHERE AlbumHash in (select distinctrow FinalPhotos.Albums.AlbumHash from FinalPhotos "+
                        "where (    (InStr([AlbumTitle],\"Birth\")<>\"0\") and (InStr([AlbumTitle],\"Kinner\")<>\"0\") and (Month_ = 7 or Month_ = 8)) "+
                        "group by FinalPhotos.Albums.AlbumHash) "+
                        "ORDER BY AlbumYear desc",
                GroupingField = "AlbumHash",
                GroupTitleField = "$$RD2$$ - $$RD0$$",
                ThumbnailQuery = "select * from Albums where AlbumHash = '$$GF$$'",
                CountQuery = "select * from FinalPhotos where FinalPhotos.Albums.AlbumHash = '$$GF$$'",
                ClickUrl = "OneAlbum.aspx?ah=$$GF$$",
                TileView = 0
            },

            new GroupInformation() { 
                Id = "Weddings",
                Title = "Family Engagements and Weddings Over The Years" ,
                Query = "select distinctrow AlbumTitle, AlbumHash, AlbumYear from Albums  "+
                        "where (("+
                            "(InStr([AlbumTitle], \"Married\") <> \"0\") or "+
                            "(InStr([AlbumTitle], \"Weds \") <> \"0\") or "+
                            "(InStr([AlbumTitle], \"Wedding\") <> \"0\") or "+
                            "(InStr([AlbumTitle], \"Engagement\") <> \"0\") or "+
                            "(InStr([AlbumTitle], \"Engaged\") <> \"0\") "+

                            ") and ("+
                            "(InStr([AlbumTitle], \"Ming\") = \"0\") and "+
                            "(InStr([AlbumTitle], \"Hanuman\") = \"0\") and "+
                            "(InStr([AlbumTitle], \"Honeymoon\") = \"0\") and "+
                            "(InStr([AlbumTitle], \"Paras\") = \"0\") and "+
                            "(InStr([AlbumTitle], \"Anniversary\") = \"0\") "+
                        ")) order by AlbumYear desc",
                GroupingField = "AlbumHash",
                GroupTitleField = "$$RD2$$ - $$RD0$$",
                ThumbnailQuery = "select * from Albums where AlbumHash = '$$GF$$'",
                CountQuery = "select * from FinalPhotos where FinalPhotos.Albums.AlbumHash = '$$GF$$'",
                ClickUrl = "OneAlbum.aspx?ah=$$GF$$",
                TileView = 0
            },

            new GroupInformation() {
                Id="Swarali",
                Title = "Swarali's Photos Over The Years",
                Query = "SELECT DISTINCTROW AlbumTitle, AlbumHash, AlbumYear "+
                        "FROM Albums " +
                        "WHERE AlbumHash in (select distinctrow FinalPhotos.Albums.AlbumHash from FinalPhotos "+
                        "where (    (InStr([FinalPhotos.People],\"Swarali\")<>\"0\") ) "+
                        "group by FinalPhotos.Albums.AlbumHash) "+
                        "ORDER BY AlbumYear desc",
                GroupingField = "AlbumHash",
                GroupTitleField = "$$RD2$$ - $$RD0$$",
                ThumbnailQuery = "select * from Albums where AlbumHash = '$$GF$$'",
                CountQuery = "select * from FinalPhotos where FinalPhotos.Albums.AlbumHash = '$$GF$$'",
                ClickUrl = "OneAlbum.aspx?ah=$$GF$$",
                TileView = 0
            },

            new GroupInformation() {
                Id="Years",
                Title = "Parikh Photos Over the Years",
                Query = "select distinctrow AlbumYear from Albums group by AlbumYear order by AlbumYear desc",
                GroupingField = "AlbumYear",
                GroupTitleField = "$$RD0$$",
                ThumbnailQuery = "select * from Albums where AlbumYear = $$GF$$",
                CountQuery = "select * from FinalPhotos where AlbumYear = $$GF$$",
                ClickUrl = "Months.aspx?ay=$$GF$$",
                TileView = 1
            }
        };

        private string GetTitle()
        {
            return m_Group.Title;
        }

        private string GetQuery()
        {
            return m_Group.Query;
        }

        private string GetGroupingField()
        {
            return m_Group.GroupingField;
        }

        private string GetGroupTitleField()
        {
            return m_Group.GroupTitleField;
        }

        private string GetGroupTitle(DataRowView r)
        {
            string s = GetGroupTitleField();
            for (int i = 0; ; i++)
            {
                try
                {
                    s = s.Replace("$$RD" + i + "$$", r[i].ToString());
                }
                catch
                {
                    break;
                }
            }
            return s;
        }

        private string GetThumbnailQuery(string qualifier)
        {
            string s = m_Group.ThumbnailQuery;
            return s.Replace("$$GF$$", qualifier);
        }

        private string GetCountQuery(string qualifier)
        {
            string s = m_Group.CountQuery;
            return s.Replace("$$GF$$", qualifier);
        }

        private string GetClickUrl(string qualifier)
        {
            string s = m_Group.ClickUrl;
            return s.Replace("$$GF$$", qualifier);
        }

        private int GetTileView()
        {
            return m_Group.TileView;
        }

        protected void PutCollectionList()
        {
            string s = "No Photos!";

            if (m_Group != null)
            {
                AlbumListGenerator1.SelectCommand = GetQuery();
                var list1 = AlbumListGenerator1.Select(DataSourceSelectArguments.Empty);
                bool fFirst = true;
                foreach (DataRowView row1 in list1)
                {
                    // Calculate the URI of the thumbnail first
                    string ThumbnailUri = null;

                    AlbumListGenerator2.SelectCommand = GetThumbnailQuery(row1[GetGroupingField()].ToString());
                    var list2 = AlbumListGenerator2.Select(DataSourceSelectArguments.Empty);
                    int count = 0;
                    foreach (DataRowView row2 in list2)
                    {
                        count++;
                    }
                    Random r = new Random();
                    int idx = r.Next(count);

                    count = 0;
                    list2 = AlbumListGenerator2.Select(DataSourceSelectArguments.Empty);
                    foreach (DataRowView row2 in list2)
                    {
                        if (count == idx)
                        {
                            ThumbnailUri = (string)row2["AlbumPhoto"];
                            break;
                        }
                        count++;
                    }

                    // Now the Text below the thumbnail
                    //string TBTT = row1[GetGroupTitleField()].ToString();
                    string TBTT = GetGroupTitle(row1);
                    string ClickUrl = GetClickUrl(row1[GetGroupingField()].ToString());

                    // Finally the count below or next to the text
                    AlbumListGenerator2.SelectCommand = GetCountQuery(row1[GetGroupingField()].ToString());
                    list2 = AlbumListGenerator2.Select(DataSourceSelectArguments.Empty);
                    count = 0;
                    foreach (DataRowView row2 in list2)
                    {
                        count++;
                    }

                    string stub = ProduceThumbnail(fFirst, ClickUrl, ThumbnailUri, TBTT, count);
                    if (fFirst)
                    {
                        s = "<p style='margin-top:50px;'><center style='font-size:x-large'>";
                        s += GetTitle() + "</center>";
                        s += "<p style='margin:40'>";
                        fFirst = false;
                    }
                    s += stub;
                }
            }
            else
            {
                // Got these colors from http://colorschemedesigner.com/
                s = "<span style='font-size:20pt'>Available Collections</span><p style='margin:100'></P>\r\n";
                s += "<style>\r\n";
                s += "span#color1 {vertical-align:bottom; text-align:right; display:inline-block; margin:15px; width:200px; height:200px; font-size:20pt; background:#5c0dac; foreground:white; cursor:hand}\r\n";
                s += "span#color2 {vertical-align:bottom; text-align:right; display:inline-block; margin:15px; width:200px; height:200px; font-size:20pt; background:#8506a9; foreground:white; cursor:hand}\r\n";
                s += "span#color3 {vertical-align:bottom; text-align:right; display:inline-block; margin:15px; width:200px; height:200px; font-size:20pt; background:#3e13af; foreground:white; cursor:hand}\r\n";
                s += "span#color4 {vertical-align:bottom; text-align:right; display:inline-block; margin:15px; width:200px; height:200px; font-size:20pt; background:#3a0470; foreground:white; cursor:hand}\r\n";
                s += "</style>\r\n";
                Random r = new Random();
                foreach (var group in m_Groups)
                {
                    s += "<span id=" + GetRandomColor(r) + " onclick=\"donavigate('collection.aspx?collection=" + group.Id + "')\"><table style='height:200px; widht:200px'><tr><td valign=bottom>" + group.Title + "</td></tr></table></span>";
                }
            }
            Response.Write(s);
        }

        private string GetRandomColor(Random r)
        {
            int i = r.Next(4);
            switch (i)
            {
                case 0: return "color1";
                case 1: return "color2"; 
                case 2: return "color3";
                case 3: return "color4";
            }
            return "colorgreen";
        }

        private string ProduceThumbnail(bool fFirst, string ClickUrl, string ThumbnailUri, string TBTT, int count)
        {
            switch (GetTileView())
            {
                case 0:
                    return TileView0(fFirst, ClickUrl, ThumbnailUri, TBTT, count);
                case 1:
                    return TileView1(fFirst, ClickUrl, ThumbnailUri, TBTT, count);
                default:
                    return TileView0(fFirst, ClickUrl, ThumbnailUri, TBTT, count);
            }
        }

        private string TileView0(bool fFirst, string ClickUrl, string ThumbnailUri, string TBTT, int count)
        {
            string s = "";
            string u = "'" + ClickUrl + "'";
            string img = ThumbnailUri;

            s += "<table style='cursor:pointer; display:inline; vertical-align:top; margin:10px' onclick=donavigate(" + u + ")><tr><td align=right>";
            s += "<span style='background:green; display:inline'><img style='margin:0px' src='" + img + "'></span> ";
            s += "</td></tr><tr><td style='text-align:right; width:150px'>";
            s += TBTT;
            s += "<br><span style='font-size:x-small'>(" + count + ")</span>";
            s += "</td></tr></table>";
            return s;
        }

        private string TileView1(bool fFirst, string ClickUrl, string ThumbnailUri, string TBTT, int count)
        {
            string s = "";
            string u = "'" + ClickUrl + "'";
            string img = ThumbnailUri;
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
            s += TBTT;
            s += "<br><span style='font-size:" + fontsz1 + "'>(" + count + ")</span>";
            s += "</td></tr></table>";
            return s;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            NameValueCollection coll = Request.QueryString;
            string collectionId = Helper.GetQueryValue(coll, "collection", "");
            m_Level = Helper.GetQueryValue(coll, "level", 0);

            m_Group = null;
            if (collectionId != "")
            {
                foreach (var group in m_Groups)
                {
                    if (group.Id == collectionId)
                    {
                        m_Group = group;
                        break;
                    }
                }
            }
        }

        int m_Level;
        GroupInformation m_Group;
    }

#if NOTYET
    public partial class XYears : System.Web.UI.Page
    {
        public string GetPhotoString(char what, object o)
        {
            return Helper.GetPhotoString(what, o);
        }

        private string BuildYearPhotoCountQuery()
        {
            string myQuery = @"SELECT DISTINCTROW FinalPhotos.AlbumYear, Count(*) AS Total FROM FinalPhotos ";
            if (m_subquery != "")
                myQuery += "WHERE (" + m_subquery + ") ";
            myQuery += @"GROUP BY FinalPhotos.Albums.AlbumYear ";
            myQuery += @"ORDER BY FinalPhotos.Albums.AlbumYear DESC";
            return myQuery;
        }

        private string BuildYearQuery(string subquery)
        {
            string myYearQuery = @"SELECT * FROM Albums ";
            if (subquery != "")
                myYearQuery += "WHERE (" + subquery + ") ";
            myYearQuery += @"ORDER BY AlbumYear DESC, AlbumMonth DESC";
            return myYearQuery;
        }

        protected void PutYearList()
        {
            Hashtable ht = new Hashtable();
            string s = "";

            AlbumListGenerator1.SelectCommand = BuildYearPhotoCountQuery();
            var list = AlbumListGenerator1.Select(DataSourceSelectArguments.Empty);
            foreach (DataRowView row in list)
            {
                ht.Add((int)row["AlbumYear"], (int)row["Total"]);
            }

            AlbumListGenerator1.SelectCommand = BuildYearQuery(m_subquery);
            list = AlbumListGenerator1.Select(DataSourceSelectArguments.Empty);

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

        protected void Page_Load(object sender, EventArgs e)
        {
            NameValueCollection coll = Request.QueryString;
            m_subquery = Helper.GetQueryValue(coll, "qry", "");
        }

        private string m_subquery = "";
    }
#endif

}
