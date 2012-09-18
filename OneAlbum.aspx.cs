using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Helpers;
using System.Collections.Specialized;
using System.Data;

namespace PhotoData
{
    public partial class OneAlbum : System.Web.UI.Page
    {
        protected void PutPhotoList()
        {
            m_coa.PutPhotoList();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            NameValueCollection coll = Request.QueryString;
            string subquery = Helper.GetQueryValue(coll, "qry", "");
            string albumHash = Helper.GetQueryValue(coll, "ah", "");

            m_coa = new CommonOneAlbum(subquery, albumHash, AlbumListGenerator, Response);
        }

        CommonOneAlbum m_coa;
    }
}
