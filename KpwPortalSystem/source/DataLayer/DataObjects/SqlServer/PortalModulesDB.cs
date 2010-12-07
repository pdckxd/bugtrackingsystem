using System.Data;
using System.Data.Common;
using Nairc.KPWPortal.DataLayer.Interface;
using Nairc.KPWPortal.Framework.Data;

namespace Nairc.KPWPortal.DataLayer.DataObjects.SqlServer
{
    internal class PortalModulesDB : IPortalModulesDB
    {
        public void DeletePortalModule(params int[] ModuleIdList)
        {
            DbParameter parameterModuleID = Db.CreateParameter("ModuleID");
            DbCommand myCommand = Db.CreateCommand(StoredProcedureNames.DeleteModule, CommandType.StoredProcedure);

            myCommand.Connection.Open();
            foreach (int ModuleId in ModuleIdList)
            {
                myCommand.Parameters.Clear();
                parameterModuleID.Value = ModuleId;
                myCommand.Parameters.Add(parameterModuleID);
                // Open the database connection and execute the command
                myCommand.ExecuteNonQuery();
            }
            // Close the connection
            myCommand.Connection.Close();
        }
    }
}