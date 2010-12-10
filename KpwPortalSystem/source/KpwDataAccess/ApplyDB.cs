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
                                + "FROM [Kpw_Applies] WHERE [UserId]=@UserId AND [ApplyDate] >= @Today";

            DbCommand dbCommand = db.GetSqlStringCommand(sqlCommand);

            db.AddInParameter(dbCommand, "UserId", DbType.String, userId);
            db.AddInParameter(dbCommand, "Today", DbType.DateTime, DateTime.Now.Date);


            // DataSet that will hold the returned results		
            using (IDataReader reader = db.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    applies.Add(new Apply
                    {
                        ID = reader.GetInt32(0),
                        UserId = reader.GetString(1),
                        ApplyDate = reader.GetDateTime(2),
                        TimeRange = reader.GetInt32(3),
                        ApplyStatus = (ApplyStatus)reader.GetInt32(4),
                        CreatedDate = reader.GetDateTime(5)
                    });
                }
            }

            return applies;
        }

        public IEnumerable<Nairc.KpwFramework.DataModel.Apply> GetAppliesByDate(DateTime date)
        {
            List<Apply> applies = new List<Apply>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "SELECT [ID],[UserId],[ApplyDate],[TimeRange],"
                                + "[ApplyStatus],[DateCreated]"
                                + "FROM [Kpw_Applies] WHERE [ApplyDate] = @Today";

            DbCommand dbCommand = db.GetSqlStringCommand(sqlCommand);

            db.AddInParameter(dbCommand, "Today", DbType.DateTime, date);


            // DataSet that will hold the returned results		
            using (IDataReader reader = db.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    applies.Add(new Apply
                    {
                        ID = reader.GetInt32(0),
                        UserId = reader.GetString(1),
                        ApplyDate = reader.GetDateTime(2),
                        TimeRange = reader.GetInt32(3),
                        ApplyStatus = (ApplyStatus)reader.GetInt32(4),
                        CreatedDate = reader.GetDateTime(5)
                    });
                }
            }

            return applies;
        }

        public IEnumerable<Nairc.KpwFramework.DataModel.Apply> GetCurrentApplies()
        {
            List<Apply> applies = new List<Apply>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "SELECT [ID],[UserId],[ApplyDate],[TimeRange],"
                                + "[ApplyStatus],[DateCreated]"
                                + "FROM [Kpw_Applies] WHERE [ApplyDate] >= @Today";

            DbCommand dbCommand = db.GetSqlStringCommand(sqlCommand);

            db.AddInParameter(dbCommand, "Today", DbType.DateTime, DateTime.Now.Date);


            // DataSet that will hold the returned results		
            using (IDataReader reader = db.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    applies.Add(new Apply
                    {
                        ID = reader.GetInt32(0),
                        UserId = reader.GetString(1),
                        ApplyDate = reader.GetDateTime(2),
                        TimeRange = reader.GetInt32(3),
                        ApplyStatus = (ApplyStatus)reader.GetInt32(4),
                        CreatedDate = reader.GetDateTime(5)
                    });
                }
            }

            return applies;
        }

        public int AddApply(Nairc.KpwFramework.DataModel.Apply apply)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "INSERT INTO [Kpw_Applies] ([UserId],[ApplyDate],[TimeRange],"
                                + "[ApplyStatus],[DateCreated])"
                                + " VALUES(@UserId,@ApplyDate,@TimeRange,@ApplyStatus,@DateCreated)";

            DbCommand dbCommand = db.GetSqlStringCommand(sqlCommand);

            db.AddInParameter(dbCommand, "UserId", DbType.String, apply.UserId);
            db.AddInParameter(dbCommand, "ApplyDate", DbType.DateTime, apply.ApplyDate);
            db.AddInParameter(dbCommand, "TimeRange", DbType.Int32, apply.TimeRange);
            db.AddInParameter(dbCommand, "ApplyStatus", DbType.Int32, apply.ApplyStatus);
            db.AddInParameter(dbCommand, "DateCreated", DbType.DateTime, apply.CreatedDate);

            return db.ExecuteNonQuery(dbCommand);
        }

        public void UpdateApply(Nairc.KpwFramework.DataModel.Apply apply)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "UPDATE [Kpw_Applies]"
                                  + "SET [UserId] = @UserId"
                                     + ",[ApplyDate] = @ApplyDate"
                                      + ",[TimeRange] = @TimeRange"
                                      + ",[ApplyStatus] = @ApplyStatus"
                                      + ",[DateCreated] = @DateCreated"
                                      + " WHERE [ID] = @ID";

            DbCommand dbCommand = db.GetSqlStringCommand(sqlCommand);

            // Retrieve products from the specified category.
            db.AddInParameter(dbCommand, "ID", DbType.Int32, apply.ID);
            db.AddInParameter(dbCommand, "UserId", DbType.String, apply.UserId);
            db.AddInParameter(dbCommand, "ApplyDate", DbType.DateTime, apply.ApplyDate);
            db.AddInParameter(dbCommand, "TimeRange", DbType.Int32, apply.TimeRange);
            db.AddInParameter(dbCommand, "ApplyStatus", DbType.Int32, apply.ApplyStatus);
            db.AddInParameter(dbCommand, "DateCreated", DbType.DateTime, apply.CreatedDate);

            db.ExecuteNonQuery(dbCommand);
        }

        #endregion

        #region IAppliesManagement Members


        public Apply GetApplyByTimeRange(DateTime date, int range)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "SELECT [ID],[UserId],[ApplyDate],[TimeRange],"
                                + "[ApplyStatus],[DateCreated]"
                                + "FROM [Kpw_Applies] WHERE [ApplyDate]=@Date AND [TimeRange]=@TimeRange";

            DbCommand dbCommand = db.GetSqlStringCommand(sqlCommand);

            db.AddInParameter(dbCommand, "Date", DbType.DateTime, date);
            db.AddInParameter(dbCommand, "TimeRange", DbType.Int32, range);


            // DataSet that will hold the returned results		
            using (IDataReader reader = db.ExecuteReader(dbCommand))
            {
                if (reader.Read())
                {
                    return new Apply
                     {
                         ID = reader.GetInt32(0),
                         UserId = reader.GetString(1),
                         ApplyDate = reader.GetDateTime(2),
                         TimeRange = reader.GetInt32(3),
                         ApplyStatus = (ApplyStatus)reader.GetInt32(4),
                         CreatedDate = reader.GetDateTime(5)
                     };
                }
            }

            return null;

        }

        #endregion

        #region IAppliesManagement Members


        public IEnumerable<Apply> GetMyApplies(DateTime date, string userId)
        {
            List<Apply> applies = new List<Apply>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "SELECT [ID],[UserId],[ApplyDate],[TimeRange],"
                                + "[ApplyStatus],[DateCreated]"
                                + "FROM [Kpw_Applies] WHERE [UserId]=@UserId AND [ApplyDate] = @Today";

            DbCommand dbCommand = db.GetSqlStringCommand(sqlCommand);

            db.AddInParameter(dbCommand, "UserId", DbType.String, userId);
            db.AddInParameter(dbCommand, "Today", DbType.DateTime, date);


            // DataSet that will hold the returned results		
            using (IDataReader reader = db.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    applies.Add(new Apply
                    {
                        ID = reader.GetInt32(0),
                        UserId = reader.GetString(1),
                        ApplyDate = reader.GetDateTime(2),
                        TimeRange = reader.GetInt32(3),
                        ApplyStatus = (ApplyStatus)reader.GetInt32(4),
                        CreatedDate = reader.GetDateTime(5)
                    });
                }
            }

            return applies;
        }

        public void DeleteApply(int id)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "delete from Kpw_Applies WHERE [ID] = @ID";

            DbCommand dbCommand = db.GetSqlStringCommand(sqlCommand);

            // Retrieve products from the specified category.
            db.AddInParameter(dbCommand, "ID", DbType.Int32, id);

            db.ExecuteNonQuery(dbCommand);
        }

        #endregion

        #region IAppliesManagement Members


        public Apply GetApplyById(int id)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "SELECT [ID],[UserId],[ApplyDate],[TimeRange],"
                                + "[ApplyStatus],[DateCreated]"
                                + "FROM [Kpw_Applies] WHERE [ID]=@id";

            DbCommand dbCommand = db.GetSqlStringCommand(sqlCommand);

            db.AddInParameter(dbCommand, "id", DbType.Int32, id);

            // DataSet that will hold the returned results		
            using (IDataReader reader = db.ExecuteReader(dbCommand))
            {
                if (reader.Read())
                {
                    return new Apply
                    {
                        ID = reader.GetInt32(0),
                        UserId = reader.GetString(1),
                        ApplyDate = reader.GetDateTime(2),
                        TimeRange = reader.GetInt32(3),
                        ApplyStatus = (ApplyStatus)reader.GetInt32(4),
                        CreatedDate = reader.GetDateTime(5)
                    };
                }
            }

            return null;
        }

        #endregion
    }
}
