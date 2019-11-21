using NPoco;
using static DataRepository.Factories.DbFactory;

namespace DataRepository.Factories
{
    public interface IDBFactory
    {
        IDatabase GetConnection(AvailableConnections conn);
    }
}