using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nairc.BugTrackingSystem.DataAccess
{
    public class Database
    {
        #region Database Members

        public System.Data.Common.DbCommand GetSqlStringCommand(string sqlString)
        {
            throw new NotImplementedException();
        }

        public virtual System.Data.Common.DbCommand GetStoredProcCommand(string storedProcString)
        {
            throw new NotImplementedException();
        }

        public virtual void AddInParameter(System.Data.Common.DbCommand cmd, string name, System.Data.DbType dbType, object value)
        {
            throw new NotImplementedException();
        }

        public virtual System.Data.IDataReader ExecuteReader(System.Data.Common.DbCommand cmd)
        {
            throw new NotImplementedException();
        }

        public virtual System.Data.IDataReader ExecuteNoQuery(System.Data.Common.DbCommand cmd)
        {
            throw new NotImplementedException();
        }

        public virtual void LoadDataSet(System.Data.Common.DbCommand command, System.Data.DataSet dataSet, string tableName)
        {
            throw new NotImplementedException();
        }

        public virtual void LoadDataSet(System.Data.Common.DbCommand command, System.Data.DataSet dataSet, string[] tableNames)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
