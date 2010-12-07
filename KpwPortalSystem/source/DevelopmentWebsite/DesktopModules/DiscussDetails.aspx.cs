using System;
using System.Web.UI;
using Nairc.KPWPortal;
using Nairc.KPWPortal.BusinessLayer.BusinessObjects;
using Nairc.KPWPortal.BusinessLayer.Facade;

namespace DesktopModules.Web
{
    public partial class DiscussDetails : Page
    {
        /*private IDesktopModulesFacade _thefacade;

        [Microsoft.Practices.ObjectBuilder.Dependency]
        public IDesktopModulesFacade thefacade
        {
            set{ _thefacade = value;}
        }*/

        private int moduleId = 0;
        private int itemId = 0;

        //*******************************************************
        //
        // The Page_Load server event handler on this page is used
        // to obtain the ModuleId and ItemId of the discussion list,
        // and to then display the message contents.
        //
        //*******************************************************

        protected void Page_Load(Object sender, EventArgs e)
        {
            // Obtain moduleId and ItemId from QueryString
            moduleId = Int32.Parse(Request.Params["Mid"]);

            if (Request.Params["ItemId"] != null)
            {
                itemId = Int32.Parse(Request.Params["ItemId"]);
            }
            else
            {
                itemId = 0;
                EditPanel.Visible = true;
                ButtonPanel.Visible = false;
            }

            // Populate message contents if this is the first visit to the page
            if (Page.IsPostBack == false && itemId != 0)
            {
                BindData();
            }

            if (PortalSecurity.HasEditPermissions(moduleId) == false)
            {
                if (itemId == 0)
                {
                    Response.Redirect("~/Errors/EditAccessDenied.aspx");
                }
                else
                {
                    ReplyBtn.Visible = false;
                }
            }
        }

        //*******************************************************
        //
        // The ReplyBtn_Click server event handler on this page is used
        // to handle the scenario where a user clicks the message's
        // "Reply" button to perform a post.
        //
        //*******************************************************

        protected void ReplyBtn_Click(Object Sender, EventArgs e)
        {
            EditPanel.Visible = true;
            ButtonPanel.Visible = false;
        }

        //*******************************************************
        //
        // The UpdateBtn_Click server event handler on this page is used
        // to handle the scenario where a user clicks the "update"
        // button after entering a response to a message post.
        //
        //*******************************************************

        protected void UpdateBtn_Click(Object sender, EventArgs e)
        {
            // Create new discussion database component            
            // Add new message (updating the "itemId" on the page)
            IDesktopModulesFacade facade = new DesktopModulesFacade();
            PortalDiscussion msg=new PortalDiscussion();
            msg.ModuleID = moduleId;           
            msg.CreatedByUser = User.Identity.Name;
            msg.Title =Server.HtmlEncode(TitleField.Text);
            msg.Body = Server.HtmlEncode(BodyField.Text);
            itemId = facade.AddMessage(msg, itemId);

            // Update visibility of page elements
            EditPanel.Visible = false;
            ButtonPanel.Visible = true;

            // Repopulate page contents with new message
            BindData();
        }

        //*******************************************************
        //
        // The CancelBtn_Click server event handler on this page is used
        // to handle the scenario where a user clicks the "cancel"
        // button to discard a message post and toggle out of
        // edit mode.
        //
        //*******************************************************

        protected void CancelBtn_Click(Object sender, EventArgs e)
        {
            // Update visibility of page elements
            EditPanel.Visible = false;
            ButtonPanel.Visible = true;
        }

        //*******************************************************
        //
        // The BindData method is used to obtain details of a message
        // from the Discussion table, and update the page with
        // the message content.
        //
        //*******************************************************

        private void BindData()
        {
            // Obtain the selected item from the Discussion table        
            IDesktopModulesFacade facade=new DesktopModulesFacade();
            PortalDiscussion discussion = facade.SingleMessage(itemId);
            
            // Security check.  verify that itemid is within the module.
            int dbModuleID = discussion.ModuleID;
            if (dbModuleID != moduleId)
            {
                Response.Redirect("~/Errors/EditAccessDenied.aspx");
            }

            // Update labels with message contents
            Title.Text = discussion.Title;
            Body.Text = discussion.Body;
            CreatedByUser.Text = discussion.CreatedByUser;
            CreatedDate.Text = String.Format("{0:d}", discussion.CreatedDate);
            TitleField.Text = ReTitle(Title.Text);

            int prevId = 0;
            int nextId = 0;

            // Update next and preview links            
            if (discussion.PrevMessageID.HasValue)
            {
                prevId = discussion.PrevMessageID.Value;
                prevItem.HRef = Request.Path + "?ItemId=" + prevId + "&mid=" + moduleId;
            }
            
            if (discussion.NextMessageID.HasValue)
            {
                nextId = discussion.NextMessageID.Value;
                nextItem.HRef = Request.Path + "?ItemId=" + nextId + "&mid=" + moduleId;
            }

            // Show/Hide Next/Prev Button depending on whether there is a next/prev message
            if (prevId <= 0)
            {
                prevItem.Visible = false;
            }

            if (nextId <= 0)
            {
                nextItem.Visible = false;
            }
        }

        //*******************************************************
        //
        // The ReTitle helper method is used to create the subject
        // line of a response post to a message.
        //
        //*******************************************************

        private static String ReTitle(String title)
        {
            if (title.Length > 0 & title.IndexOf("Re: ", 0) == -1)
            {
                title = "Re: " + title;
            }

            return title;
        }

    }
}
