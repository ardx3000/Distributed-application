namespace Server.DataBase.Entity
{
    public class Logs
    {
        public int Id { get; set; }//Primary key
        public int UserID { get; set; }//Forign key
        public DateTime DateTime { get; set; }
        public string Command { get; set; }
        public string Result { get; set; }

        // Navigation property
        public User User { get; set; }
    }
}
