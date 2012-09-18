using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace PhotoData
{
    public partial class Lucky : System.Web.UI.Page
    {
        private string BuildAlbumQuery()
        {
            string mQuery = @"SELECT AlbumHash FROM Albums";
            return mQuery;
        }

        protected void PutPhotoList()
        {
            m_coa.PutPhotoList();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            AlbumListGenerator.SelectCommand = BuildAlbumQuery();
            var list = AlbumListGenerator.Select(DataSourceSelectArguments.Empty);

            int count = 0;
            foreach (DataRowView row in list)
            {
                count++;
            }

            Random r = new Random();
            int rnd = r.Next(count);
            string ah = "";

            count = 0;
            foreach (DataRowView row in list)
            {
                if (count == 0)
                {
                    ah = (string)row["AlbumHash"];
                }

                if (count == rnd)
                {
                    ah = (string)row["AlbumHash"];
                    break;
                }

                count++;
            }
            m_coa = new CommonOneAlbum("", ah, AlbumListGenerator, Response);
        }

        private CommonOneAlbum m_coa;
    }
}
