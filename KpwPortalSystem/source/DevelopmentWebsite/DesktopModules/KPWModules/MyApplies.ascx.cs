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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (this.datepicker.Value.Length == 0)
                    this.datepicker.Value = DateTime.Now.ToShortDateString();

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

            for (int i = 0; i < 13; i++)
            {
                var checkbox = FindCheckBox(i);

                if (checkbox.Enabled == true)
                {
                    var apply = db.GetApplyByTimeRange(date, i);

                    if (checkbox.Checked)
                    {
                        if (apply == null)
                        {
                            apply = new Apply()
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
                        if (apply != null)
                            db.DeleteApply(apply.ID);
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
                if (item.ApplyStatus == ApplyStatus.Submitted)
                {
                    FindCheckBox(item.TimeRange).Checked = true;
                }
                else if (item.ApplyStatus == ApplyStatus.Approved)
                {
                    FindCheckBox(item.TimeRange).Enabled = false;

                    var myapples = db.GetMyApplies(date, HttpContext.Current.User.Identity.Name);

                    if (myapples.Select(x => x.ID == item.ID).Any())
                    {
                        FindCheckBox(item.TimeRange).Checked = true;
                    }
                }
            }
        }

        private void ClearCheckbox()
        {
            this.CheckBox1.Enabled = true;
            this.CheckBox1.Checked = false;

            this.CheckBox2.Enabled = true;
            this.CheckBox2.Checked = false;

            this.CheckBox3.Enabled = true;
            this.CheckBox3.Checked = false;

            this.CheckBox4.Enabled = true;
            this.CheckBox4.Checked = false;

            this.CheckBox5.Enabled = true;
            this.CheckBox5.Checked = false;

            this.CheckBox6.Enabled = true;
            this.CheckBox6.Checked = false;

            this.CheckBox7.Enabled = true;
            this.CheckBox7.Checked = false;

            this.CheckBox8.Enabled = true;
            this.CheckBox8.Checked = false;

            this.CheckBox9.Enabled = true;
            this.CheckBox9.Checked = false;

            this.CheckBox10.Enabled = true;
            this.CheckBox10.Checked = false;

            this.CheckBox11.Enabled = true;
            this.CheckBox11.Checked = false;

            this.CheckBox12.Enabled = true;
            this.CheckBox12.Checked = false;

        }

        private CheckBox FindCheckBox(int i)
        {
            switch (i)
            {
                case 1:
                    return this.CheckBox1;
                case 2:
                    return this.CheckBox2;
                case 3:
                    return this.CheckBox3;
                case 4:
                    return this.CheckBox4;
                case 5:
                    return this.CheckBox5;
                case 6:
                    return this.CheckBox6;
                case 7:
                    return this.CheckBox7;
                case 8:
                    return this.CheckBox8;
                case 9:
                    return this.CheckBox9;
                case 10:
                    return this.CheckBox10;
                case 11:
                    return this.CheckBox11;
                default:
                    return this.CheckBox12;
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
                            TimeRange = Helper.GetTimeRange(q.TimeRange),
                            Status = Helper.GetStatus(q.ApplyStatus)
                        };
            this.GridView1.DataSource = query;
            this.GridView1.DataBind();
        }

       

    
    }
}