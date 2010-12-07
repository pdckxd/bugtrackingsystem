using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Nairc.KPWPortal;
using Nairc.KpwFramework.TelescopeControler;
using Nairc.KpwFramework.DataModel;

namespace DesktopModules.Web
{
    public partial class KPWSample : PortalModuleControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.videoPlayer.sourceUrl = "http://159.226.75.28:8040";

            videoPlayer.autoStart = true;

            //videoPlayer.height = "300";

            //videoPlayer.width = "300";

        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            this.txtRa.Text =  KpwRuntime.Instance.TelescopeStatus.RaPos.ToString();
            this.txtDec.Text = KpwRuntime.Instance.TelescopeStatus.DecPos.ToString();

            this.txtDatetime.Text = DateTime.Now.ToLongDateString() + DateTime.Now.ToLongTimeString();

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
    }
}