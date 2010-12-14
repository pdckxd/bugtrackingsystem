using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nairc.KpwDataAccess;
using Nairc.KpwFramework.DataModel;

namespace Nairc.KpwFramework.TelescopeControler
{
    public class ObservationManagementFacade
    {
        public bool IsUserCanObservation(string user)
        {
            ApplyDB db = new ApplyDB();

            IEnumerable<Apply> applies = db.GetMyApplies(DateTime.Now.Date, user);

            var query = applies.Select(x => x.ApplyStatus == ApplyStatus.Approved && DateTime.Now >= x.TimeFrom && DateTime.Now <= x.TimeTo);

            return query.First();
        }
    }
}
