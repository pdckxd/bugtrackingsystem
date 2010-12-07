using System;
using System.Data;
using Nairc.KPWPortal.BusinessLayer.BusinessObjects;
using Nairc.KPWPortal.DataLayer.Interface;
using Nairc.KPWPortal.Framework.Data;

namespace Nairc.KPWPortal.DataLayer.DataObjects.SqlServer
{
    internal class HtmlTextDB : IHtmlTextDB
    {
        /// <summary>
        /// details about a specific item 
        /// </summary>
        public PortalHtmlText GetHtmlText(int moduleId)
        {
            return Db.Map<PortalHtmlText>(StoredProcedureNames.GetHtmlText, CommandType.StoredProcedure,
                                          Db.CreateParameter("ModuleID", moduleId));
        }

        /// <summary>
        /// updates a specified item 
        /// </summary>
        public void UpdateHtmlText(int moduleId, String desktopHtml, String mobileSummary, String mobileDetails)
        {
            Db.ExecuteNonQuery(StoredProcedureNames.UpdateHtmlText, CommandType.StoredProcedure,
                               Db.CreateParameter("ModuleID", moduleId),
                               Db.CreateParameter("DesktopHtml", desktopHtml),
                               Db.CreateParameter("MobileSummary", mobileSummary),
                               Db.CreateParameter("MobileDetails", mobileDetails));
        }
    }
}