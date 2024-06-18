using Server.DataBase.Entity;
using Server.DataBase.Repository;

namespace Server.Services
{
    public class LogsService : ILogsService
    {
        private readonly IUnitOfWork _unitOfWork;

        public LogsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Logs GetLogs(int id)
        {
            return _unitOfWork.Logs.Get(id);
        }

        public IEnumerable<Logs> GetAllLogs()
        {
            return _unitOfWork.Logs.GetAll();
        }

        public void CreateLog(Logs logs)
        {
            _unitOfWork.Logs.Add(logs);
            _unitOfWork.Complete();
        }

        public void DeleteLog(int id)
        {
            var logs = _unitOfWork.Logs.Get(id);
            if (logs != null)
            {
                _unitOfWork.Logs.Delete(logs);
                _unitOfWork.Complete();
            }
        }
    }
}
