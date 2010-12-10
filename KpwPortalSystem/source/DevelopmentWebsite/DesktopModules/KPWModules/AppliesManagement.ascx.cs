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

        protected void GridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.Cells[4].Text == "已申请")
                e.Row.Cells[5].Text = "批准申请";
            else if (e.Row.Cells[4].Text == "批准")
                e.Row.Cells[5].Text = "取消申请";
           
        }

        private void LoadCurrentApplies()
        {
            this.GridView1.DataSource = null;

            ApplyDB db = new ApplyDB();

            var applies = db.GetCurrentApplies();

            var query = from q in applies
                        select new
                        {
                            Date = q.ApplyDate,
                            TimeRange = Helper.GetTimeRange(q.TimeRange),
                            Status = Helper.GetStatus(q.ApplyStatus)
                        };
            this.GridView1.DataSource = query;
            this.GridView1.DataBind();
        }
    }
}