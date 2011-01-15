using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Nairc.KpwFramework.DataModel;
using Nairc.KpwDataAccess;
using Nairc.KPWPortal;

namespace DesktopModules.Web
{
    public partial class AppliesManagement : PortalModuleControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.LoadCurrentApplies();
            }
        }

        protected void GridView_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int id = int.Parse(e.Keys[e.RowIndex].ToString());
            ApplyDB db = new ApplyDB();
            Apply apply = db.GetApplyById(id);

            if (apply != null && apply.ApplyStatus == ApplyStatus.Submitted)
            {
                apply.ApplyStatus = ApplyStatus.Approved;
                db.UpdateApply(apply);
            }
            else if (apply != null && apply.ApplyStatus == ApplyStatus.Approved)
            {
                apply.ApplyStatus = ApplyStatus.Deactivated;
                db.UpdateApply(apply);
            }
            this.LoadCurrentApplies();

        }

        protected void GridView1_RowCommand(object sender,GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Approve" || e.CommandName == "Deactivate")
            {
                int index = Convert.ToInt32(e.CommandArgument);

                int id = int.Parse(GridView1.DataKeys[index].Value.ToString());
                ApplyDB db = new ApplyDB();
                Apply apply = db.GetApplyById(id);

                if (apply != null && apply.ApplyStatus == ApplyStatus.Submitted)
                {
                    apply.ApplyStatus = ApplyStatus.Approved;
                    db.UpdateApply(apply);

                    var applies = db.GetApplyByTimeRange(apply.ApplyDate, apply.TimeRange, ApplyStatus.Submitted);

                    foreach (var item in applies)
                    {
                        item.ApplyStatus = ApplyStatus.Deactivated;
                        db.UpdateApply(item);
                    }
                }
                else if (apply != null && apply.ApplyStatus == ApplyStatus.Approved)
                {
                    apply.ApplyStatus = ApplyStatus.Deactivated;
                    db.UpdateApply(apply);
                }
                this.LoadCurrentApplies();
            }
        }


        protected void GridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowIndex > -1)
            {
                if (e.Row.Cells[3].Text == "已申请")
                    (e.Row.Cells[4].FindControl("cancelAction") as Button).Visible = false;
                else if (e.Row.Cells[3].Text == "批准")
                    (e.Row.Cells[4].FindControl("approveAction") as Button).Visible = false;
                else
                {
                    (e.Row.Cells[4].FindControl("cancelAction") as Button).Visible = false;
                    (e.Row.Cells[4].FindControl("approveAction") as Button).Visible = false;
                }
            }
        }

        private void LoadCurrentApplies()
        {
            this.GridView1.DataSource = null;

            ApplyDB db = new ApplyDB();

            var applies = db.GetCurrentApplies();

            var query = from q in applies
                        select new
                        {
                            ID = q.ID,
                            Name = q.UserId,
                            Date = q.ApplyDate,
                            TimeRange = Helper.TimeRangeArray[q.TimeRange],
                            Status = Helper.GetStatus(q.ApplyStatus)
                        };
            this.GridView1.DataSource = query.ToList();
            this.GridView1.DataBind();
        }

        protected void GridView_PageIndexChanging(Object sender, GridViewPageEventArgs e)
        {
            this.GridView1.PageIndex = e.NewPageIndex;
            LoadCurrentApplies();
        }
    }
}