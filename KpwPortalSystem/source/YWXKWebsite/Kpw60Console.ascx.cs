using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Nairc.KpwFramework.TelescopeControler;
using System.Drawing;
using log4net;
using System.IO;
using System.Configuration;
using Nairc.KpwFramework.DataModel;

namespace YWXKPortal.Web
{
    public partial class Kpw60Console : System.Web.UI.UserControl
    {
        private static ILog logger = log4net.LogManager.GetLogger(typeof(Kpw60Console));

        protected string Url
        {
            get
            {
                return ConfigurationManager.AppSettings["ImageRelativePath"].ToString();
            }
        }

        protected string WebSiteUrl
        {
            get
            {
                string AppPath = "";
                HttpContext HttpCurrent = HttpContext.Current;
                HttpRequest Req;
                if (HttpCurrent != null)
                {
                    Req = HttpCurrent.Request;
                    string UrlAuthority = Req.Url.GetLeftPart(UriPartial.Authority);
                    if (Req.ApplicationPath == null || Req.ApplicationPath == "/")
                        //直接安装在   Web   站点   
                        AppPath = UrlAuthority;
                    else
                        //安装在虚拟子目录下   
                        AppPath = UrlAuthority + Req.ApplicationPath;
                }
                return AppPath;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string user = (string)Session["User"];

            if (user == "admin")
            {
                this.controlDiv.Visible = true;
            }
            else
            {
                this.controlDiv.Visible = false;
            }
           
            this.Refresh();
        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            this.Refresh();
        }

        protected void lnkImage_Click(object sender, EventArgs e)
        {
            LinkButton lnk = sender as LinkButton;
            string fileName = lnk.CommandArgument;// "N001_big.fit";//客户端保存的文件名
            string filePath = Server.MapPath(fileName);//路径
            //以字符流的形式下载文件
            FileStream fs = new FileStream(filePath, FileMode.Open);
            byte[] bytes = new byte[(int)fs.Length];
            fs.Read(bytes, 0, bytes.Length);
            fs.Close();
            Response.ContentType = "application/octet-stream";
            //通知浏览器下载文件而不是打开
            Response.AddHeader("Content-Disposition", "attachment;  filename=" + HttpUtility.UrlEncode(fileName, System.Text.Encoding.UTF8));
            Response.BinaryWrite(bytes);
            Response.Flush();
            Response.End();
        }

        private void Refresh()
        {
            try
            {
                if (KpwRuntime.Instance.CommandSender.IsConnected == false)
                {
                    string defaultString = "---";

                    this.txtConnect.Text = "未连接";
                    this.txtConnect.ForeColor = Color.Red;

                    this.txtRa.Text = defaultString;
                    this.txtDec.Text = defaultString;
                    this.txtRaOverflow.Text = defaultString;
                    this.txtDecOverflow.Text = defaultString;
                    this.txtLevelOverflow.Text = defaultString;
                    this.txtMirrorStatus.Text = defaultString;
                    this.txtSearchStar.Text = defaultString;
                    this.txtDome.Text = defaultString;

                    return;
                }

                this.txtConnect.Text = "已连接";
                this.txtConnect.ForeColor = Color.Black;

                this.txtRa.Text = KpwRuntime.Instance.TelescopeStatus.RaPos.ToString();
                this.txtDec.Text = KpwRuntime.Instance.TelescopeStatus.DecPos.ToString();

                if (KpwRuntime.Instance.TelescopeStatus.RaOverflow == RaOverFlow.Positive)
                {
                    this.txtRaOverflow.Text = "正限位";
                }
                else if (KpwRuntime.Instance.TelescopeStatus.RaOverflow == RaOverFlow.Negative)
                {
                    this.txtRaOverflow.Text = "反限位";
                }
                else
                {
                    this.txtRaOverflow.Text = "未限位";
                }

                if (KpwRuntime.Instance.TelescopeStatus.DecOverflow == DecOverFlow.Positive)
                {
                    this.txtDecOverflow.Text = "正限位";
                }
                else if (KpwRuntime.Instance.TelescopeStatus.DecOverflow == DecOverFlow.Negative)
                {
                    this.txtDecOverflow.Text = "反限位";
                }
                else
                {
                    this.txtDecOverflow.Text = "未限位";
                }

                if (KpwRuntime.Instance.TelescopeStatus.LevelOverflow == true)
                {
                    this.txtLevelOverflow.Text = "限位";
                }
                else
                {
                    this.txtLevelOverflow.Text = "未限位";
                }

                if (KpwRuntime.Instance.TelescopeStatus.MirrorStatus == MirrorCoverStatus.O)
                {
                    this.txtMirrorStatus.Text = "已开";
                }
                else if (KpwRuntime.Instance.TelescopeStatus.MirrorStatus == MirrorCoverStatus.C)
                {
                    this.txtMirrorStatus.Text = "已关";
                }
                else
                {
                    this.txtMirrorStatus.Text = "运动中";
                }

                if (KpwRuntime.Instance.TelescopeStatus.SearchStatus == SearchStarStatus.P)
                {
                    this.txtSearchStar.Text = "正在找";
                }
                else if (KpwRuntime.Instance.TelescopeStatus.SearchStatus == SearchStarStatus.S)
                {
                    this.txtSearchStar.Text = "成功";
                }
                else
                {
                    this.txtSearchStar.Text = "失败";
                }

                if (KpwRuntime.Instance.TelescopeStatus.DomeStatus == DomeStatus.P)
                {
                    this.txtDome.Text = "正在随动";
                }
                else if (KpwRuntime.Instance.TelescopeStatus.DomeStatus == DomeStatus.S)
                {
                    this.txtDome.Text = "以对准";
                }
                else
                {
                    this.txtDome.Text = "失败";
                }
            }
            catch (Exception ex)
            {
                logger.Error("Refresh failed.", ex);
            }
        }
    }
}