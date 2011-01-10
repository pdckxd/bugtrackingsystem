using System;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Nairc.KPWPortal;
using Nairc.KPWPortal.BusinessLayer.BusinessObjects;
using Nairc.KPWPortal.BusinessLayer.Facade;

namespace Admin.Web
{
    /// <summary>The TabLayout.aspx page is used to control the layout settings of an
    /// individual tab within the portal.
    /// </summary>
    public partial class TabLayout : Page
    {        
        private int tabId = 0;
        protected ArrayList leftList;
        protected ArrayList contentList;
        protected ArrayList rightList;
        
        /// <summary>The Page_Load server event handler on this page is used
        /// to populate a tab's layout settings on the page
        /// </summary>
        /// <param name="Sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(Object Sender, EventArgs e)
        {
            // Verify that the current user has access to access this page
            if (PortalSecurity.IsInRoles("Admins") == false)
            {
                Response.Redirect("~/Errors/EditAccessDenied.aspx");
            }

            // Determine Tab to Edit
            if (Request.Params["tabid"] != null)
            {
                tabId = Int32.Parse(Request.Params["tabid"]);
            }

            // If first visit to the page, update all entries
            if (Page.IsPostBack == false)
            {
                BindData();
            }
        }
        
        /// <summary>The AddModuleToPane_Click server event handler on this page is used
        /// to add a new portal module into the tab
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void AddModuleToPane_Click(Object sender, EventArgs e)
        {
            // All new modules go to the end of the contentpane
            ModuleItem m = new ModuleItem();
            m.ModuleTitle = moduleTitle.Text;
            m.ModuleDefId = Int32.Parse(moduleType.SelectedItem.Value);
            m.ModuleOrder = 999;

            // save to database
            Configuration config = new Configuration();
            m.ModuleId =
                config.AddModule(tabId, m.ModuleOrder, "ContentPane", m.ModuleTitle, m.ModuleDefId, 0, "Admins", false);

            // Obtain portalId from Current Context
            PortalSettings portalSettings = (PortalSettings) Context.Items["PortalSettings"];

            // reload the portalSettings from the database
            HttpContext.Current.Items["PortalSettings"] = new PortalSettings(portalSettings.PortalId, tabId);

            // reorder the modules in the content pane
            ArrayList modules = GetModules("ContentPane");
            OrderModules(modules);

            // resave the order
            foreach (ModuleItem item in modules)
            {
                config.UpdateModuleOrder(item.ModuleId, item.ModuleOrder, "ContentPane");
            }

            // Redirect to the same page to pick up changes
            Response.Redirect(Request.RawUrl);
        }
        
        /// <summary>The UpDown_Click server event handler on this page is
        /// used to move a portal module up or down on a tab's layout pane
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void UpDown_Click(Object sender, ImageClickEventArgs e)
        {
            String cmd = ((ImageButton) sender).CommandName;
            String pane = ((ImageButton) sender).CommandArgument;
            ListBox _listbox = (ListBox) Page.FindControl(pane);

            ArrayList modules = GetModules(pane);

            if (_listbox.SelectedIndex != -1)
            {
                int delta;
                int selection = -1;

                // Determine the delta to apply in the order number for the module
                // within the list.  +3 moves down one item; -3 moves up one item

                if (cmd == "down")
                {
                    delta = 3;
                    if (_listbox.SelectedIndex < _listbox.Items.Count - 1)
                    {
                        selection = _listbox.SelectedIndex + 1;
                    }
                }
                else
                {
                    delta = -3;
                    if (_listbox.SelectedIndex > 0)
                    {
                        selection = _listbox.SelectedIndex - 1;
                    }
                }

                ModuleItem m;
                m = (ModuleItem) modules[_listbox.SelectedIndex];
                m.ModuleOrder += delta;

                // reorder the modules in the content pane
                OrderModules(modules);

                // resave the order
                Configuration config = new Configuration();
                foreach (ModuleItem item in modules)
                {
                    config.UpdateModuleOrder(item.ModuleId, item.ModuleOrder, pane);
                }
            }

            // Redirect to the same page to pick up changes
            Response.Redirect(Request.RawUrl);
        }
        
        /// <summary>The RightLeft_Click server event handler on this page is
        /// used to move a portal module between layout panes on
        /// the tab page
        /// </summary>        
        protected void RightLeft_Click(Object sender, ImageClickEventArgs e)
        {
            String sourcePane = ((ImageButton) sender).Attributes["sourcepane"];
            String targetPane = ((ImageButton) sender).Attributes["targetpane"];

            //fix send by: http://www.codeplex.com/site/users/view/pathurun
            ContentPlaceHolder cph = (ContentPlaceHolder)Page.Master.FindControl("maincontent");
            ListBox sourceBox = (ListBox)cph.FindControl(sourcePane);//(ListBox) Page.FindControl(sourcePane);
            ListBox targetBox = (ListBox)cph.FindControl(targetPane);//(ListBox) Page.FindControl(targetPane);

            if (sourceBox.SelectedIndex != -1)
            {
                // get source arraylist
                ArrayList sourceList = GetModules(sourcePane);

                // get a reference to the module to move
                // and assign a high order number to send it to the end of the target list
                ModuleItem m = (ModuleItem) sourceList[sourceBox.SelectedIndex];

                // add it to the database
                Configuration config = new Configuration();
                config.UpdateModuleOrder(m.ModuleId, 998, targetPane);

                // delete it from the source list
                sourceList.RemoveAt(sourceBox.SelectedIndex);

                // Obtain portalId from Current Context
                PortalSettings portalSettings = (PortalSettings) Context.Items["PortalSettings"];

                // reload the portalSettings from the database
                HttpContext.Current.Items["PortalSettings"] = new PortalSettings(portalSettings.PortalId, tabId);

                // reorder the modules in the source pane
                sourceList = GetModules(sourcePane);
                OrderModules(sourceList);

                // resave the order
                foreach (ModuleItem item in sourceList)
                {
                    config.UpdateModuleOrder(item.ModuleId, item.ModuleOrder, sourcePane);
                }

                // reorder the modules in the target pane
                ArrayList targetList = GetModules(targetPane);
                OrderModules(targetList);

                // resave the order
                foreach (ModuleItem item in targetList)
                {
                    config.UpdateModuleOrder(item.ModuleId, item.ModuleOrder, targetPane);
                }

                // Redirect to the same page to pick up changes
                Response.Redirect(Request.RawUrl);
            }
        }
        
        /// <summary>The Apply_Click server event handler on this page is
        /// used to save the current tab settings to the database and
        /// then redirect back to the main admin page.
        /// </summary>
        /// <param name="Sender"></param>
        /// <param name="e"></param>
        protected void Apply_Click(Object Sender, EventArgs e)
        {
            // Save changes then navigate back to admin.
            String id = ((LinkButton) Sender).ID;

            SaveTabData();

            // redirect back to the admin page

            // Obtain PortalSettings from Current Context
            PortalSettings portalSettings = (PortalSettings) Context.Items["PortalSettings"];
            int adminIndex = portalSettings.DesktopTabs.Count - 1;

            Response.Redirect("~/DesktopDefault.aspx?tabindex=" + adminIndex + "&tabid=" +
                              ((TabStripDetails) portalSettings.DesktopTabs[adminIndex]).TabId);
        }
        
        /// <summary>The TabSettings_Change server event handler on this page is
        /// invoked any time the tab name or access security settings
        /// change.  The event handler in turn calls the "SaveTabData"
        /// helper method to ensure that these changes are persisted
        /// to the portal configuration file.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TabSettings_Change(Object sender, EventArgs e)
        {
            // Ensure that settings are saved
            SaveTabData();
        }
        
        /// <summary>The SaveTabData helper method is used to persist the
        /// current tab settings to the database.
        /// </summary>
        protected void SaveTabData()
        {
            // Construct Authorized User Roles String
            String authorizedRoles = "";

            foreach (ListItem item in authRoles.Items)
            {
                if (item.Selected)
                {
                    string role = item.Text == "所有用户" ? "All Users" : item.Text;
                    authorizedRoles = authorizedRoles + role + ";";
                }
            }

            // Obtain PortalSettings from Current Context
            PortalSettings portalSettings = (PortalSettings) Context.Items["PortalSettings"];

            // update Tab info in the database
            Configuration config = new Configuration();
            config.UpdateTab(portalSettings.PortalId, tabId, tabName.Text, portalSettings.ActiveTab.TabOrder,
                             authorizedRoles, mobileTabName.Text, showMobile.Checked);
        }
        
        /// <summary>The EditBtn_Click server event handler on this page is
        /// used to edit an individual portal module's settings
        /// </summary>
        protected void EditBtn_Click(Object sender, ImageClickEventArgs e)
        {
            String pane = ((ImageButton) sender).CommandArgument;

            //fix send by: http://www.codeplex.com/site/users/view/pathurun
            ContentPlaceHolder cph = (ContentPlaceHolder)Page.Master.FindControl("maincontent");
            ListBox _listbox = (ListBox)cph.FindControl(pane); //(ListBox)Page.FindControl(pane);

            if (_listbox.SelectedIndex != -1)
            {
                int mid = Int32.Parse(_listbox.SelectedItem.Value);

                // Redirect to module settings page
                Response.Redirect("ModuleSettings.aspx?mid=" + mid + "&tabid=" + tabId);
            }
        }
        
        /// <summary>The DeleteBtn_Click server event handler on this page is
        /// used to delete an portal module from the page
        /// </summary>        
        protected void DeleteBtn_Click(Object sender, ImageClickEventArgs e)
        {
            String pane = ((ImageButton) sender).CommandArgument;

            //fix send by: http://www.codeplex.com/site/users/view/pathurun
            ContentPlaceHolder cph = (ContentPlaceHolder)Page.Master.FindControl("maincontent");
            ListBox _listbox = (ListBox)cph.FindControl(pane);//(ListBox) Page.FindControl(pane);
            
            ArrayList modules = GetModules(pane);

            if (_listbox.SelectedIndex != -1)
            {
                ModuleItem m = (ModuleItem) modules[_listbox.SelectedIndex];
                if (m.ModuleId > -1)
                {
                    // config.DeleteModule() deletes the module in PortalCFG.xml
                    // and deletes any associated content in the database.
                    Configuration config = new Configuration();
                    config.DeleteModule(m.ModuleId);
                }
            }

            // Redirect to the same page to pick up changes
            Response.Redirect(Request.RawUrl);
        }
        
        /// <summary>The BindData helper method is used to update the tab's
        /// layout panes with the current configuration information
        /// </summary>
        private void BindData()
        {
            // Obtain PortalSettings from Current Context
            PortalSettings portalSettings = (PortalSettings) Context.Items["PortalSettings"];
            TabSettings tab = portalSettings.ActiveTab;

            // Populate Tab Names, etc.
            tabName.Text = tab.TabName;
            mobileTabName.Text = tab.MobileTabName;
            showMobile.Checked = tab.ShowMobile;

            // Populate checkbox list with all security roles for this portal
            // and "check" the ones already configured for this tab        
            IAccountFacade facade = new AccountFacade();
            System.Collections.Generic.IList<PortalRole> roles = facade.PortalRoles(portalSettings.PortalId);

            // Clear existing items in checkboxlist
            authRoles.Items.Clear();

            ListItem allItem = new ListItem();
            allItem.Text = "所有用户";

            if (tab.AuthorizedRoles.LastIndexOf("All Users") > -1)
            {
                allItem.Selected = true;
            }

            authRoles.Items.Add(allItem);

            foreach(PortalRole role in roles)
            {
                ListItem item = new ListItem();
                item.Text = role.RoleName;
                item.Value = role.RoleID.ToString();

                if ((tab.AuthorizedRoles.LastIndexOf(item.Text)) > -1)
                {
                    item.Selected = true;
                }

                authRoles.Items.Add(item);
            }

            // Populate the "Add Module" Data
            Configuration config = new Configuration();
            moduleType.DataSource = config.GetModuleDefinitions(portalSettings.PortalId);
            moduleType.DataBind();

            // Populate Right Hand Module Data
            rightList = GetModules("RightPane");
            rightPane.DataBind();

            // Populate Content Pane Module Data
            contentList = GetModules("ContentPane");
            contentPane.DataBind();

            // Populate Left Hand Pane Module Data
            leftList = GetModules("LeftPane");
            leftPane.DataBind();
        }
        
        /// <summary>The GetModules helper method is used to get the modules
        /// for a single pane within the tab
        /// </summary>        
        private ArrayList GetModules(String pane)
        {
            // Obtain PortalSettings from Current Context
            PortalSettings portalSettings = (PortalSettings) Context.Items["PortalSettings"];
            ArrayList paneModules = new ArrayList();

            foreach (Nairc.KPWPortal.ModuleSettings module in portalSettings.ActiveTab.Modules)
            {
                if ((module.PaneName).ToLower() == pane.ToLower())
                {
                    ModuleItem m = new ModuleItem();
                    m.ModuleTitle = module.ModuleTitle;
                    m.ModuleId = module.ModuleId;
                    m.ModuleDefId = module.ModuleDefId;
                    m.ModuleOrder = module.ModuleOrder;
                    paneModules.Add(m);
                }
            }

            return paneModules;
        }
        
        /// <summary>The OrderModules helper method is used to reset the display
        /// order for modules within a pane
        /// </summary>
        private static void OrderModules(ArrayList list)
        {
            int i = 1;

            // sort the arraylist
            list.Sort();

            // renumber the order
            foreach (ModuleItem m in list)
            {
                // number the items 1, 3, 5, etc. to provide an empty order
                // number when moving items up and down in the list.
                m.ModuleOrder = i;
                i += 2;
            }
        }
    }
}