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

            var query = from x in applies
                        where x.ApplyStatus == ApplyStatus.Approved && DateTime.Now >= x.TimeFrom && DateTime.Now <= x.TimeTo
                        select x;

            return query.Any();
        }
    }
}
