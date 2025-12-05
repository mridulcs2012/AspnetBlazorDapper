namespace DataAccessLibrary.SqlDataAccess
{
    public interface ISqlDataAcess
    {
        Task<IEnumerable<T>> GetData<T, P>(string spName, P parameters, string connectionId = "conn");

        Task SaveData<T>(string spName, T paramerters, string connectionId = "conn");

    }
}