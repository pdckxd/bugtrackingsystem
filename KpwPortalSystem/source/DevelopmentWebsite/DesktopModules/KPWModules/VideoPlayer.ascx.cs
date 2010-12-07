using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DesktopModules.Web
{

    public partial class VideoPlayer : System.Web.UI.UserControl
    {
        #region Properties to customize the Video/Audio Control
        /// <summary>
        /// true:FullScreen, false:CustomSize
        /// </summary>
        public Boolean isFullSize
        {
            set
            {
                ViewState["isFullSize"] = value;
            }
            get
            {
                if (ViewState["isFullSize"] != null)
                    return Convert.ToBoolean(ViewState["isFullSize"]);
                else
                    return true;
            }
        }
        /// <summary>
        /// Full url-path of Video/Audio
        /// </summary>
        public String sourceUrl
        {
            set
            {
                ViewState["sourceUrl"] = value;
            }
            get
            {
                if (ViewState["sourceUrl"] != null)
                    return Convert.ToString(ViewState["sourceUrl"]);
                else
                    return "http://www.video.com/myVideo.mpeg"; //Default video
            }
        }
        /// <summary>
        /// width of player
        /// </summary>
        public String width
        {
            set
            {
                ViewState["width"] = value;
            }
            get
            {
                if (ViewState["width"] != null)
                    return Convert.ToString(ViewState["width"]);
                else
                    return "640";
            }
        }
        /// <summary>
        /// Height of player
        /// </summary>
        public String height
        {
            set
            {
                ViewState["height"] = value;
            }
            get
            {
                if (ViewState["height"] != null)
                    return Convert.ToString(ViewState["height"]);
                else
                    return "480";
            }
        }
        /// <summary>
        /// Custom message when player initializes
        /// </summary>
        public String standByMessage
        {
            set
            {
                ViewState["standByMessage"] = value;
            }
            get
            {
                if (ViewState["standByMessage"] != null)
                    return Convert.ToString(ViewState["standByMessage"]);
                else
                    return "Please wait while the player inializes...";
            }
        }
        /// <summary>
        /// States whether media automatic starts or not
        /// </summary>
        public Boolean autoStart
        {
            set
            {
                ViewState["autoStart"] = value;
            }
            get
            {
                if (ViewState["autoStart"] != null)
                    return Convert.ToBoolean(ViewState["autoStart"]);
                else
                    return true;
            }
        }
        /// <summary>
        /// -100 is fully left, 100 is fully right.
        /// </summary>
        public String balance
        {
            set
            {
                ViewState["balance"] = value;
            }
            get
            {
                try
                {
                    if (ViewState["balance"] != null)
                        return Convert.ToString(ViewState["balance"]);
                    else
                        return "0";
                }
                catch
                {
                    return "0";
                }
            }
        }
        /// <summary>
        /// Position in seconds when starting.
        /// </summary>
        public Int32 currentPosition
        {
            set
            {
                ViewState["currentPosition"] = value;
            }
            get
            {
                if (ViewState["currentPosition"] != null)
                    return Convert.ToInt32(ViewState["currentPosition"]);
                else
                    return 0;
            }
        }
        /// <summary>
        /// Show play/stop/pause controls
        /// </summary>
        public Boolean showcontrols
        {
            set
            {
                ViewState["showcontrols"] = value;
            }
            get
            {
                if (ViewState["showcontrols"] != null)
                    return Convert.ToBoolean(ViewState["showcontrols"]);
                else
                    return true;
            }
        }
        /// <summary>
        /// Allow right-click
        /// </summary>
        public Boolean contextMenu
        {
            set
            {
                ViewState["contextMenu"] = value;
            }
            get
            {
                if (ViewState["contextMenu"] != null)
                    return Convert.ToBoolean(ViewState["contextMenu"]);
                else
                    return false;
            }
        }
        /// <summary>
        /// Toggle sound on/off
        /// </summary>
        public Boolean mute
        {
            set
            {
                ViewState["mute"] = value;
            }
            get
            {
                if (ViewState["mute"] != null)
                    return Convert.ToBoolean(ViewState["mute"]);
                else
                    return false;
            }
        }
        /// <summary>
        /// Number of times the content will play
        /// </summary>
        public Int32 playCount
        {
            set
            {
                ViewState["playCount"] = value;
            }
            get
            {
                if (ViewState["playCount"] != null)
                    return Convert.ToInt32(ViewState["playCount"]);
                else
                    return 1;
            }
        }
        /// <summary>
        /// 0.5=Slow, 1.0=Normal, 2.0=Fast
        /// </summary>
        public Double rate
        {
            set
            {
                ViewState["rate"] = value;
            }
            get
            {
                if (ViewState["rate"] != null)
                    return Convert.ToDouble(ViewState["rate"]);
                else
                    return 1.0;
            }
        }
        /// <summary>
        /// full, mini, custom, none, invisible
        /// </summary>
        public String uiMode
        {
            set
            {
                ViewState["uiMode"] = value;
            }
            get
            {
                if (ViewState["uiMode"] != null)
                    return Convert.ToString(ViewState["uiMode"]);
                else
                    return "Full";
            }
        }
        /// <summary>
        /// Show or hide the name of the file/url
        /// </summary>
        public Boolean showDisplay
        {
            set
            {
                ViewState["showDisplay"] = value;
            }
            get
            {
                if (ViewState["showDisplay"] != null)
                    return Convert.ToBoolean(ViewState["showDisplay"]);
                else
                    return false;
            }
        }
        /// <summary>
        /// 0=lowest, 50= normal, 100=highest
        /// </summary>
        public Int32 volume
        {
            set
            {
                ViewState["volume"] = value;
            }
            get
            {
                if (ViewState["volume"] != null)
                    return Convert.ToInt32(ViewState["volume"]);
                else
                    return 50;
            }
        }
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                ltVideo.Text = this.MyVideoPlayer(this.sourceUrl, this.isFullSize);
            }
            catch (Exception ex)
            {
                lblError.Text = ex.ToString();
                phError.Visible = true;
            }
        }
        #region VideoPlayer
        /// <summary>
        /// Return the whPlayer to Play Video/ Audio Content
        /// </summary>
        /// <param name="sourceUrl">Source of content</param>
        /// <param name="isFullSize">Size of Player</param>
        /// <returns></returns>
        private string MyVideoPlayer(string strsourceUrl, bool boolFullSize)
        {
            string whPlayer = "";
            strsourceUrl = strsourceUrl + "";
            strsourceUrl = strsourceUrl.Trim();
            if (strsourceUrl.Length > 0)
            {
                //play content
            }
            else
            {
                throw new System.ArgumentNullException("strsourceUrl");
            }

            if (boolFullSize)
            {
                //this.width = String.Empty;
                //this.height = String.Empty;
                this.width = "530";
                this.height = "400";
            }
            else
            {
                //continue with supplied width/height
            }
            whPlayer = whPlayer + "<object classid='CLSID:22D6F312-B0F6-11D0-94AB-0080C74C7E95' id='player' width='" + this.width + "' height='" + this.height + "' standby= '" + this.standByMessage + "'>";
            whPlayer = whPlayer + "<param name='url' value='" + this.sourceUrl + "' />";
            whPlayer = whPlayer + "<param name='src' value='" + this.sourceUrl + "' />";
            whPlayer = whPlayer + "<param name='AutoStart' value='" + this.autoStart + "' />";
            whPlayer = whPlayer + "<param name='Balance' value='" + this.balance + "' />";
            whPlayer = whPlayer + "<param name='CurrentPosition' value='" + this.currentPosition + "' />";
            whPlayer = whPlayer + "<param name='showcontrols' value='" + this.showcontrols + "' />";
            whPlayer = whPlayer + "<param name='enablecontextmenu' value='" + this.contextMenu + "' />";
            whPlayer = whPlayer + "<param name='fullscreen' value='" + this.isFullSize + "' />";
            whPlayer = whPlayer + "<param name='mute' value='" + this.mute + "' />";
            whPlayer = whPlayer + "<param name='PlayCount' value='" + this.playCount + "' />";
            whPlayer = whPlayer + "<param name='rate' value='" + this.rate + "' />";
            whPlayer = whPlayer + "<param name='uimode' value='" + this.uiMode + "' />";
            whPlayer = whPlayer + "<param name='showdisplay' value='" + this.showDisplay + "' />";
            whPlayer = whPlayer + "<param name='volume' value='" + this.volume + "' />";
            whPlayer = whPlayer + "</object>";
            return whPlayer;
        }
        #endregion
    }
}