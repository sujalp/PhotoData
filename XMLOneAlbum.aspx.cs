using System;
using System.Collections.Specialized;
using System.Data;
using System.Security;
using Helpers;
using PhotoData;

namespace NewPV.Web
{
    public partial class OneAlbum : System.Web.UI.Page
    {
        protected void PutPhotoList()
        {
            m_coa.PutXMLPhotoList();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            NameValueCollection coll = Request.QueryString;
            string subquery = Helper.GetQueryValue(coll, "qry", "");
            string albumHash = Helper.GetQueryValue(coll, "ah", "7db1052c6e2f1d74");

            m_coa = new CommonOneAlbum(subquery, albumHash, AlbumListGenerator, Response);
        }

        CommonOneAlbum m_coa;
    }
}
