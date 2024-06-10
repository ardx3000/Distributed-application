using Server.DataBase.Entity;

namespace Server.DataBase.Repository
{
    public class LogsRepository : BaseRepository<Logs>
    {
        public LogsRepository(ApplicationContext context) : base(context)
        {

        }
    }
}
