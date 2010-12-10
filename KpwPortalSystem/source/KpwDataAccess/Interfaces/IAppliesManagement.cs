using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nairc.KpwFramework.DataModel;

namespace Nairc.KpwDataAccess
{
    public interface IAppliesManagement
    {
        IEnumerable<Apply> GetMyApplies(string userId);
        IEnumerable<Apply> GetMyApplies(DateTime date, string userId);
        IEnumerable<Apply> GetAppliesByDate(DateTime date);
        IEnumerable<Apply> GetCurrentApplies();
        int AddApply(Apply apply);
        void UpdateApply(Apply apply);
        void DeleteApply(int id);
        IEnumerable<Apply> GetApplyByTimeRange(DateTime date, int range, ApplyStatus status);
        Apply GetApplyById(int id);
    }
}
