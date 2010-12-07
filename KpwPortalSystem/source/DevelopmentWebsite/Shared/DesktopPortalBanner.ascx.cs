using System;
using System.Collections;
using System.Web;
using System.Web.UI;
using Nairc.KPWPortal;
using System.Web.UI.WebControls;

namespace WebApplication
{
    //***************************************************************************************
    //
    // The DesktopPortalBanner User Control is responsible for displaying the standard Portal
    // banner at the top of each .aspx page.
    //
    // The DesktopPortalBanner uses the Portal Configuration System to obtain a list of the
    // portal's sitename and tab settings. It then render's this content into the page.
    //
    //***************************************************************************************

    public partial class DesktopPortalBanner : UserControl
    {
        public int tabIndex;
        public bool ShowTabs = true;
        protected String LogoffLink = "";

        private int selectedIndex = 0;

        protected Hashtable hashTable = new Hashtable();
        protected void Page_Load(Object sender, EventArgs e)
        {
            // Obtain PortalSettings from Current Context
            PortalSettings portalSettings = (PortalSettings)HttpContext.Current.Items["PortalSettings"];

            // Dynamically Populate the Portal Site Name
            siteName.Text = portalSettings.PortalName;

            // If user logged in, customize welcome message
            if (Request.IsAuthenticated == true)
            {

                WelcomeMessage.Text = "»¶Ó­Äú " + Context.User.Identity.Name + "! <" + "span class=Accent" + ">|<" + "/span" + ">";

                // if authentication mode is Cookie, provide a logoff link
                if (Context.User.Identity.AuthenticationType == "Forms")
                {
                    LogoffLink = "<" + "span class=\"Accent\">|</span>\n" + "<" + "a href=" + Global.GetApplicationPath(Request) + "/Admin/Logoff.aspx class=SiteLink> ×¢Ïú" + "<" + "/a>";
                }
            }
            else
            {
                WelcomeMessage.Text = "»¶Ó­Äú! <" + "span class=Accent" + ">|<" + "/span" + ">";
            }

            // Dynamically render portal tab strip
            if (ShowTabs == true)
            {

                tabIndex = portalSettings.ActiveTab.TabIndex;

                // Build list of tabs to be shown to user
                ArrayList authorizedTabs = new ArrayList();
                int addedTabs = 0;

                for (int i = 0; i < portalSettings.DesktopTabs.Count; i++)
                {

                    TabStripDetails tab = (TabStripDetails)portalSettings.DesktopTabs[i];

                    if (PortalSecurity.IsInRoles(tab.AuthorizedRoles))
                    {
                        authorizedTabs.Add(tab);
                    }

                    if (addedTabs == tabIndex)
                    {
                        selectedIndex = addedTabs;
                        hashTable.Add(addedTabs, "active");
                    }
                    else
                    {
                        hashTable.Add(addedTabs, "m");
                    }

                    addedTabs++;
                }
                
                // Populate Tab List at Top of the Page with authorized tabs
                tabsRepeater.DataSource = authorizedTabs;
                tabsRepeater.DataBind();
            }

            this.lblDatatime.Text = DateTime.Now.ToLongDateString() + DateTime.Now.ToLongTimeString();

        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            this.lblDatatime.Text = DateTime.Now.ToLongDateString() + DateTime.Now.ToLongTimeString();
        }
    }
}
