namespace Server.DataBase.Entity
{
    public class Logs
    {
        public int Id { get; set; }
        public string UserID { get; set; }
        public DateTime dateTime { get; set; }
        public string Command { get; set; }
        public string result { get; set; }
    }
}
