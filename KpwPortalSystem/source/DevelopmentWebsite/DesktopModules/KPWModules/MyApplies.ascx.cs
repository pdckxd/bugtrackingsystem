using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Nairc.KpwDataAccess;
using Nairc.KpwFramework.DataModel;
using Nairc.KPWPortal;
using System.Linq;

namespace DesktopModules.Web
{
    public partial class MyApplies : PortalModuleControl
    {
        private CheckBox[] TimeRangeCheckBoxs
        {
            get
            {
                return new CheckBox[]{
                    CheckBox1,
                    CheckBox1,
                    CheckBox2,
                    CheckBox3,
                    CheckBox4,
                    CheckBox5,
                    CheckBox6,
                    CheckBox7,
                    CheckBox8,
                    CheckBox9,
                    CheckBox10,
                    CheckBox11,
                    CheckBox12,
                    CheckBox13,
                    CheckBox14,
                    CheckBox15,
                    CheckBox16,
                    CheckBox17,
                    CheckBox18,
                    CheckBox19,
                    CheckBox20,
                    CheckBox21,
                    CheckBox22,
                    CheckBox23,
                    CheckBox24
                };
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (this.datepicker.Value.Length == 0)
                    this.datepicker.Value = DateTime.Now.Date.ToShortDateString();

                this.SearchAppliesByDate(DateTime.Now.Date);
                this.LoadMyApplies();
            }
        }

        protected void btnOk_Click(object sender, EventArgs e)
        {
            DateTime date = DateTime.Now.Date;

            if (!string.IsNullOrEmpty(this.datepicker.Value))
            {
                date = DateTime.Parse(this.datepicker.Value);
            }

            this.SearchAppliesByDate(date);
            this.LoadMyApplies();
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            ApplyDB db = new ApplyDB();
            DateTime date = DateTime.Parse(this.datepicker.Value);

            for (int i = 0; i < 24; i++)
            {
                var checkbox = TimeRangeCheckBoxs[i];

                if (checkbox.Enabled == true)
                {
                    var applies = db.GetApplyByTimeRange(HttpContext.Current.User.Identity.Name, date, i, ApplyStatus.Submitted);

                    if (checkbox.Checked)
                    {
                        if (applies.Count()==0)
                        {
                            var apply = new Apply()
                            {
                                UserId = HttpContext.Current.User.Identity.Name,
                                ApplyDate = date,
                                ApplyStatus =  ApplyStatus.Submitted,
                                TimeRange = i,
                                CreatedDate = DateTime.Now.Date
                            };

                            db.AddApply(apply);
                        }
                    }
                    else
                    {
                        if (applies.Count() > 0)
                            db.DeleteApply(applies.First().ID);
                    }
                }
            }

            this.SearchAppliesByDate(date);
            this.LoadMyApplies();
        }

        private void SearchAppliesByDate(DateTime date)
        {

            this.ClearCheckbox();

            ApplyDB db = new ApplyDB();

            IEnumerable<Apply> applies = db.GetAppliesByDate(date);

            foreach (var item in applies)
            {
                if (item.ApplyStatus == ApplyStatus.Submitted && item.UserId == HttpContext.Current.User.Identity.Name)
                {
                    TimeRangeCheckBoxs[item.TimeRange].Checked = true;
                }
                else if (item.ApplyStatus == ApplyStatus.Approved)
                {
                    TimeRangeCheckBoxs[item.TimeRange].Enabled = false;

                    var myapples = db.GetMyApplies(date, HttpContext.Current.User.Identity.Name);

                    if (myapples.Select(x => x.ID == item.ID).Any())
                    {
                        TimeRangeCheckBoxs[item.TimeRange].Checked = true;
                    }
                }
                //else
                //{
                //    FindCheckBox(item.TimeRange).Enabled = false;
                //}
            }
        }

        private void ClearCheckbox()
        {
            foreach (var item in this.TimeRangeCheckBoxs)
            {
                item.Enabled = true;
                item.Checked = false;
            }
        }

        private void LoadMyApplies()
        {
            this.GridView1.DataSource = null;

            ApplyDB db = new ApplyDB();

            IEnumerable<Apply> applies = db.GetMyApplies(HttpContext.Current.User.Identity.Name);

            var query = from q in applies
                        select new
                        {
                            Date = q.ApplyDate,
                            TimeRange = Helper.TimeRangeArray[q.TimeRange],
                            Status = Helper.GetStatus(q.ApplyStatus)
                        };
            this.GridView1.DataSource = query;
            this.GridView1.DataBind();
        }

       

    
    }
}