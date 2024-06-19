using Server.DataBase.Entity;

namespace Server.Services
{
    public interface ILogsService
    {
        Logs GetLogs(int id);
        IEnumerable<Logs> GetAllLogs();
        void CreateLog(Logs log);
        void DeleteLog(int id); 
    }
}
