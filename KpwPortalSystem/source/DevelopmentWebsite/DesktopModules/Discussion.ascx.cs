using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Nairc.KPWPortal;
using Nairc.KPWPortal.BusinessLayer.BusinessObjects;
using Nairc.KPWPortal.BusinessLayer.Facade;

namespace DesktopModules.Web
{
    public partial class Discussion : PortalModuleControl
    {
        //*******************************************************
        //
        // The Page_Load server event handler on this User Control is used
        // on the first visit of the page to obtain and databind a list of
        // discussion messages.
        //
        //*******************************************************

        protected void Page_Load(Object Sender, EventArgs e)
        {
            if (Page.IsPostBack == false)
            {
                BindList();
            }
        }

        //*******************************************************
        //
        // The BindList method obtains the list of top-level messages
        // from the Discussion table and then databinds them against
        // the "TopLevelList" asp:datalist server control.  It uses
        // the Nairc.KPWPortal.DiscussionDB() data component to encapsulate
        // all data access functionality.
        //
        //*******************************************************

        private void BindList()
        {
            // Obtain a list of discussion messages for the module
            // and bind to datalist        
            IDesktopModulesFacade facade = new DesktopModulesFacade();
            TopLevelList.DataSource = facade.TopLevelMessages(ModuleId);
            TopLevelList.DataBind();
        }

        //*******************************************************
        //
        // The GetThreadMessages method is used to obtain the list
        // of messages contained within a sub-topic of the
        // a top-level discussion message thread.  This method is
        // used to populate the "DetailList" asp:datalist server control
        // in the SelectedItemTemplate of "TopLevelList".
        //
        //*******************************************************

        protected IList<PortalDiscussion> GetThreadMessages()
        {
            // Obtain a list of discussion messages for the module        
            IDesktopModulesFacade facade = new DesktopModulesFacade();
            IList<PortalDiscussion> view = facade.ThreadMessages(TopLevelList.DataKeys[TopLevelList.SelectedIndex].ToString());

            // Return the filtered DataView
            return view;
        }

        //*******************************************************
        //
        // The TopLevelList_Select server event handler is used to
        // expand/collapse a selected discussion topic within the
        // hierarchical <asp:DataList> server control.
        //
        //*******************************************************

        protected void TopLevelList_Select(object Sender, DataListCommandEventArgs e)
        {
            // Determine the command of the button (either "select" or "collapse")
            String command = ((ImageButton) e.CommandSource).CommandName;

            // Update asp:datalist selection index depending upon the type of command
            // and then rebind the asp:datalist with content

            if (command == "collapse")
            {
                TopLevelList.SelectedIndex = -1;
            }
            else
            {
                TopLevelList.SelectedIndex = e.Item.ItemIndex;
            }

            BindList();
        }

        //*******************************************************
        //
        // The FormatUrl method is a helper messages called by a
        // databinding statement within the <asp:DataList> server
        // control template.  It is defined as a helper method here
        // (as opposed to inline within the template) to to improve
        // code organization and avoid embedding logic within the
        // content template.
        //
        //*******************************************************

        protected String FormatUrl(int item)
        {
            return "~/DesktopModules/DiscussDetails.aspx?ItemID=" + item + "&mid=" + ModuleId;
        }

        //*******************************************************
        //
        // The NodeImage method is a helper method called by a
        // databinding statement within the <asp:datalist> server
        // control template.  It controls whether or not an item
        // in the list should be rendered as an expandable topic
        // or just as a single node within the list.
        //
        //*******************************************************

        protected static String NodeImage(int count)
        {
            if (count > 0)
            {
                return "~/images/plus.gif";
            }
            else
            {
                return "~/images/node.gif";
            }
        }
    }
}