using DataRepository.NPocoRepository.MapObjects;
using NPoco;
using NPoco.RowMappers;
using NPoco.FluentMappings;
using System.Data.SqlClient;

namespace DataRepository.Factories
{
    public class DbFactory : IDBFactory
    {
        private readonly string _authConnectionString;
        private readonly string _appConnectionsString;

        public DbFactory(string authConnectionString, string appConnectionString)
        {
            _authConnectionString = authConnectionString;
            _appConnectionsString = appConnectionString;
        }

        public IDatabase GetConnection(AvailableConnections conn)
        {

            return conn switch
            {
                AvailableConnections.Auth => new Database(_authConnectionString, DatabaseType.SqlServer2008, SqlClientFactory.Instance),
                AvailableConnections.App => new Database(_appConnectionsString, DatabaseType.SqlServer2008, SqlClientFactory.Instance),
                _ => null,
            };
        }

        public enum AvailableConnections
        {
            Auth,
            App
        }
    }
}