using System.Data;

namespace BookingSystem.Storage
{
    public abstract class StorageBase
    {
        internal readonly IDbConnection _dbConnection;

        public StorageBase(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }
    }
}
