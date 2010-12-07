using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Nairc.KpwFramework.DataModel;
using System.Configuration;
using Nairc.KpwDataAccess;

namespace DesktopModules.Web.KPWModules
{
    public partial class ImageGalleryEx : System.Web.UI.Page
    {
       protected void Page_Load(object sender, EventArgs e)
        {
            //galleryRepeater.DataSource = new List<ImageInfo>()
            //{
            //    new ImageInfo(){
            //        StarName = "太阳",
            //        CreatedBy = "Tony Wu",
            //        BigImage = KpwConfiguration.BigImageRelativePath + "3261/2538183196_8baf9a8015_b.jpg",
            //        Image = KpwConfiguration.ImageRelativePath + "2538183196_8baf9a8015.jpg",
            //        CreatedDate = DateTime.Now.Date,
            //        ThumbImage = KpwConfiguration.ThumbImageRelativePath + "2538183196_8baf9a8015_s.jpg"
            //    },

            //     new ImageInfo(){
            //        StarName = "土星",
            //        CreatedBy = "Tony Wu",
            //        BigImage = KpwConfiguration.BigImageRelativePath + "2538171134_2f77bc00d9_b.jpg",
            //        Image = KpwConfiguration.ImageRelativePath + "2538171134_2f77bc00d9.jpg",
            //        CreatedDate = DateTime.Now.Date,
            //        ThumbImage = KpwConfiguration.ThumbImageRelativePath + "2538171134_2f77bc00d9_s.jpg"
            //    },

            //     new ImageInfo(){
            //        StarName = "金星",
            //        CreatedBy = "Tony Wu",
            //        BigImage = KpwConfiguration.BigImageRelativePath + "2538168854_f75e408156_b.jpg",
            //        Image = KpwConfiguration.ImageRelativePath +"2538168854_f75e408156.jpg",
            //        CreatedDate = DateTime.Now.Date,
            //        ThumbImage = KpwConfiguration.ThumbImageRelativePath + "2538168854_f75e408156_s.jpg"
            //    }
            //};

            ImageInfoDB db = new ImageInfoDB();

            List<ImageInfo> list = db.GetTop20Images();

            if (list.Count == 0)
            {
                list.Add(new ImageInfo()
                {
                    StarName = "默认图片",
                    CreatedBy = "KPW 60 望远镜",
                    BigImage = Request.ApplicationPath + "astronomy/images/930424599_e75865c0d6_b.jpg",
                    Image = Request.ApplicationPath + "astronomy/images/930424599_e75865c0d6.jpg",
                    CreatedDate = DateTime.Now.Date,
                    ThumbImage = Request.ApplicationPath + "astronomy/images_small/930424599_e75865c0d6_s.jpg"
                });
            }

            galleryRepeater.DataSource = list;
            galleryRepeater.DataBind();
            
        }

        protected void btnSearch_Click(Object sender, EventArgs e)
        {
            ImageInfoDB db = new ImageInfoDB();

            List<ImageInfo> list = null;

            if (this.datepicker.Value.Trim() == "")
            {
                list = db.GetImageInfoByDate(DateTime.Now.Date);
            }
            else
            {
                list = db.GetImageInfoByDate(DateTime.Parse(this.datepicker.Value));
            }

            if (list.Count == 0)
            {
                list.Add(new ImageInfo()
                {
                    StarName = "默认图片",
                    CreatedBy = "KPW 60 望远镜",
                    BigImage = Request.ApplicationPath + "astronomy/images/930424599_e75865c0d6_b.jpg",
                    Image = Request.ApplicationPath + "astronomy/images/930424599_e75865c0d6.jpg",
                    CreatedDate = DateTime.Now.Date,
                    ThumbImage = Request.ApplicationPath + "astronomy/images_small/930424599_e75865c0d6_s.jpg"
                });
            }

            galleryRepeater.DataSource = list;
            galleryRepeater.DataBind();
        }
    }
}
