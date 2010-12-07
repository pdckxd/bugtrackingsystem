using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;
using Nairc.KpwFramework.DataModel;

namespace Nairc.KpwDataAccess
{
    public class ImageInfoDB:IImageInfoDB
    {
        
        public ImageInfoDB()
        {

        }

        #region IImageInfoDB Members

        public List<Nairc.KpwFramework.DataModel.ImageInfo> GetTop20Images()
        {

            List<ImageInfo> list = new List<ImageInfo>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "SELECT TOP 20 [ID],[Name],[Size],[Image],"
                                + "[ThumbImage],[BigImage],[FitsImage],"
                                + "[StarName],[Ra],[Dec],[CreateBy],[CreateDate]"
                                + "FROM [Kpw_Images]";

            DbCommand dbCommand = db.GetSqlStringCommand(sqlCommand);

            // Retrieve products from the specified category.
            

            // DataSet that will hold the returned results		
            using (IDataReader reader = db.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    list.Add(new ImageInfo()
                    {
                        ID = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Size = reader.GetInt32(2),
                        Image = KpwConfiguration.ImageRelativePath + reader.GetString(3),
                        ThumbImage = KpwConfiguration.ThumbImageRelativePath + reader.GetString(4),
                        BigImage = KpwConfiguration.BigImageRelativePath + reader.GetString(5),
                        FitsImage = KpwConfiguration.FitsImageRelativePath + reader.GetString(6),
                        StarName = reader.GetString(7),
                        Ra = reader.GetInt32(8),
                        Dec = reader.GetInt32(9),
                        CreatedBy = reader.GetString(10),
                        CreatedDate = reader.GetDateTime(11)
                    });
                }
            }

            return list;
        }

        public List<Nairc.KpwFramework.DataModel.ImageInfo> GetImageInfoByDate(DateTime date)
        {
            List<ImageInfo> list = new List<ImageInfo>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "SELECT [ID],[Name],[Size],[Image],"
                                + "[ThumbImage],[BigImage],[FitsImage],"
                                + "[StarName],[Ra],[Dec],[CreateBy],[CreateDate]"
                                + "FROM [Kpw_Images] WHERE [CreateDate]=@CREATEDATE";

            DbCommand dbCommand = db.GetSqlStringCommand(sqlCommand);

            // Retrieve products from the specified category.
            db.AddInParameter(dbCommand, "CREATEDATE", DbType.Date, date);

            // DataSet that will hold the returned results		
            using (IDataReader reader = db.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    list.Add(new ImageInfo()
                    {
                        ID = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Size = reader.GetInt32(2),
                        Image = KpwConfiguration.ImageRelativePath + reader.GetString(3),
                        ThumbImage = KpwConfiguration.ThumbImageRelativePath + reader.GetString(4),
                        BigImage = KpwConfiguration.BigImageRelativePath + reader.GetString(5),
                        FitsImage = KpwConfiguration.FitsImageRelativePath + reader.GetString(6),
                        StarName = reader.GetString(7),
                        Ra = reader.GetInt32(8),
                        Dec = reader.GetInt32(9),
                        CreatedBy = reader.GetString(10),
                        CreatedDate = reader.GetDateTime(11)

                    });
                }
            }

            return list;

        }

        public int AddImageInfo(Nairc.KpwFramework.DataModel.ImageInfo image)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "INSERT INTO [Kpw_Images] ([Name],[Size],[Image],"
                                + "[ThumbImage],[BigImage],[FitsImage],"
                                + "[StarName],[Ra],[Dec],[CreateBy],[CreateDate])"
                                + "VALUES(@Name,@Size,@Image,@ThumbImage,@BigImage,@FitsImage,@StarName,@Ra,@Dec,@CreatedBy,@CreatedDate)";

            DbCommand dbCommand = db.GetSqlStringCommand(sqlCommand);

            db.AddInParameter(dbCommand, "Name", DbType.String, image.Name);
            db.AddInParameter(dbCommand, "Size", DbType.Int32, image.Size);
            db.AddInParameter(dbCommand, "Image", DbType.String, image.Image);
            db.AddInParameter(dbCommand, "ThumbImage", DbType.String, image.ThumbImage);
            db.AddInParameter(dbCommand, "BigImage", DbType.String, image.BigImage);
            db.AddInParameter(dbCommand, "FitsImage", DbType.String, image.FitsImage);
            db.AddInParameter(dbCommand, "StarName", DbType.String, image.StarName);
            db.AddInParameter(dbCommand, "Ra", DbType.Int32, image.Ra);
            db.AddInParameter(dbCommand, "Dec", DbType.Int32, image.Dec);
            db.AddInParameter(dbCommand, "CreatedBy", DbType.String, image.CreatedBy);
            db.AddInParameter(dbCommand, "CreatedDate", DbType.Date, image.CreatedDate);

            return db.ExecuteNonQuery(dbCommand);
        }

        #endregion

        #region IImageInfoDB Members


        public ImageInfo GetImageByID(int id)
        {
            ImageInfo image = null;

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "SELECT [ID],[Name],[Size],[Image],"
                                + "[ThumbImage],[BigImage],[FitsImage],"
                                + "[StarName],[Ra],[Dec],[CreateBy],[CreateDate]"
                                + "FROM [Kpw_Images] WHERE [ID]=@CREATEDATE";

            DbCommand dbCommand = db.GetSqlStringCommand(sqlCommand);

            // Retrieve products from the specified category.
            db.AddInParameter(dbCommand, "ID", DbType.Int32, id);

            // DataSet that will hold the returned results		
            using (IDataReader reader = db.ExecuteReader(dbCommand))
            {
                if (reader.Read())
                {
                    image = new ImageInfo()
                    {
                        ID = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Size = reader.GetInt32(2),
                        Image = KpwConfiguration.ImageRelativePath + reader.GetString(3),
                        ThumbImage = KpwConfiguration.ThumbImageRelativePath + reader.GetString(4),
                        BigImage = KpwConfiguration.BigImageRelativePath + reader.GetString(5),
                        FitsImage = KpwConfiguration.FitsImageRelativePath + reader.GetString(6),
                        StarName = reader.GetString(7),
                        Ra = reader.GetInt32(8),
                        Dec = reader.GetInt32(9),
                        CreatedBy = reader.GetString(10),
                        CreatedDate = reader.GetDateTime(11)

                    };
                }
            }

            return image;
        }

        public void UpdateImage(ImageInfo image)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "UPDATE [Kpw_Images]"
                                  + "SET [Name] = @Name"
                                     + ",[Size] = @Size"
                                      + ",[Image] = @Image"
                                      + ",[ThumbImage] = @ThumbImage"
                                      + ",[BigImage] = @BigImage"
                                      + ",[FitsImage] = @FitsImage"
                                      + ",[StarName] = @StarName"
                                      + ",[Ra] = @Ra"
                                      + ",[Dec] = @Dec"
                                      + ",[CreateBy] = @CreateBy"
                                      + ",[CreateDate] = @CreateDate"
                                      + "WHERE [ID] = @ID";

            DbCommand dbCommand = db.GetSqlStringCommand(sqlCommand);

            // Retrieve products from the specified category.
            db.AddInParameter(dbCommand, "ID", DbType.Int32, image.ID);
            db.AddInParameter(dbCommand, "Name", DbType.String, image.Name);
            db.AddInParameter(dbCommand, "Size", DbType.Int32, image.Size);
            db.AddInParameter(dbCommand, "Image", DbType.String, image.Image);
            db.AddInParameter(dbCommand, "ThumbImage", DbType.String, image.ThumbImage);
            db.AddInParameter(dbCommand, "BigImage", DbType.String, image.BigImage);
            db.AddInParameter(dbCommand, "FitsImage", DbType.String, image.FitsImage);
            db.AddInParameter(dbCommand, "StarName", DbType.String, image.StarName);
            db.AddInParameter(dbCommand, "Ra", DbType.Int32, image.Ra);
            db.AddInParameter(dbCommand, "Dec", DbType.Int32, image.Dec);
            db.AddInParameter(dbCommand, "CreatedBy", DbType.String, image.CreatedBy);
            db.AddInParameter(dbCommand, "CreatedDate", DbType.Date, image.CreatedDate);

            db.ExecuteNonQuery(dbCommand);

            
        }

        #endregion
    }
}
