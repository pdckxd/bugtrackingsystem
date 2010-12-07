namespace Nairc.KPWPortal.DataLayer.DataObjects
{
    /// <summary>
    /// Factory of factories. This class is a factory class that creates
    /// data-base specific factories which in turn create data acces objects.
    /// 
    /// GoF Design Patterns: Factory.
    /// </summary>
    /// <remarks>
    /// This is the abstract factory design pattern applied in a hierarchy
    /// in which there is a factory of factories.
    /// </remarks>
    public class DaoFactories
    {
        /// <summary>
        /// Gets a provider specific (i.e. database specific) factory 
        /// 
        /// GoF Design Pattern: Factory
        /// </summary>
        /// <param name="dataProvider">Database provider.</param>
        /// <returns>Data access object factory.</returns>
        public static DaoFactory GetFactory(string dataProvider)
        {
            // Return the requested DaoFactory
            switch (dataProvider)
            {
                case "System.Data.OleDb":
                    return new MSAccess.MSAccessDaoFactory();
                case "System.Data.SqlClient":
                    return new SqlServer.SqlServerDaoFactory();
                case "System.Data.OracleClient":
                    return new Oracle.OracleDaoFactory();
                    // Just in case: the Design Pattern Framework always has something available.
                default:
                    return new SqlServer.SqlServerDaoFactory();
            }
        }
    }
}