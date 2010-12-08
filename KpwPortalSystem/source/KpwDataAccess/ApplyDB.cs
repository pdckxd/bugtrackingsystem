using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nairc.KpwFramework.DataModel;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;

namespace Nairc.KpwDataAccess
{
    public class ApplyDB:IAppliesManagement
    {
        #region IAppliesManagement Members

        public IEnumerable<Nairc.KpwFramework.DataModel.Apply> GetMyApplies(string userId)
        {
            List<Apply> applies = new List<Apply>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "SELECT [ID],[UserId],[ApplyDate],[TimeRange],"
                                + "[ApplyStatus],[DateCreated]"
                                + "FROM [Kpw_Images] WHERE [UserId]=@UserId";

            DbCommand dbCommand = db.GetSqlStringCommand(sqlCommand);

            db.AddInParameter(dbCommand, "UserId", DbType.String, userId);


            // DataSet that will hold the returned results		
            using (IDataReader reader = db.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    applies.Add(new Apply
                    {
                        ID = reader.GetInt32(0),
                        UserId = reader.GetString(1),
                        
                    });
                }
            }

            return applies;
        }

        public IEnumerable<Nairc.KpwFramework.DataModel.Apply> GetAppliesByDate(DateTime date)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Nairc.KpwFramework.DataModel.Apply> GetCurrentApplies()
        {
            throw new NotImplementedException();
        }

        public int AddApply(Nairc.KpwFramework.DataModel.Apply apply)
        {
            throw new NotImplementedException();
        }

        public void UpdateApply(Nairc.KpwFramework.DataModel.Apply apply)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
